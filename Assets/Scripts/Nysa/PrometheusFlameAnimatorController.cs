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
    [RequireComponent(typeof(FollowMotion))]
    [RequireComponent(typeof(Animator))]
    public class PrometheusFlameAnimatorController : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private string _movementParameterName = "Movement";

        [SerializeField]
        private string _rotateParameterName = "Rotate";

        [SerializeField]
        private string _dragParameterName = "Drag";

        [SerializeField]
        private string _dragingParameterName = "Draging";

        [SerializeField]
        private string _undragParameterName = "Undrag";

        private Animator _animator = null;

        private FollowMotion _movement = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _movement = GetComponent<FollowMotion>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _movement.OnAfterMoving.AddListener(HandleMovement);
            _movement.OnDirectionChanged.AddListener(HandleDirectionChanged);

            DragMotion drag = _movement.Subject.GetComponent<DragMotion>();

            drag.OnDrag.AddListener(StartDrag);
            drag.OnDragJump.AddListener(StopDrag);
            drag.OnDragFall.AddListener(StopDrag);
        }

        private void OnDisable()
        {
            _movement.OnAfterMoving.RemoveListener(HandleMovement);
            _movement.OnDirectionChanged.RemoveListener(HandleDirectionChanged);

            DragMotion drag = _movement.Subject.GetComponent<DragMotion>();

            drag.OnDrag.RemoveListener(StartDrag);
            drag.OnDragJump.RemoveListener(StopDrag);
            drag.OnDragFall.RemoveListener(StopDrag);
        }

        private void HandleMovement()
        {
            _animator.SetFloat(_movementParameterName, Mathf.Abs(_movement.Movement.x));
        }

        private void HandleDirectionChanged()
        {
            _animator.SetTrigger(_rotateParameterName);
        }

        private void StartDrag()
        {
            _animator.SetTrigger(_dragParameterName);
            _animator.SetBool(_dragingParameterName, true);
        }

        private void StopDrag()
        {
            _animator.SetTrigger(_undragParameterName);
            _animator.SetBool(_dragingParameterName, false);
        }

        # endregion
    }
}
