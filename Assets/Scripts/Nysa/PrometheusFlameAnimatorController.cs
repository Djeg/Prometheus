using System.Collections;
using UnityEngine;
using Djeg.Prometheus.Motion;

namespace Djeg.Prometheus.Nysa
{
    /**
     * <summary>
     * Control the prometheus flame animator
     * </summary>
     */
    [RequireComponent(typeof(IMovableMotion))]
    [RequireComponent(typeof(Animator))]
    public class PrometheusFlameAnimatorController : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private string _movementParameterName = "Movement";

        [SerializeField]
        private string _rotateParameterName = "Rotate";

        private Animator _animator = null;

        private IMovableMotion _movement = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _movement = GetComponent<IMovableMotion>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _movement.OnAfterMoving.AddListener(HandleMovement);
            _movement.OnDirectionChanged.AddListener(HandleDirectionChanged);
        }

        private void OnDisable()
        {
            _movement.OnAfterMoving.RemoveListener(HandleMovement);
        }

        private void HandleMovement()
        {
            _animator.SetFloat(_movementParameterName, Mathf.Abs(_movement.Movement.x));
        }

        private void HandleDirectionChanged()
        {
            _animator.SetTrigger(_rotateParameterName);
        }

        # endregion
    }
}
