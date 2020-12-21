using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Control the ability of a game object to move.
     * </summary>
     */
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveMotion : MonoBehaviour, IMovableMotion
    {
        # region Properties

        [SerializeField]
        [Header("Parameters")]
        [Tooltip("Define the speed of the movement")]
        private float _speed = 200f;

        [SerializeField]
        [Tooltip("If set to true move the object verticaly")]
        private bool _isAerial = false;

        [SerializeField]
        [Tooltip("The algorithm used for the movement")]
        private MovementAlgorithm _algorithm = MovementAlgorithm.Normal;

        [SerializeField]
        [Tooltip("The smooth time when Algorithm is on Smoothing")]
        private float _smoothTime = 0.25f;

        [SerializeField]
        [Header("Events")]
        private OnDirectionChangedEvent _onDirectionChanged = new OnDirectionChangedEvent();

        [SerializeField]
        private OnMovementStartEvent _onMovementStart = new OnMovementStartEvent();

        [SerializeField]
        private OnMovementStopEvent _onMovementStop = new OnMovementStopEvent();

        [SerializeField]
        private OnBeforeMovingEvent _OnBeforeMoving = new OnBeforeMovingEvent();

        [SerializeField]
        private OnAfterMovingEvent _OnAfterMoving = new OnAfterMovingEvent();

        private float _lookDirection = 1f;

        private Vector2 _movement = Vector2.zero;

        private bool _isMoving = false;

        private Rigidbody2D _body = null;

        private Vector2 _velocity = Vector2.zero;

        # endregion

        # region PropertyAccessors

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public MovementAlgorithm Algorithm
        {
            get => _algorithm;
            set => _algorithm = value;
        }

        public MovementDirection Direction
        {
            get => _lookDirection == 1
                ? MovementDirection.Right
                : MovementDirection.Left;
        }

        public Vector2 Movement
        {
            get => _movement;
            set
            {
                _movement = new Vector2(value.x, value.y);

                Vector2 normalized = new Vector2(
                    value.normalized.x == 0 ? 0 : value.normalized.x > 0 ? 1 : -1,
                    value.normalized.y == 0 ? 0 : value.normalized.y > 0 ? 1 : -1
                );

                float lookDirection = 0 != normalized.x ? normalized.x : _lookDirection;
                bool mooving = _isAerial
                    ? normalized.x != 0 || normalized.y != 0
                    : normalized.x != 0;

                if (mooving != _isMoving)
                {
                    _isMoving = mooving;

                    if (_isMoving)
                        OnMovementStart.Invoke();
                    else
                        OnMovementStop.Invoke();
                }

                if (_lookDirection != lookDirection)
                {
                    _lookDirection = lookDirection;

                    OnDirectionChanged.Invoke();
                }
            }
        }

        public bool IsMoving { get => _isMoving; }

        public bool IsLookingLeft { get => _lookDirection == -1; }

        public bool IsLoogingRight { get => _lookDirection == 1; }

        public OnDirectionChangedEvent OnDirectionChanged { get => _onDirectionChanged; }

        public OnMovementStartEvent OnMovementStart { get => _onMovementStart; }

        public OnMovementStopEvent OnMovementStop { get => _onMovementStop; }

        public OnBeforeMovingEvent OnBeforeMoving { get => _OnBeforeMoving; }

        public OnAfterMovingEvent OnAfterMoving { get => _OnAfterMoving; }

        # endregion

        # region PublicMethods

        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            _body.velocity = new Vector2(0, _body.velocity.y);
        }

        private void OnEnable()
        {
            _velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            OnBeforeMoving.Invoke();

            if (_algorithm == MovementAlgorithm.Normal)
                Move();
            else
                SmoothMove();

            OnAfterMoving.Invoke();
        }

        private void Move()
        {
            float moveX = _speed * _movement.x * Time.deltaTime;
            float moveY = _speed * _movement.y * Time.deltaTime;

            _body.velocity = new Vector2(
                moveX,
                _isAerial ? moveY : _body.velocity.y
            );
        }

        private void SmoothMove()
        {
            float moveX = _speed * _movement.x * Time.deltaTime;
            float moveY = _speed * _movement.y * Time.deltaTime;

            Vector2 target = new Vector2(
                moveX,
                _isAerial ? moveY : _body.velocity.y
            );

            _body.velocity = Vector2.SmoothDamp(
                _body.velocity,
                target,
                ref _velocity,
                _smoothTime
            );
        }

        private void OnMove(InputValue input)
        {
            Movement = input.Get<Vector2>();
        }

        # endregion
    }
}
