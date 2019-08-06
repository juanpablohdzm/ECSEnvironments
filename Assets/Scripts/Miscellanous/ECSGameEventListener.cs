using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECSEnvironments.ScriptableObjects;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSGameEventListener : ScriptableObject
    {
        [SerializeField]
        private ECSGameEvent gameEvent;
        [SerializeField]
        private UnityEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}
