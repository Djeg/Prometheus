using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Djeg.Prometheus.Motion
{
    /// <summary>
    /// Add the ability to an object to drag on walls
    /// </summary>
    [RequireComponent(typeof(JumpMotion))]
    [RequireComponent(typeof(MoveMotion))]
    [RequireComponent(typeof(TurnAroundMotion))]
    [RequireComponent(typeof(AerialMovementMotion))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class DragMotion : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        [Header("Parameters")]
        private GameObject _dragDetectorUp = null;

        [SerializeField]
        private GameObject _dragDetectorDown = null;

        [SerializeField]
        private LayerMask _whatIsWall = Physics2D.AllLayers;

        [SerializeField]
        private float _releaseForce = 10f;

        [SerializeField]
        private float _jumpForce = 20f;

        [SerializeField]
        private float _limit = -1f;

        [Space(20)]

        [SerializeField]
        [Header("Debug")]
        private Color _debugColor = Color.yellow;

        [Space(20)]

        [SerializeField]
        [Header("Events")]
        private OnDragEvent _onDrag = new OnDragEvent();

        [SerializeField]
        private OnDragJumpEvent _onDragJump = new OnDragJumpEvent();

        [SerializeField]
        private OnDragFallEvent _onDragFall = new OnDragFallEvent();

        private bool _isWallColliding = false;

        private bool _isDraging = false;

        private bool _requestJump = false;

        private bool _requestRelease = false;

        private float _currentDirection = 0f;

        private JumpMotion _jump = null;

        private MoveMotion _move = null;

        private AerialMovementMotion _aerialMovement = null;

        private Rigidbody2D _body = null;

        private TurnAroundMotion _turnAround = null;

        # endregion

        # region PropertyAccessors

        public bool IsDraging { get => _isDraging; }

        public OnDragEvent OnDrag { get => _onDrag; }
        public OnDragJumpEvent OnDragJump { get => _onDragJump; }
        public OnDragFallEvent OnDragFall { get => _onDragFall; }

        # endregion

        # region PublicMethods

        /// <summary>
        /// Detect wall collision
        /// </summary>
        public bool DetectWallCollision()
        {
            Collider2D[] upColliders = Physics2D.OverlapPointAll(
                _dragDetectorUp.transform.position,
                _whatIsWall
            );

            Collider2D[] downColliders = Physics2D.OverlapPointAll(
                _dragDetectorDown.transform.position,
                _whatIsWall
            );

            return upColliders.Length > 0 && downColliders.Length > 0;
        }

        /// <summary>
        /// Detect draging
        /// </summary>
        public bool DetectDraging()
        {
            bool draging = _jump.IsJumping && _isWallColliding;

            if (draging && !_isDraging)
                OnDrag.Invoke();

            return draging;
        }

        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _jump           = GetComponent<JumpMotion>();
            _move           = GetComponent<MoveMotion>();
            _body           = GetComponent<Rigidbody2D>();
            _aerialMovement = GetComponent<AerialMovementMotion>();
            _turnAround     = GetComponent<TurnAroundMotion>();
        }

        private void OnEnable()
        {
            OnDrag.AddListener(DisableMovement);

            OnDragJump.AddListener(EnableMovement);
            OnDragFall.AddListener(EnableMovement);

            _jump.OnLand.AddListener(EnableMovement);
            _jump.OnLand.AddListener(EnableAerialMovement);
        }

        private void OnDisable()
        {
            OnDrag.RemoveListener(DisableMovement);

            OnDragJump.RemoveListener(EnableMovement);
            OnDragFall.RemoveListener(EnableMovement);

            _jump.OnLand.RemoveListener(EnableMovement);
            _jump.OnLand.RemoveListener(EnableAerialMovement);
        }

        private void FixedUpdate()
        {
            if (_requestJump)
            {
                _requestJump = false;
                float direction = _move.IsLoogingRight ? -1 : 1;

                Vector2 force = new Vector2(
                    direction * _jumpForce,
                    0
                );

                _body.AddForce(force, ForceMode2D.Impulse);
                _move.IsLocked = false;
                _move.Movement = new Vector2(direction, 0);

                OnDragJump.Invoke();

                return;
            }

            if (_requestRelease)
            {
                _requestRelease = false;
                float direction = _move.IsLoogingRight ? -1 : 1;

                Vector2 force = new Vector2(
                    direction * _releaseForce,
                    0
                );

                _body.AddForce(force, ForceMode2D.Impulse);
                _move.IsLocked = false;
                _move.Movement = new Vector2(direction, 0);

                OnDragFall.Invoke();

                return;
            }

            _isWallColliding = DetectWallCollision();
            _isDraging = DetectDraging();

            if (!_isDraging)
                return;

            if (_body.velocity.y >= _limit)
                return;

            _body.velocity = new Vector2(
                _body.velocity.x,
                _limit
            );

        }

        private void OnDrawGizmosSelected()
        {
            if (null == _dragDetectorUp || null == _dragDetectorDown)
                return;

            Gizmos.color = _debugColor;

            Gizmos.DrawLine(_dragDetectorUp.transform.position, _dragDetectorDown.transform.position);
        }

        private void OnCancelInputStart()
        {
            if (!_isDraging)
                return;

            _isDraging = false;
            _requestRelease = true;
        }

        private void OnJumpInputStart()
        {
            if (!_isDraging)
                return;

            if (
                (_move.Direction == MovementDirection.Right && _move.Movement.x > 0)
                || (_move.Direction == MovementDirection.Left && _move.Movement.x < 0)
            )
                return;

            _jump.AllowAerialJump = true;
            _isDraging = false;
            _requestJump = true;
        }

        private void DisableMovement()
        {
            _move.IsLocked = true;
            _move.Reset();

            _move.enabled = false;
            _aerialMovement.enabled = false;
        }

        private void EnableMovement()
        {
            _move.IsLocked = false;
            _move.enabled = true;
        }

        private void EnableAerialMovement()
        {
            _aerialMovement.enabled = true;

            if (_currentDirection == 0f)
                _move.Movement = Vector2.zero;
        }

        private void OnMoveInput(InputValue input)
        {
            _currentDirection = input.Get<Vector2>().x;
        }

        # endregion
    }
}
