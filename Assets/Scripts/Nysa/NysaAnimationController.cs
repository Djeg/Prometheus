using System.Collections;
using UnityEngine;
using Djeg.Prometheus.Motion;

namespace Djeg.Prometheus.Nysa
{
    /**
     * <summary>
     * Control the animation of Nysa
     * </summary>
     */
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MoveMotion))]
    [RequireComponent(typeof(JumpMotion))]
    [RequireComponent(typeof(DragMotion))]
    public class NysaAnimationController : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private string _movementParameterName = "Movement";

        [SerializeField]
        private string _jumpingParameterName = "Jumping";

        [SerializeField]
        private string _jumpParameterName = "Jump";

        [SerializeField]
        private string _fallingParameterName = "Falling";

        [SerializeField]
        private string _fallParameterName = "Fall";

        [SerializeField]
        private string _dragParameterName = "Drag";

        [SerializeField]
        private string _dragingParameterName = "Draging";

        private MoveMotion _move = null;

        private JumpMotion _jump = null;

        private DragMotion _drag = null;

        private Animator _animator = null;

        private AerialMovementMotion _aerialMovement = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _move           = GetComponent<MoveMotion>();
            _animator       = GetComponent<Animator>();
            _jump           = GetComponent<JumpMotion>();
            _aerialMovement = GetComponent<AerialMovementMotion>();
            _drag           = GetComponent<DragMotion>();
        }

        private void OnEnable()
        {
            _aerialMovement.enabled = false;
            _move.KeepVelocityOnDisabled = true;

            _move.OnAfterMoving.AddListener(UpdateMovement);

            _jump.OnJump.AddListener(StartJumping);
            _jump.OnFall.AddListener(StartFalling);
            _jump.OnLand.AddListener(StopJumpingAndFalling);

            _drag.OnDrag.AddListener(StartDrag);
            _drag.OnDragJump.AddListener(StopDrag);
            _drag.OnDragFall.AddListener(StopDrag);
        }

        private void OnDisable()
        {
            _move.OnAfterMoving.RemoveListener(UpdateMovement);

            _jump.OnJump.RemoveListener(StartJumping);
            _jump.OnFall.RemoveListener(StartFalling);
            _jump.OnLand.RemoveListener(StopJumpingAndFalling);

            _drag.OnDrag.RemoveListener(StartDrag);
            _drag.OnDragJump.RemoveListener(StopDrag);
            _drag.OnDragFall.RemoveListener(StopDrag);
        }

        private void UpdateMovement()
        {
            _animator.SetFloat(_movementParameterName, Mathf.Abs(_move.Movement.x));
        }

        private void StartJumping()
        {
            _animator.SetTrigger(_jumpParameterName);
            _animator.SetBool(_jumpingParameterName, true);
        }

        private void StartFalling()
        {
            _animator.SetTrigger(_fallParameterName);
            _animator.SetBool(_fallingParameterName, true);
        }

        private void StopJumpingAndFalling()
        {
            _animator.SetBool(_jumpingParameterName, false);
            _animator.SetBool(_fallingParameterName, false);
        }

        private void StartDrag()
        {
            _animator.SetTrigger(_dragParameterName);
            _animator.SetBool(_dragingParameterName, true);
        }

        private void StopDrag()
        {
            _animator.SetBool(_dragingParameterName, false);
        }

        # endregion
    }
}
