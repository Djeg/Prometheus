using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Move
{
    /// <summary>
    /// Contains the parameters needed in order to make an object
    /// move.
    /// </summary>
    [System.Serializable]
    public sealed class Parameter
    {
        # region Properties

        /// <summary>
        /// Contains the movement speed
        /// </summary>
        [Tooltip("Contains the movement speed")]
        public float Speed = 280f;

        /// <summary>
        /// Contains the direction
        /// </summary>
        [Tooltip("Contains the direction between -1 for left and 1 for right.")]
        public float Direction = 0f;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Return the current direction as an absolute value
        /// </summary>
        public float AbsoluteDirection { get => Mathf.Abs(Direction); }

        /// <summary>
        /// Retrieve the current direction as a vector2
        /// </summary>
        public Vector2 DirectionVector { get => new Vector2(Direction, 0); }

        # endregion

        # region PublicMethods
        # endregion
    }
}
