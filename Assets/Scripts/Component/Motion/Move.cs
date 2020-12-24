using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Component.Motion
{
    /// <summary>
    /// Add the ability to an object to move
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Move : MonoBehaviour
    {
        # region PublicAttributes

        /// <summary>
        /// The move data
        /// </summary>
        public Data.Motion.Move.Parameter Parameter = new Data.Motion.Move.Parameter();

        /// <summary>
        /// The feature
        /// </summary>
        public Data.Motion.Move.Feature Feature = new Data.Motion.Move.Feature();

        /// <summary>
        /// The animation
        /// </summary>
        public Data.Motion.Move.Animation Animation = new Data.Motion.Move.Animation();

        /// <summary>
        /// The event
        /// </summary>
        [Space(20)]
        public Data.Motion.Move.Event Event = new Data.Motion.Move.Event();

        # endregion

        # region PrivateAttributes

        /// <summary>
        /// The rigidbody 2D
        /// </summary>
        private Rigidbody2D _body = null;

        /// <summary>
        /// A reference to the animator
        /// </summary>
        private Animator _animator = null;

        /// <summary>
        /// A reference to the previous body veloicty on the X axis
        /// </summary>
        private float _previousVelocity = 0f;

        /// <summary>
        /// Store a boolean to know if the object has stop
        /// </summary>
        private bool _hasStop = false;

        /// <summary>
        /// Store a boolean to know if the object has just been mooving
        /// </summary>
        private bool _hasMove = false;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Test if the subject is going right
        /// </summary>
        public bool IsGoingRight { get => _body.velocity.x > 0; }

        /// <summary>
        /// Test if the subject is going left
        /// </summary>
        public bool IsGoingLeft { get => _body.velocity.x < 0; }

        # endregion

        # region PublicMethods

        /// <summary>
        /// Return a leoft direction as a float
        /// </summary>
        public static float LeftDirection() => -1;

        /// <summary>
        /// Return the right direction float
        /// </summary>
        public static float RightDirection() => 1;

        /// <summary>
        /// Inverse a direction
        /// </summary>
        public static float InverseDirection(float direction) => direction > 0 ? -direction : Mathf.Abs(direction);

        /// <summary>
        /// Update the direction from an InputAction
        /// </summary>
        public void UpdateMoveDirection(InputAction.CallbackContext ctx) =>
            Parameter.Direction = ctx.ReadValue<Vector2>().x;

        /// <summary>
        /// Retrieve the dependencies
        /// </summary>
        public void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Set the previous velocity
        /// </summary>
        public void OnEnable()
        {
            _previousVelocity = _body.velocity.x;
            _hasStop = false;
            _hasMove = false;
        }

        /// <summary>
        /// Move the object according the the move data
        /// </summary>
        public void FixedUpdate()
        {
            ApplyMovement();
            UpdateAnimatorParameters();
            UpdateDirection();
        }

        /// <summary>
        /// Apply a movement
        /// </summary>
        public void ApplyMovement()
        {
            // First dispatch the before moving event
            Event.OnBeforeMoving.Invoke();

            // Move the object using the velocity
            _body.velocity = new Vector2(
                Parameter.Speed * Parameter.Direction * Time.deltaTime,
                _body.velocity.y
            );

            // Detect if the object has stop
            if (_body.velocity.x == 0 && !_hasStop)
            {
                _hasStop = true;
                _hasMove = false;

                Event.OnStop.Invoke();
            }

            // Detect if the object has move
            if (_body.velocity.x != 0 && !_hasMove)
            {
                _hasMove = true;
                _hasStop = false;

                Event.OnMove.Invoke();
            }

            // Finally dispatch the after moving event
            Event.OnAfterMoving.Invoke();
        }

        /// <summary>
        /// Update animator parameters
        /// </summary>
        public void UpdateAnimatorParameters()
        {
            float velocity = Animation.IsAbsolute
                ? Mathf.Abs(_body.velocity.x)
                : _body.velocity.x
            ;

            _animator.SetFloat(Animation.FloatParameterName, velocity);
        }

        /// <summary>
        /// Update the object direction
        /// </summary>
        public void UpdateDirection()
        {
            if (_body.velocity.x == 0 || !Feature.IsTurningAround)
                return;

            if (
                (_previousVelocity < 0 && _body.velocity.x > 0)
                || (_previousVelocity > 0 && _body.velocity.x < 0)
            ) {
                transform.Rotate(0f, 180f, 0f);

                Event.OnTurnAround.Invoke();
            }

            _previousVelocity = _body.velocity.x;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
