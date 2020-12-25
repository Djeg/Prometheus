using UnityEngine;
using UnityEngine.Events;

namespace Djeg.Prometheus.Data.Motion.TurnAround
{
    /// <summary>
    /// Contains the turn around events
    /// </summary>
    [System.Serializable]
    public sealed class Event
    {
        # region Events

        /// <summary>
        /// Trigger when turning around
        /// </summary>
        [System.Serializable]
        public sealed class OnTurnAroundEvent : UnityEvent
        {
        }

        # endregion

        # region Properties

        /// <summary>
        /// The turn around event
        /// </summary>
        public OnTurnAroundEvent OnTurnAround = new OnTurnAroundEvent();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods
        # endregion
    }
}
