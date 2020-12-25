using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Jump
{
    /// <summary>
    /// Contains the data needed to make an object jump
    /// </summary>
    [System.Serializable]
    public sealed class Parameter
    {
        # region Properties

        /// <summary>
        /// The force
        /// </summary>
        [Tooltip("The jump force")]
        public float Force = 18f;

        /// <summary>
        /// The fall modifier
        /// </summary>
        [Tooltip("The intensity of the fall when not holding the jump")]
        public float FallingModifier = .05f;

        /// <summary>
        /// The raycast length used for ground collision
        /// </summary>
        [Tooltip("The raycast length used for ground collision.")]
        public float RaycastLength = .7f;

        /// <summary>
        /// Define what is the ground using a layer
        /// </summary>
        [Tooltip("The layer used for detecting ground.")]
        public LayerMask WhatIsGround = Physics2D.AllLayers;

        /// <summary>
        /// Is it on the air ?
        /// </summary>
        [Tooltip("Test if the object is on the air")]
        public bool IsOnAir = false;

        /// <summary>
        /// Is it falling ?
        /// </summary>
        [Tooltip("Test if the object is falling")]
        public bool IsFalling = false;

        /// <summary>
        /// Does the jump button is pressed
        /// </summary>
        [Tooltip("Return the state of the jump button pressure")]
        public bool IsRequested = false;

        /// <summary>
        /// Does the jump button is pressed
        /// </summary>
        [Tooltip("Return the state of the jump button hold")]
        public bool IsHolding = false;

        /// <summary>
        /// The debug color
        /// </summary>
        [System.NonSerialized]
        [Tooltip("The color used for the debugging")]
        public Color DebugColor = Color.yellow;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Return the force as a Vector2
        /// </summary>
        public Vector2 ForceVector { get => Vector2.up * Force; }

        # endregion

        # region PublicMethods
        # endregion
    }
}
