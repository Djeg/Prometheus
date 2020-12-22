using UnityEngine;
using Djeg.Prometheus.Data.Physic;

namespace Djeg.Prometheus.Data.Motion
{
    /// <summary>
    /// Contains the data used to make an object move in the air
    /// </summary>
    [System.Serializable]
    public sealed class SmoothMove
    {
        # region Properties

        /// <summary>
        /// The speed at wich the object can move in the air
        /// </summary>
        [Tooltip("The speed at wich the object goes in the air")]
        public float speed = 280f;

        /// <summary>
        /// The smooth time. Since moving in the air must be a smooth movement.
        /// </summary>
        [Tooltip("The smooth time used to increase and decrease the speed in the air")]
        public float smoothTime = .3f;

        /// <summary>
        /// A reference to a velocity used by the Vector2.SmoothDamp algorithm.
        /// </summary>
        [Tooltip("A reference to a velocity used by the Vector2.SmoothDamp algorithm")]
        public Vector2D velocity = new Vector2D();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
