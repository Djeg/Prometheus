using UnityEngine;
using UnityEngine.Events;

namespace Djeg.Prometheus.Data.Motion.Jump
{
    /// <summary>
    /// Contains all the jump event
    /// </summary>
    [System.Serializable]
    public sealed class Event
    {
        # region Events

        /// <summary>
        /// Trigger when the object start jumping
        /// </summary>
        [System.Serializable]
        public sealed class OnJumpEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger when the object land
        /// </summary>
        [System.Serializable]
        public sealed class OnLandEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger when the object start falling
        /// </summary>
        [System.Serializable]
        public sealed class OnFallEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger on each fixed update right before any force is applied
        /// </summary>
        [System.Serializable]
        public sealed class OnBeforeJumpingEvent : UnityEvent
        {
        }

        /// <summary>
        /// Trigger on each fixed update right after any force has been applied
        /// </summary>
        [System.Serializable]
        public sealed class OnAfterJumpingEvent : UnityEvent
        {
        }

        # endregion

        # region Properties

        /// <summary>
        /// On jump
        /// </summary>
        public OnJumpEvent OnJump = new OnJumpEvent();

        /// <summary>
        /// On land
        /// </summary>
        public OnLandEvent OnLand = new OnLandEvent();

        /// <summary>
        /// On fall
        /// </summary>
        public OnFallEvent OnFall = new OnFallEvent();

        /// <summary>
        /// Before jumping
        /// </summary>
        public OnBeforeJumpingEvent OnBeforeJumping = new OnBeforeJumpingEvent();

        /// <summary>
        /// After jumping
        /// </summary>
        public OnAfterJumpingEvent OnAfterJumping = new OnAfterJumpingEvent();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods
        # endregion
    }
}
