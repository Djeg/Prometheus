using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Djeg.Prometheus.Motion;

namespace Djeg.Prometheus.Motion.Animation
{
    /**
     * <summary>
     * Turn around an object when the animation exit.
     * </summary>
     */
    public class TurnAroundWhenEndedMotion : StateMachineBehaviour
    {
        # region Properties

        private TurnAroundMotion _turnAround = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _turnAround = animator.gameObject.GetComponent<TurnAroundMotion>();

            if (null == _turnAround)
                throw new Exception($"{animator.gameObject.name} does not contains a TurnAroundMotion component");
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _turnAround.TurnAround();
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
