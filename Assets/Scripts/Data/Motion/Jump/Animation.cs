using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Jump
{
    /// <summary>
    /// Contains the data needed to make a jump animation
    /// </summary>
    [System.Serializable]
    public sealed class Animation
    {
        # region Properties

        /// <summary>
        /// The velocity parameter name
        /// </summary>
        [Header("Parameter name")]
        public string velocityFloatName = "VerticalMovement";

        /// <summary>
        /// The jump trigger name
        /// </summary>
        public string JumpTriggerName = "Jump";

        /// <summary>
        /// The on the air boolean name
        /// </summary>
        public string IsOnTheAirBoolName = "OnTheAir";

        /// <summary>
        /// The fall trigger name
        /// </summary>
        public string FallTriggerName = "Fall";

        /// <summary>
        /// The falling boolean name
        /// </summary>
        public string FallingBoolName = "Falling";

        /// <summary>
        /// Synchronize the velocity with the aimator
        /// </summary>
        [Space(20)]
        [Header("Parameter synchronization")]
        public bool SyncVelocity = true;

        /// <summary>
        /// Does the jump must trigger the jump
        /// </summary>
        public bool TriggerJump = true;

        /// <summary>
        /// Does the jump must trigger the fall
        /// </summary>
        public bool TriggerFall = true;

        /// <summary>
        /// Check in on the air boolean
        /// </summary>
        public bool CheckIsOnTheAir = true;

        /// <summary>
        /// Check the falling boolean
        /// </summary>
        public bool CheckFalling = true;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
