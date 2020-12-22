using UnityEngine;

namespace Djeg.Prometheus.Data.Motion
{
    /// <summary>
    /// Contains the data needed in order to make an object jump.
    /// </summary>
    [System.Serializable]
    public sealed class Jump
    {
        # region Properties

        /// <summary>
        /// The force
        /// </summary>
        [Tooltip("The force sent to the rigidbody in order to make this object jump.")]
        public float force = 16f;

        /// <summary>
        /// Is it currently in the air
        /// </summary>
        [Tooltip("Control if the object is in the air.")]
        public bool isJumping = false;

        /// <summary>
        /// Is it currently falling
        /// </summary>
        [Tooltip("Control if the object is in the air and falling.")]
        public bool isFalling = false;

        /// <summary>
        /// A simple boolean that indicates is the player is requesting a jump
        /// </summary>
        public bool isPressed = false;

        /// <summary>
        /// A simple boolean that indicates if the player is holding the jump button
        /// </summary>
        public bool isHolding = false;

        /// <summary>
        /// Contains the length of the raycast used to detect ground collision
        /// </summary>
        public float raycastLength = .7f;

        /// <summary>
        /// Contains the layer used to detect the ground
        /// </summary>
        public LayerMask whatIsGround = Physics2D.AllLayers;

        /// <summary>
        /// The debug color
        /// </summary>
        public Color debugColor = Color.yellow;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Trigger a raycast and return a boolean, true if the raycast hit
        /// the ground.
        /// </summary>
        public bool IsGrounded(GameObject subject)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                subject.transform.position,
                Vector2.down,
                raycastLength,
                whatIsGround
            );

            return null != hit.collider;
        }

        /// <summary>
        /// Debug using gizmos
        /// </summary>
        public void DrawGizmos(GameObject subject)
        {
            Gizmos.color = debugColor;

            Gizmos.DrawLine(
                subject.transform.position,
                new Vector2(
                    subject.transform.position.x,
                    subject.transform.position.y - raycastLength
                )
            );
        }

        # endregion
    }
}
