using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Control the aerial movement of any object that is in the air.
     * </summary>
     */
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MoveMotion))]
    public class AerialMovementMotion : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private float _multiplier = .25f;

        private float _speed = 0f;

        private MovementDirection _initialDirection = MovementDirection.Right;

        private Rigidbody2D _body = null;

        private MoveMotion _movement = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _body     = GetComponent<Rigidbody2D>();
            _movement = GetComponent<MoveMotion>();
        }

        private void OnEnable()
        {
            _speed            = _movement.Speed;
            _initialDirection = _movement.Direction;

            Debug.Log(_body.velocity.x);
        }

        private void FixedUpdate()
        {
            float direction   = _movement.Movement.x;
            float maxVelocity = _initialDirection == MovementDirection.Left
                ? -(_speed * Time.deltaTime)
                : _speed  * Time.deltaTime;
            float newVelocity = _body.velocity.x + (direction * _multiplier);

            if (
                (_initialDirection == MovementDirection.Right && newVelocity < 0)
                || (_initialDirection == MovementDirection.Left && newVelocity > 0)
            )
                newVelocity = 0;

            if (Mathf.Abs(newVelocity) > Mathf.Abs(maxVelocity))
                newVelocity = maxVelocity;

            _body.velocity = new Vector2(
                newVelocity,
                _body.velocity.y
            );
        }

        # endregion
    }
}
