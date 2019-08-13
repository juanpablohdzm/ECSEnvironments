using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSEnvironments.PlayableSystems;
using Sirenix.OdinInspector;

namespace ECSEnvironments.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ECSGameEvent_", menuName = "Environment/GameEvent")]
    public class ECSGameEvent : ScriptableObject
    {
        private List<ECSGameEventListener> listeners = new List<ECSGameEventListener>();

        [Button]
        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
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
