using System;
using System.Collections;
using UnityEngine;

namespace Djeg.Prometheus.Motion
{
    /**
     * <summary>
     * Turn a sprite around when the MoveMotion is changing direction
     * </summary>
     */
    public class TurnAroundMotion : MonoBehaviour
    {
        # region Properties

        [SerializeField]
        private bool _manual = false;

        private IMovableMotion _movement = null;

        # endregion

        # region PropertyAccessors

        public bool Manual
        {
            get => _manual;
            set => _manual = value;
        }

        # endregion

        # region PublicMethods

        public void TurnAround()
        {
            gameObject.transform.Rotate(0f, 180f, 0f);
        }

        # endregion

        # region PrivateMethods

        private void Awake()
        {
            _movement = GetComponent<IMovableMotion>();

            if (null == _movement)
                throw new Exception($"{gameObject.name} must contains a IMovableMotion component");
        }

        private void OnEnable()
        {
            _movement.OnDirectionChanged.AddListener(GuardTurnAround);
        }

        private void OnDisable()
        {
            _movement.OnDirectionChanged.RemoveListener(GuardTurnAround);
        }

        private void GuardTurnAround()
        {
            if (_manual)
                return;

            TurnAround();
        }

        # endregion
    }
}
