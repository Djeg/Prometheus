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
    public class AerialMovementMotion : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private float _force = .7f;

        [SerializeField]
        private float _maxVelocity = .8f;

        private Rigidbody2D _body = null;

        private float _direction = 0f;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(_direction) < .5f)
                return;

            _body.AddForce(new Vector2(
                _direction * _force,
                0
            ), ForceMode2D.Impulse);

            if (Mathf.Abs(_body.velocity.x) > _maxVelocity)
                _body.velocity = new Vector2(
                    _body.velocity.x < 0 ? -_maxVelocity : _maxVelocity,
                    _body.velocity.y
                );
        }

        private void OnMove(InputValue input)
        {
            _direction = input.Get<Vector2>().x;
        }

        # endregion
    }
}
