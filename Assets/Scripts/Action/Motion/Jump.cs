using UnityEngine;
using Djeg.Prometheus.Controller;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Action.Motion
{
    /// <summary>
    /// Allow an object to jump
    /// </summary>
    public sealed class Jump : StateMachineBehaviour
    {
        # region Requirement

        /// <summary>
        /// The Jump requirements
        /// </summary>
        public struct Requirement
        {
            public Data.Motion.Jump jump;
            public Rigidbody2D body;
        }

        # endregion

        # region Properties

        /// <summary>
        /// The jump trigger name
        /// </summary>
        [SerializeField]
        private string _jumpTriggerName = "Jump";

        /// <summary>
        /// The jump boolean name
        /// </summary>
        [SerializeField]
        private string _jumpingBoolName = "Jumping";

        /// <summary>
        /// The falling boolean name
        /// </summary>
        [SerializeField]
        private string _fallingBoolName = "Falling";

        /// <summary>
        /// The fall trigger name
        /// </summary>
        [SerializeField]
        private string _fallTriggerName = "Fall";

        /// <summary>
        /// The previous velocity on the Y axis
        /// </summary>
        private float _previousVelocityY = 0f;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// The data requirement
        /// </summary>
        public Requirement Data { get; private set; }

        # endregion

        # region PublicMethods

        /// <summary>
        /// Initialize the data requirements
        /// </summary>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Data = new Requirement {
                jump = animator.GetComponent<BaseController>().GetData<Data.Motion.Jump>(),
                body = animator.GetComponent<Rigidbody2D>()
            };

            _previousVelocityY = Data.body.velocity.y;
            
            HandleJumpAndFall(animator);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleJumpAndFall(animator);
        }

        private void HandleJumpAndFall(Animator animator)
        {
            bool isGrounded = Data.jump.IsGrounded(animator.gameObject);

            if (Data.jump.isPressed && isGrounded)
            {
                Data.jump.isPressed = false;
                Data.body.AddForce(new Vector2(0, Data.jump.force), ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
            }

            animator.SetFloat("VerticalVelocity", Data.body.velocity.y);
            animator.SetBool("IsGrounded", isGrounded);

            if (_previousVelocityY >= 0 && Data.body.velocity.y < 0)
                animator.SetTrigger("Fall");

            _previousVelocityY = Data.body.velocity.y;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
