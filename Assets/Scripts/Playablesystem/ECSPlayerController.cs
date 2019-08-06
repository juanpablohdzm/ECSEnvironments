using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSPlayerController : MonoBehaviour
    {
        [Header("Spawn properties")]
        [SerializeField] private GameObject universalAnchor;
        [SerializeField] private GameObject leftAnchor;
        [SerializeField] private GameObject rightAnchor;

        [SerializeField] private GameObject controller;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            OVRPlugin.Handedness handedness = OVRPlugin.GetDominantHand();
            Instantiate(controller, universalAnchor.transform);
            switch (handedness)
            {

                case OVRPlugin.Handedness.LeftHanded:
                    universalAnchor.transform.parent = leftAnchor.transform;
                    break;
                case OVRPlugin.Handedness.RightHanded:
                case OVRPlugin.Handedness.Unsupported:
                    universalAnchor.transform.parent = rightAnchor.transform;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
