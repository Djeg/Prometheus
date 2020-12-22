using UnityEngine;
using Djeg.Prometheus.Controller;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Action.Motion
{
    /// <summary>
    /// Allow an object to turn around when changing direction
    /// </summary>
    public sealed class TurnAround : StateMachineBehaviour
    {
        # region Requirement

        /// <summary>
        /// The TurnAround requirements
        /// </summary>
        public struct Requirement
        {
            public Rigidbody2D body;
        }

        # endregion

        # region Properties

        /// <summary>
        /// The previous x velocity
        /// </summary>
        [SerializeField]
        private float _previousVelocityX = 0f;

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
                body = animator.GetComponent<Rigidbody2D>(),
            };
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Data.body.velocity.x >= -0.1 && Data.body.velocity.x <= 0.1)
                return;

            if (
                (Data.body.velocity.x > 0 && _previousVelocityX < 0)
                || (Data.body.velocity.x < 0 && _previousVelocityX > 0)
            )
                animator.gameObject.transform.Rotate(0f, 180f, 0f);

            _previousVelocityX = Data.body.velocity.x;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
