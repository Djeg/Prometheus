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
    public class SmoothMove : MonoBehaviour
    {
        # region PublicAttributes

        /// <summary>
        /// The parameter
        /// </summary>
        public Data.Motion.SmoothMove.Parameter Parameter = new Data.Motion.SmoothMove.Parameter();

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
        /// Store a boolean to know if the object has stop
        /// </summary>
        private bool _hasStop = false;

        /// <summary>
        /// Store a boolean to know if the object has just been mooving
        /// </summary>
        private bool _hasMove = false;

        /// <summary>
        /// A reference of the smooth velocity used by the SmoothDamp
        /// algorithm
        /// </summary>
        private Vector2 _velocity = Vector2.zero;

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
            _body     = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Set the previous velocity and boolean trigger
        /// </summary>
        public void OnEnable()
        {
            _velocity         = _body.velocity;
            _hasStop          = false;
            _hasMove          = false;
        }

        /// <summary>
        /// Move the object according the the move data
        /// </summary>
        public void FixedUpdate()
        {
            ApplyMovement();
            UpdateAnimatorParameters();
        }

        /// <summary>
        /// Apply a movement
        /// </summary>
        public void ApplyMovement()
        {
            // First dispatch the before moving event
            Event.OnBeforeMoving.Invoke();

            // Move the object using the velocity
            _body.velocity = Vector2.SmoothDamp(
                _body.velocity,
                new Vector2(
                    Parameter.Speed * Parameter.Direction * Time.deltaTime,
                    _body.velocity.y
                ),
                ref _velocity,
                Parameter.SmoothTime
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

        # endregion

        # region PrivateMethods
        # endregion
    }
}
