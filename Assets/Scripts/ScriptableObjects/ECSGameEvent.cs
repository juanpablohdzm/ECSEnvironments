using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSEnvironments.PlayableSystems;

namespace ECSEnvironments.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ECSGameEvent_", menuName = "Environments/GameEvent")]
    public class ECSGameEvent : ScriptableObject
    {
        private List<ECSGameEventListener> listeners = new List<ECSGameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(ECSGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ECSGameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
