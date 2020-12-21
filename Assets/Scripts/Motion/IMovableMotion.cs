using UnityEngine;

namespace Djeg.Prometheus.Motion
{
    /// <summary>
    /// Define the contract of a movable motion.
    /// </summary>
    public interface IMovableMotion
    {
        # region Properties

        /// <summary>
        /// Defineds the speed of a movable motion
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Put here the movable algorithm
        /// </summary>
        MovementAlgorithm Algorithm { get; set; }

        /// <summary>
        /// This is the movement vector wich contains value between
        /// -1 (for left or down) and 1 (for right and up)
        /// </summary>
        Vector2 Movement { get; set; }

        /// <summary>
        /// Contains the direction of the current object.
        /// </summary>
        MovementDirection Direction { get; }

        /// <summary>
        /// Ensure that the object is moving or not
        /// </summary>
        bool IsMoving { get; }

        /// <summary>
        /// Ensure that the object currently looking right
        /// </summary>
        bool IsLoogingRight { get; }

        /// <summary>
        /// Ensure that the object currently looking left
        /// </summary>
        bool IsLookingLeft { get; }

        /// <summary>
        /// Events triggered when the object chnage direction
        /// </summary>
        OnDirectionChangedEvent OnDirectionChanged { get; }

        /// <summary>
        /// Events triggered the object start moving
        /// </summary>
        OnMovementStartEvent OnMovementStart { get; }

        /// <summary>
        /// Events triggered the object stop moving
        /// </summary>
        OnMovementStopEvent OnMovementStop { get; }

        /// <summary>
        /// Events triggered on each update before any deplacement
        /// </summary>
        OnBeforeMovingEvent OnBeforeMoving { get; }

        /// <summary>
        /// Events triggered on each update after any deplacement
        /// </summary>
        OnAfterMovingEvent OnAfterMoving { get; }

        # endregion

        # region Methods
        # endregion
    }
}
