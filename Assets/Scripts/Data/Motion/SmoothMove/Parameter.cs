using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.SmoothMove
{
    /// <summary>
    /// The parameter of a smooth move
    /// </summary>
    [System.Serializable]
    public sealed class Parameter
    {
        # region Properties

        /// <summary>
        /// The smooth move speed
        /// </summary>
        [Tooltip("The speed of the movement")]
        public float Speed = 280f;

        /// <summary>
        /// The smooth move time
        /// </summary>
        [Tooltip("The time of smoothing")]
        public float SmoothTime = 0.5f;

        /// <summary>
        /// The direction
        /// </summary>
        [Tooltip("The direction of the movement")]
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
