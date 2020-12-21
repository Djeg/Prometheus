using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Add the ability to an object to jump.
     * </summary>
     */
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MoveMotion))]
    public class JumpMotion : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        [Header("Parameters")]
        private float _force = 10f;

        [SerializeField]
        private float _decreaseModifer = .5f;

        [SerializeField]
        private LayerMask _whatIsGround = Physics2D.AllLayers;

        [SerializeField]
        private float _raycastLength = .7f;

        [SerializeField]
        private Color _debugColor = Color.blue;

        [SerializeField]
        private bool _allowAerialJump = false;

        [Space(20)]

        [SerializeField]
        private OnJumpEvent _onJump = new OnJumpEvent();

        [SerializeField]
        private OnFallEvent _onFall = new OnFallEvent();

        [SerializeField]
        private OnLandEvent _onLand = new OnLandEvent();

        [SerializeField]
        private OnBeforeJumpingEvent _onBeforeJumping = new OnBeforeJumpingEvent();

        [SerializeField]
        private OnAfterJumpingEvent _onAfterJumping = new OnAfterJumpingEvent();

        private bool _jumping = false;

        private bool _falling = false;

        private bool _requestJump = false;

        private bool _holdingJump = false;

        private float _previousY = 0f;

        private Rigidbody2D _body = null;

        private MoveMotion _movement = null;

        # endregion

        # region PropertyAccessors

        public float Force { get => _force; }
        public bool IsJumping { get => _jumping; }
        public bool IsFalling { get => _falling; }
        public bool AllowAerialJump
        {
            get => _allowAerialJump;
            set => _allowAerialJump = value;
        }

        public OnJumpEvent OnJump { get => _onJump; }
        public OnFallEvent OnFall { get => _onFall; }
        public OnLandEvent OnLand { get => _onLand; }
        public OnBeforeJumpingEvent OnBeforeJumping { get => _onBeforeJumping; }
        public OnAfterJumpingEvent OnAfterJumping { get => _onAfterJumping; }

        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _movement = GetComponent<MoveMotion>();
        }

        private void OnEnable()
        {
            _previousY = transform.position.y;

            OnJump.AddListener(DisableMovement);
            OnLand.AddListener(EnableMovement);
        }

        private void OnDisable()
        {
            OnJump.RemoveListener(DisableMovement);
            OnLand.RemoveListener(EnableMovement);
        }

        private void FixedUpdate()
        {
            DetectJumping();
            DetectFalling();

            if (!HasTriggerJump())
            {
                if (!_holdingJump && !_falling)
                {
                    _body.velocity = new Vector2(
                        _body.velocity.x,
                        _body.velocity.y * _decreaseModifer
                    );
                }

                return;
            }

            _body.AddForce(Vector2.up * _force, ForceMode2D.Impulse);

            OnAfterJumping.Invoke();
        }

        private bool HasTriggerJump()
        {
            bool trigger = (!_falling && _requestJump) || (_requestJump && _allowAerialJump);

            _requestJump = false;
            _allowAerialJump = false;

            if (trigger)
                OnBeforeJumping.Invoke();

            return trigger;
        }

        private void DetectFalling()
        {
            bool falling = _jumping && _previousY > transform.position.y;

            if (falling && !_falling)
                OnFall.Invoke();

            _falling = falling;
            _previousY = transform.position.y;
        }

        private void DetectJumping()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                _raycastLength,
                _whatIsGround
            );

            bool jumping = null == hit.collider;

            if (jumping && !_jumping)
                OnJump.Invoke();

            if (!jumping && _jumping)
                OnLand.Invoke();

            _jumping = jumping;
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 center = gameObject.transform.position;
            Vector3 end = new Vector3(
                center.x,
                center.y - _raycastLength,
                center.z
            );

            Gizmos.color = _debugColor;

            Gizmos.DrawLine(
                center,
                end
            );
        }

        private void OnJumpInputStart(InputValue input)
        {
            _requestJump = true;
            _holdingJump = true;
        }

        private void OnJumpInputStop(InputValue input)
        {
            _requestJump = false;
            _holdingJump = false;
        }

        private void DisableMovement()
        {
            _movement.KeepVelocityOnDisabled = true;
            _movement.enabled = false;
        }

        private void EnableMovement()
        {
            _movement.enabled = true;
        }

        # endregion
    }
}
