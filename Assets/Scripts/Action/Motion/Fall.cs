using UnityEngine;
using Djeg.Prometheus.Controller;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Action.Motion
{
    /// <summary>
    /// Control the ability of an object to fall
    /// </summary>
    public sealed class Fall : StateMachineBehaviour
    {
        # region Requirement

        /// <summary>
        /// The Fall requirements
        /// </summary>
        public struct Requirement
        {
            public Data.Motion.Jump jump;
        }

        # endregion

        # region Properties
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
                
            };
        }

        

        # endregion

        # region PrivateMethods
        # endregion
    }
}
