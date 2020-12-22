using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Controller
{
    /// <summary>
    /// Glue the data and the input required for nysa
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class NysaController : BaseController
    {
        # region Properties

        /// <summary>
        /// The move data
        /// </summary>
        public Data.Motion.Move move = new Data.Motion.Move();

        /// <summary>
        /// The jump data
        /// </summary>
        public Data.Motion.Jump jump = new Data.Motion.Jump();

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Set the movement direction
        /// </summary>
        public void UpdateMoveDirection(InputAction.CallbackContext input)
        {
            move.direction = input.ReadValue<Vector2>().x;
        }

        /// <summary>
        /// Set the jump pressed and holding boolean
        /// </summary>
        public void UpdateJump(InputAction.CallbackContext input)
        {
            jump.isPressed = input.performed ? true : jump.isPressed;
            jump.isHolding = input.performed ? true : jump.isHolding;

            jump.isPressed = input.canceled ? false : jump.isPressed;
            jump.isHolding = input.canceled ? false : jump.isHolding;
        }

        /// <summary>
        /// Debug the data
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            jump.DrawGizmos(gameObject);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
