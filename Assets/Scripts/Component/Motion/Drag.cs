using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Component.Motion
{
    /// <summary>
    /// Add the ability to an object to drag on wall
    /// </summary>
    [RequireComponent(typeof(Jump))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Drag : MonoBehaviour
    {
        # region PublicAttributes

        /// <summary>
        /// The drag parameters
        /// </summary>
        public Data.Motion.Drag.Parameter Parameter = new Data.Motion.Drag.Parameter();

        /// <summary>
        /// The drag animation
        /// </summary>
        public Data.Motion.Drag.Animation Animation = new Data.Motion.Drag.Animation();

        /// <summary>
        /// The drag events
        /// </summary>
        [Space(20)]
        public Data.Motion.Drag.Event Event = new Data.Motion.Drag.Event();

        # endregion

        # region PrivateAttributes

        /// <summary>
        /// A reference to the Jump
        /// </summary>
        private Jump _jump = null;

        /// <summary>
        /// A reference to the Rigidbody2D
        /// </summary>
        private Rigidbody2D _body = null;

        /// <summary>
        /// A reference to the animator
        /// </summary>
        public Animator _animator = null;

        /// <summary>
        /// The authorized direction for the jump
        /// </summary>
        private float _initialDirection = 0f;

        # endregion

        # region PropertyAccessors

        public bool CanJump =>
            Parameter.IsDraging && Parameter.IsRequestingJump && (
                (_initialDirection < 0 && Parameter.Direction > 0)
                || (_initialDirection > 0 && Parameter.Direction < 0)
            );

        public bool CanFall =>
            Parameter.IsDraging && Parameter.IsRequestingFall;

        # endregion

        # region PublicMethods

        /// <summary>
        /// Initialize references
        /// </summary>
        private void Awake()
        {
            _body     = GetComponent<Rigidbody2D>();
            _jump     = GetComponent<Jump>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Do the drag effect
        /// </summary>
        private void FixedUpdate()
        {
            UpdateInitialDirection();
            DetectDrag();
            LimitVelocityOnDrag();
            MakeDragJump();
            MakeDragFall();
        }

        /// <summary>
        /// Update the initial direction
        /// </summary>
        public void UpdateInitialDirection()
        {
            if (!Parameter.IsDraging)
            {
                _initialDirection = 0;

                return;
            }

            _initialDirection = Parameter.DragUpCollider.transform.position.x - transform.position.x;
        }

        /// <summary>
        /// Make a drag jump
        /// </summary>
        public void MakeDragJump()
        {
            if (!CanJump)
                return;

            float direction = _initialDirection < 0 ? 1 : -1;

            Vector2 force = new Vector2(direction * Parameter.DragJumpForceX, Parameter.DragJumpForceY);
            _body.AddForce(force, ForceMode2D.Impulse);

            _animator.SetBool(Animation.DragingBoolName, false);
            _animator.SetTrigger(Animation.DragJumpTriggerName);

            Parameter.IsDraging = false;
            Parameter.IsRequestingJump = false;
            Parameter.IsRequestingFall = false;
        }

        /// <summary>
        /// Make a drag fall
        /// </summary>
        public void MakeDragFall()
        {
            if (!CanFall)
                return;

            float direction = _initialDirection < 0 ? 1 : -1;

            Vector2 force = new Vector2(direction * Parameter.DragReleaseForceX, Parameter.DragReleaseForceY);
            _body.AddForce(force, ForceMode2D.Impulse);

            _animator.SetBool(Animation.DragingBoolName, false);

            Parameter.IsDraging = false;
            Parameter.IsRequestingJump = false;
            Parameter.IsRequestingFall = false;
        }

        /// <summary>
        /// Limit the velocity when falling and draging
        /// </summary>
        public void LimitVelocityOnDrag()
        {
            if (Parameter.IsDraging)
                _body.velocity = new Vector2(0, _body.velocity.y);

            if (!Parameter.IsDraging || !_jump.Parameter.IsFalling)
                return;

            _body.velocity = new Vector2(
                _body.velocity.x,
                _body.velocity.y * Parameter.Intensity
            );
        }

        /// <summary>
        /// Detect wall collision
        /// </summary>
        public void DetectDrag()
        {
            Collider2D[] upColliders = Physics2D.OverlapPointAll(
                Parameter.DragUpCollider.transform.position,
                Parameter.WhatIsWall
            );

            Collider2D[] downColliders = Physics2D.OverlapPointAll(
                Parameter.DragDownCollider.transform.position,
                Parameter.WhatIsWall
            );

            bool isDraging = _jump.Parameter.IsOnAir && 0 != upColliders.Length && 0 != downColliders.Length;

            if (!Parameter.IsDraging && isDraging)
            {
                _animator.SetTrigger(Animation.DragTriggerName);
                _animator.SetBool(Animation.DragingBoolName, true);

                Event.OnDragStart.Invoke();
            }

            if (Parameter.IsDraging && !isDraging)
            {
                _animator.SetBool(Animation.DragingBoolName, false);

                Event.OnDragEnd.Invoke();
            }

            Parameter.IsDraging = isDraging;
        }

        /// <summary>
        /// Draw the draging detection line
        /// </summary>
        public void OnDrawGizmosSelected()
        {
            if (null == Parameter.DragUpCollider || null == Parameter.DragDownCollider)
                return;

            Gizmos.color = Parameter.DebugColor;

            Gizmos.DrawLine(
                Parameter.DragUpCollider.transform.position,
                Parameter.DragDownCollider.transform.position
            );
        }

        /// <summary>
        /// Update the draging direction
        /// </summary>
        public void UpdateDirectionFromInput(InputAction.CallbackContext context)
        {
            if (!Parameter.IsDraging)
            {
                Parameter.Direction = 0f;

                return;
            }

            Parameter.Direction = context.ReadValue<Vector2>().x;
        }

        /// <summary>
        /// Update the jump request from inputs
        /// </summary>
        public void UpdateJumpRequestFromInput(InputAction.CallbackContext context)
        {
            if (context.performed)
                Parameter.IsRequestingJump = true;

            if (context.canceled)
                Parameter.IsRequestingJump = false;
        }

        /// <summary>
        /// Update the fall request from inputs
        /// </summary>
        public void UpdateFallRequestFromInput(InputAction.CallbackContext context)
        {
            if (context.performed)
                Parameter.IsRequestingFall = true;

            if (context.canceled)
                Parameter.IsRequestingFall = false;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
