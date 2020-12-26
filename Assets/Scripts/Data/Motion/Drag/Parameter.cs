using UnityEngine;

namespace Djeg.Prometheus.Data.Motion.Drag
{
    /// <summary>
    /// The parameters used in order to drag an object on wall
    /// </summary>
    [System.Serializable]
    public sealed class Parameter
    {
        # region Properties

        /// <summary>
        /// The drag intensity
        /// </summary>
        [Tooltip("The drag intensity")]
        public float Intensity = 0.1f;

        /// <summary>
        /// The drag force on the x Axis
        /// </summary>
        public float DragJumpForceX = 10f;

        /// <summary>
        /// The drag force on the y axis
        /// </summary>
        public float DragJumpForceY = 18f;

        /// <summary>
        /// The drag force on the x Axis
        /// </summary>
        public float DragReleaseForceX = 0f;

        /// <summary>
        /// The drag force on the y axis
        /// </summary>
        public float DragReleaseForceY = 5f;

        /// <summary>
        /// Object used to detect up collision
        /// </summary>
        [Tooltip("The game object used to detect up collision")]
        public GameObject DragUpCollider = null;

        /// <summary>
        /// Object used to detect down collision
        /// </summary>
        [Tooltip("The game object used to detect down collision")]
        public GameObject DragDownCollider = null;

        /// <summary>
        /// Layer used to detect wall collision
        /// </summary>
        [Tooltip("The layer used for wall")]
        public LayerMask WhatIsWall = Physics2D.AllLayers;

        /// <summary>
        /// The direction given when dragging
        /// </summary>
        [Tooltip("The direction given by a source (input, IA)")]
        public float Direction = 0f;

        /// <summary>
        /// Contains the draging state
        /// </summary>
        public bool IsDraging = false;

        /// <summary>
        /// Does the jump button is pressed
        /// </summary>
        [Tooltip("Does the source is requesting a jump")]
        public bool IsRequestingJump = false;

        /// <summary>
        /// Does this drag is requesting a fall
        /// </summary>
        public bool IsRequestingFall = false;

        /// <summary>
        /// The debug color used by gizmos to draw the drag line
        /// </summary>
        public Color DebugColor = Color.cyan;

        # endregion

        # region PropertyAccessors

        /// <summary>
        /// Return the distance between the up and down collider
        /// </summary>
        public float UpAndDownDistance
        { 
            get => DragUpCollider.transform.position.y - DragDownCollider.transform.position.y;
        }

        # endregion

        # region PublicMethods
        # endregion
    }
}
