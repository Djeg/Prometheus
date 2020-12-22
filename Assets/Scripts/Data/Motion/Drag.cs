using UnityEngine;

namespace Djeg.Prometheus.Data.Motion
{
    /// <summary>
    /// Contains the data used in order to make a game object drag on wall
    /// </summary>
    [System.Serializable]
    public sealed class Drag
    {
        # region Properties

        /// <summary>
        /// Contains the maximum Y velocity when draging
        /// </summary>
        [Tooltip("Contains the maximum speed when a game object is draging and falling")]
        public float maxVelocity = .1f;

        /// <summary>
        /// Test if this game object is actually draging
        /// </summary>
        [Tooltip("Test if this object is actually draging")]
        public bool isDraging = false;

        /// <summary>
        /// Test if this game object is actually colliding with a wall
        /// </summary>
        [Tooltip("Test if this object is actually colliding with wall")]
        public bool isColliding = false;

        /// <summary>
        /// Contains the object used to detect top wall collision
        /// </summary>
        [Tooltip("The object used to detect top wall colision")]
        public GameObject topCollider = null;

        /// <summary>
        /// Contains the object used to detect bottom collision
        /// </summary>
        [Tooltip("The object used to detect bottom wall colision")]
        public GameObject bottomCollider = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion
    }
}
