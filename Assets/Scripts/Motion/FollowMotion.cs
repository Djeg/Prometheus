using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Follow a game object
     * </summary>
     */
    public class FollowMotion : MonoBehaviour, IMovableMotion
    {
        # region Properties

        [SerializeField]
        [Header("Parameters")]
        private GameObject _subject = null;

        [Space(10)]

        [SerializeField]
        [Header("Events")]
        private OnDirectionChangedEvent _onDirectionChanged = new OnDirectionChangedEvent();

        [SerializeField]
        private OnMovementStartEvent _onMovementStart = new OnMovementStartEvent();

        [SerializeField]
        private OnMovementStopEvent _onMovementStop = new OnMovementStopEvent();

        [SerializeField]
        private OnBeforeMovingEvent _onBeforeMoving = new OnBeforeMovingEvent();

        [SerializeField]
        private OnAfterMovingEvent _onAfterMoving = new OnAfterMovingEvent();

        private IMovableMotion _subjectMovement = null;

        # endregion

        # region PropertyAccessors

        public GameObject Subject { get => _subject; }

        public float Speed
        {
            get => _subjectMovement.Speed;
            set => _subjectMovement.Speed = value;
        }

        public MovementAlgorithm Algorithm
        {
            get => _subjectMovement.Algorithm;
            set => _subjectMovement.Algorithm = value;
        }

        public Vector2 Movement
        {
            get => _subjectMovement.Movement;
            set => _subjectMovement.Movement = value;
        }

        public MovementDirection Direction { get => _subjectMovement.Direction; }

        public bool IsMoving { get => _subjectMovement.IsMoving; }

        public bool IsLoogingRight { get => _subjectMovement.IsLoogingRight; }

        public bool IsLookingLeft { get => _subjectMovement.IsLookingLeft; }

        public OnDirectionChangedEvent OnDirectionChanged { get => _onDirectionChanged; }

        public OnMovementStartEvent OnMovementStart { get => _onMovementStart; }

        public OnMovementStopEvent OnMovementStop { get => _onMovementStop; }

        public OnBeforeMovingEvent OnBeforeMoving { get => _onBeforeMoving; }

        public OnAfterMovingEvent OnAfterMoving { get => _onAfterMoving; }

        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _subjectMovement = _subject.GetComponent<IMovableMotion>();

            if (null == _subjectMovement)
                throw new Exception($"{_subject.name} does not contains a IMovableMotion component");
        }

        private void OnEnable()
        {
            gameObject.transform.position = _subject.gameObject.transform.position;

            _subjectMovement.OnDirectionChanged.AddListener(TriggerDirectionChanged);
            _subjectMovement.OnMovementStart.AddListener(TriggerMovementStart);
            _subjectMovement.OnMovementStop.AddListener(TriggerMovementStop);
            _subjectMovement.OnBeforeMoving.AddListener(TriggerBeforeMoving);
            _subjectMovement.OnAfterMoving.AddListener(TriggerAfterMoving);
        }

        private void OnDisable()
        {
            _subjectMovement.OnDirectionChanged.RemoveListener(TriggerDirectionChanged);
            _subjectMovement.OnMovementStart.RemoveListener(TriggerMovementStart);
            _subjectMovement.OnMovementStop.RemoveListener(TriggerMovementStop);
            _subjectMovement.OnBeforeMoving.RemoveListener(TriggerBeforeMoving);
            _subjectMovement.OnAfterMoving.RemoveListener(TriggerAfterMoving);
        }

        private void FixedUpdate()
        {
            gameObject.transform.position = _subject.gameObject.transform.position;
        }

        private void TriggerDirectionChanged() => OnDirectionChanged.Invoke();
        private void TriggerMovementStart() => OnMovementStart.Invoke();
        private void TriggerMovementStop() => OnMovementStop.Invoke();
        private void TriggerBeforeMoving() => OnBeforeMoving.Invoke();
        private void TriggerAfterMoving() => OnAfterMoving.Invoke();

        # endregion
    }
}
