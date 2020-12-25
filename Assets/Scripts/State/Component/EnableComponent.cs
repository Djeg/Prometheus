using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Djeg.Prometheus.State.Component
{
    /// <summary>
    /// Enable a given component when started
    /// </summary>
    public class EnableComponent : StateMachineBehaviour
    {
        # region Properties

        /// <summary>
        /// The component name to enable
        /// </summary>
        public string ComponentName = "";

        /// <summary>
        /// Does this component needs to be disable when exiting
        /// </summary>
        public bool DisableOnExit = true;

        /// <summary>
        /// A reference to the component
        /// </summary>
        private Behaviour _component = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Enable the given component
        /// </summary>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _component = (Behaviour)animator.GetComponent(ComponentName);

            _component.enabled = true;
        }

        /// <summary>
        /// Disable the given component
        /// </summary>
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!DisableOnExit)
                return;

            _component.enabled = false;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
