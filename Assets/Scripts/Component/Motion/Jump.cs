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
    /// Add the ability to jump to any game object
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Jump : MonoBehaviour
    {
        # region PublicAttributes

        /// <summary>
        /// The parameters
        /// </summary>
        public Data.Motion.Jump.Parameter Parameter = new Data.Motion.Jump.Parameter();

        /// <summary>
        /// The Animation
        /// </summary>
        public Data.Motion.Jump.Animation Animation = new Data.Motion.Jump.Animation();

        /// <summary>
        /// The event
        /// </summary>
        [Space(20)]
        public Data.Motion.Jump.Event Event = new Data.Motion.Jump.Event();

        # endregion

        # region PrivateAttributes

        /// <summary>
        /// A reference to the Rigidbody2D
        /// </summary>
        private Rigidbody2D _body = null;

        /// <summary>
        /// A reference to the Animation
        /// </summary>
        private Animator _animator = null;

        /// <summary>
        /// A reference on the previous velocity on the Y axis.
        /// </summary>
        private float _previousPosition = 0f;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Initialize the Jump component
        /// </summary>
        public void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Set up the velocity
        /// </summary>
        public void OnEnable()
        {
            _previousPosition = transform.position.y;
        }

        /// <summary>
        /// Trigger on each fixed update frame
        /// </summary>
        public void FixedUpdate()
        {
            DetectGroundColision();
            DetectFallingState();

            if (Parameter.IsOnAir)
                Event.OnBeforeJumping.Invoke();

            if (!Parameter.IsOnAir && Parameter.IsRequested)
            {
                Parameter.IsRequested = false;

                // make the jump
                _body.AddForce(Parameter.ForceVector, ForceMode2D.Impulse);
            }

            if (Parameter.IsOnAir && !Parameter.IsHolding && !Parameter.IsFalling)
                // Decrease the velocity when not holding the jump button
                _body.velocity = new Vector2(
                    _body.velocity.x,
                    _body.velocity.y * Parameter.FallingModifier
                );

            if (Parameter.IsOnAir)
                Event.OnAfterJumping.Invoke();
        }

        /// <summary>
        /// Detect ground colision
        /// </summary>
        public void DetectGroundColision()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                Parameter.RaycastLength,
                Parameter.WhatIsGround
            );

            bool onAir = null == hit.collider;

            // Update the animator state
            _animator.SetBool(Animation.IsOnTheAirBoolName, onAir);

            if (onAir && !Parameter.IsOnAir)
            {
                _animator.SetTrigger(Animation.JumpTriggerName);

                Event.OnJump.Invoke();
            }

            if (!onAir && Parameter.IsOnAir)
                Event.OnLand.Invoke();

            Parameter.IsOnAir = onAir;
        }

        /// <summary>
        /// Detect the falling state
        /// </summary>
        public void DetectFallingState()
        {
            if (!Parameter.IsOnAir)
            {
                _animator.SetBool(Animation.FallingBoolName, false);

                Parameter.IsFalling = false;
                _previousPosition = transform.position.y;

                return;
            }

            bool falling = _previousPosition > transform.position.y;
            _animator.SetBool(Animation.FallingBoolName, falling);

            if (falling && !Parameter.IsFalling)
            {
                _animator.SetTrigger(Animation.FallTriggerName);

                Event.OnFall.Invoke();
            }

            Parameter.IsFalling = falling;
            _previousPosition = transform.position.y;
        }

        /// <summary>
        /// Synchronized the input with the jump parameters
        /// </summary>
        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Parameter.IsRequested = true;
                Parameter.IsHolding = true;
            }

            if (context.canceled)
            {
                Parameter.IsRequested = false;
                Parameter.IsHolding = false;
            }
        }

        # endregion

        # region PrivateMethods

        /// <summary>
        /// Draw the raycast used for the ground collision using gizsmos
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Parameter.DebugColor;

            Gizmos.DrawLine(
                transform.position,
                new Vector2(
                    transform.position.x,
                    transform.position.y - Parameter.RaycastLength
                )
            );
        }

        # endregion
    }
}
