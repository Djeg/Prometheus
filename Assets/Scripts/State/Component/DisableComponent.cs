using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Djeg.Prometheus.State.Component
{
    /// <summary>
    /// Disable a component when entering the state
    /// </summary>
    public class DisableComponent : StateMachineBehaviour
    {
        # region Properties

        /// <summary>
        /// The component name
        /// </summary>
        public string ComponentName = "";

        /// <summary>
        /// Does is need to enable when exiting?
        /// </summary>
        public bool EnableOnExit = true;

        /// <summary>
        /// A reference to the component
        /// </summary>
        private Behaviour _component = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Disable the given component
        /// </summary>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _component = (Behaviour)animator.GetComponent(ComponentName);

            _component.enabled = false;
        }

        /// <summary>
        /// Enable the component when exiting
        /// </summary>
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!EnableOnExit)
                return;

            _component.enabled = true;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
