using UnityEngine;

namespace Djeg.Prometheus.Data.Physic
{
    /// <summary>
    /// A simple representation of a Vector2 but serializable
    /// </summary>
    [System.Serializable]
    public sealed class Vector2D
    {
        # region Properties

        /// <summary>
        /// The x value
        /// </summary>
        public float x = 0f;

        /// <summary>
        /// The y value
        /// </summary>
        public float y = 0f;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Convert the value into a Vector2
        /// </summary>
        public Vector2 Vector
        {
            get => new Vector2(x, y);
            set
            {
                x = value.x;
                y = value.y;
            }
        }

        # endregion

        # region PublicMethods
        # endregion
    }
}
