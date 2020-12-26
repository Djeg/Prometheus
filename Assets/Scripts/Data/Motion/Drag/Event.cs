using UnityEngine;
using UnityEngine.Events;

namespace Djeg.Prometheus.Data.Motion.Drag
{
    /// <summary>
    /// Contains all the event dispatched by a drag motion
    /// </summary>
    [System.Serializable]
    public sealed class Event
    {
        # region Events

        /// <summary>
        /// Triggered when a drag start
        /// </summary>
        [System.Serializable]
        public sealed class OnDragStartEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger when the drag end
        /// </summary>
        [System.Serializable]
        public sealed class OnDragEndEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger on drag jump
        /// </summary>
        [System.Serializable]
        public sealed class OnDragJumpEvent : UnityEvent
        {
        }

        /// <summary>
        /// Triggered on drag release
        /// </summary>
        [System.Serializable]
        public sealed class OnDragReleaseEvent : UnityEvent
        {
        }

        /// <summary>
        /// Before any draging is applied before FixedUpdate
        /// </summary>
        [System.Serializable]
        public sealed class OnBeforeDragingEvent : UnityEvent
        {
        }

        /// <summary>
        /// After any draging is applied on FixedUpdate
        /// </summary>
        [System.Serializable]
        public sealed class OnAfterDragingEvent : UnityEvent
        {
        }

        # endregion

        # region Properties

        /// <summary>
        /// On drag start
        /// </summary>
        public OnDragStartEvent OnDragStart = new OnDragStartEvent();

        /// <summary>
        /// On drag end event
        /// </summary>
        public OnDragEndEvent OnDragEnd = new OnDragEndEvent();

        /// <summary>
        /// On drg jump event
        /// </summary>
        public OnDragJumpEvent OnDragJump = new OnDragJumpEvent();

        /// <summary>
        /// on drag release event
        /// </summary>
        public OnDragReleaseEvent OnDragRelease = new OnDragReleaseEvent();

        /// <summary>
        /// On before draging event
        /// </summary>
        public OnBeforeDragingEvent OnBeforeDraging = new OnBeforeDragingEvent();

        /// <summary>
        /// On After draging event
        /// </summary>
        public OnAfterDragingEvent OnAfterDraging = new OnAfterDragingEvent();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods
        # endregion
    }
}
