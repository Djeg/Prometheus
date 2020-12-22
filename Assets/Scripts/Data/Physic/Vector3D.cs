using UnityEngine;

namespace Djeg.Prometheus.Data.Physic
{
    /// <summary>
    /// A simple serializable representation of a 3D vector.
    /// </summary>
    [System.Serializable]
    public sealed class Vector3D
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

        /// <summary>
        /// The z value
        /// </summary>
        public float z = 0f;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Retrieve and set the 3DVector as a unity Vector3
        /// </summary>
        public Vector3 Vector
        {
            get => new Vector3(x, y, z);

            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        # endregion

        # region PublicMethods
        # endregion
    }
}
