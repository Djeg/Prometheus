using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Move
{
    /// <summary>
    /// Contains the data needed to animate a move
    /// </summary>
    [System.Serializable]
    public sealed class Animation
    {
        # region Properties

        /// <summary>
        /// Contains the float parameter name coresponding to the animator
        /// parameter for the X velocity
        /// </summary>
        [Tooltip("The name of the float parameter used to store the velocity on the X axis")]
        public string FloatParameterName = "Movement";

        /// <summary>
        /// If set to true send the velociy as an absolute value
        /// </summary>
        [Tooltip("Send the velocity as an absolute value to the animator")]
        public bool IsAbsolute = true;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
