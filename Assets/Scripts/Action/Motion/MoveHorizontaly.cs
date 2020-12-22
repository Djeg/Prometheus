using UnityEngine;
using Djeg.Prometheus.Controller;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Action.Motion
{
    /// <summary>
    /// Move an object horizontaly
    /// </summary>
    public sealed class MoveHorizontaly : StateMachineBehaviour
    {
        # region Requirement

        /// <summary>
        /// The MoveHorizontaly requirements
        /// </summary>
        public struct Requirement
        {
            public Data.Motion.Move move;
            public Rigidbody2D body;
        }

        # endregion

        # region Properties

        /// <summary>
        /// The movement float parameter name
        /// </summary>
        [SerializeField]
        private string _movementFloatParameter = "Movement";

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
                move = animator.GetComponent<BaseController>().GetData<Data.Motion.Move>(),
                body = animator.GetComponent<Rigidbody2D>()
            };
        }

        /// <summary>
        /// Move using speed, direction and the body velocity
        /// </summary>
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Data.body.velocity = new Vector2(
                Data.move.speed * Data.move.direction * Time.deltaTime,
                Data.body.velocity.y
            );

            animator.SetFloat(_movementFloatParameter, Data.move.AbsoluteDirection);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
