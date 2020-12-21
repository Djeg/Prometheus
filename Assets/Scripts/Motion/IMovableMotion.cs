using UnityEngine;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Define the contract of a movable motion.
     * </summary>
     */
    public interface IMovableMotion
    {
        # region Properties

        float Speed { get; set; }

        MovementAlgorithm Algorithm { get; set; }

        Vector2 Movement { get; set; }

        MovementDirection Direction { get; }

        bool IsMoving { get; }

        bool IsLoogingRight { get; }

        bool IsLookingLeft { get; }

        OnDirectionChangedEvent OnDirectionChanged { get; }

        OnMovementStartEvent OnMovementStart { get; }

        OnMovementStopEvent OnMovementStop { get; }

        OnBeforeMovingEvent OnBeforeMoving { get; }

        OnAfterMovingEvent OnAfterMoving { get; }

        # endregion

        # region Methods
        # endregion
    }
}
