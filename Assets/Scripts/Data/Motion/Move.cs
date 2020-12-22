using UnityEngine;

namespace Djeg.Prometheus.Data.Motion
{
    /// <summary>
    /// Contains all the data needed in order to make a game object move.
    /// </summary>
    [System.Serializable]
    public sealed class Move
    {
        # region Properties

        /// <summary>
        /// The movement speed
        /// </summary>
        [Tooltip("Contains the movement speed")]
        public float speed = 280f;

        /// <summary>
        /// The movement direction
        /// </summary>
        [Tooltip("Contains the current direction between -1 for left and 1 for right.")]
        public float direction = 0f;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Return the absolute direction
        /// </summary>
        public float AbsoluteDirection { get => Mathf.Abs(direction); }

        # endregion

        # region PublicMethods
        # endregion
    }
}
