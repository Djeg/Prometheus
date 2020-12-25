using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Data = Djeg.Prometheus.Data;

namespace Djeg.Prometheus.Component.Motion
{
    /// <summary>
    /// Add the ability of an object to turn around
    /// </summary>
    public class TurnAround : MonoBehaviour
    {
        # region PublicAttributes

        /// <summary>
        /// The EVent
        /// </summary>
        public Data.Motion.TurnAround.Event Event = new Data.Motion.TurnAround.Event();

        # endregion

        # region PrivateAttributes

        /// <summary>
        /// The previous X position
        /// </summary>
        private float _previousPosition = 0f;

        /// <summary>
        /// The difference between the previous and current position
        /// </summary>
        private float _difference = 0f;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// When enabled set the previous position
        /// </summary>
        public void OnEnable()
        {
            _previousPosition = transform.position.x;
            _difference = 0f;
        }

        /// <summary>
        /// Detect of turning around
        /// </summary>
        private void FixedUpdate()
        {
            float difference = transform.position.x - _previousPosition;

            if (difference >= -0.05f && difference <= 0.05f)
                return;

            if (
                (difference < 0 && _difference > 0)
                || (difference > 0 && _difference < 0)
            ) {
                Event.OnTurnAround.Invoke();

                transform.Rotate(0f, 180f, 0f);
            }

            _difference = difference;
            _previousPosition = transform.position.x;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
