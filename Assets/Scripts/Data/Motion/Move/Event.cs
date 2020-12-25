using UnityEngine;
using UnityEngine.Events;

namespace Djeg.Prometheus.Data.Motion.Move
{
    /// <summary>
    /// Contains the event containing in a move component
    /// </summary>
    [System.Serializable]
    public sealed class Event
    {
        # region Events

        /// <summary>
        /// Dispatched when the object start moving
        /// </summary>
        [System.Serializable]
        public sealed class OnMoveEvent : UnityEvent
        {
        }

        /// <summary>
        /// Dispatched when the object stop moving
        /// </summary>
        [System.Serializable]
        public sealed class OnStopEvent : UnityEvent
        {
        }

        /// <summary>
        /// Dispatched right before the movement is applying it's velocity
        /// </summary>
        [System.Serializable]
        public sealed class OnBeforeMovingEvent : UnityEvent
        {
        }

        /// <summary>
        /// Dispatched right after the movement is applying it's velocity
        /// </summary>
        [System.Serializable]
        public sealed class OnAfterMovingEvent : UnityEvent
        {
        }

        # endregion

        # region Properties

        /// <summary>
        /// When the object start moving
        /// </summary>
        public OnMoveEvent OnMove = new OnMoveEvent();

        /// <summary>
        /// When the object stop moving
        /// </summary>
        public OnStopEvent OnStop = new OnStopEvent();

        /// <summary>
        /// Right before applying the move velocity
        /// </summary>
        public OnBeforeMovingEvent OnBeforeMoving = new OnBeforeMovingEvent();

        /// <summary>
        /// Right after applying the move velocity
        /// </summary>
        public OnAfterMovingEvent OnAfterMoving = new OnAfterMovingEvent();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
