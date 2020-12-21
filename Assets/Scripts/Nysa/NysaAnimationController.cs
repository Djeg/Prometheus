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

        private MoveMotion _move = null;

        private JumpMotion _jump = null;

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
        }

        private void OnEnable()
        {
            _aerialMovement.enabled = false;

            _move.OnAfterMoving.AddListener(UpdateMovement);
            _jump.OnJump.AddListener(StartJumping);
            _jump.OnFall.AddListener(StartFalling);
            _jump.OnLand.AddListener(StopJumpingAndFalling);
        }

        private void OnDisable()
        {
            _move.OnAfterMoving.RemoveListener(UpdateMovement);
        }

        private void UpdateMovement()
        {
            _animator.SetFloat(_movementParameterName, Mathf.Abs(_move.Movement.x));
        }

        private void StartJumping()
        {
            _move.enabled = false;
            _aerialMovement.enabled = true;
            _animator.SetTrigger(_jumpParameterName);
            _animator.SetBool(_jumpingParameterName, true);
        }

        private void StartFalling()
        {
            _animator.SetBool(_fallingParameterName, true);
        }

        private void StopJumpingAndFalling()
        {
            _move.enabled = true;
            _aerialMovement.enabled = false;
            _animator.SetBool(_jumpingParameterName, false);
            _animator.SetBool(_fallingParameterName, false);
        }

        # endregion
    }
}
