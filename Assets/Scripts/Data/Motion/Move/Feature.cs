using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Move
{
    /// <summary>
    /// Contains the features data of a move motion component
    /// </summary>
    [System.Serializable]
    public sealed class Feature
    {
        # region Properties

        /// <summary>
        /// Add the ability to freeze the movement. When frozen the velocity
        /// is exaclty the same and don't be touched.
        /// </summary>
        [Tooltip("When move is frozen the velocity is keep untouched")]
        public bool IsFrozen = false;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
