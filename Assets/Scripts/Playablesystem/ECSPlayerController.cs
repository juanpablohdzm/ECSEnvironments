using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSEnvironments.Interfaces;

namespace ECSEnvironments.PlayableSystems
{
    public class ECSPlayerController : MonoBehaviour
    {
        [Header("Spawn properties")]
        [SerializeField] private GameObject universalAnchor;
        [SerializeField] private GameObject leftAnchor;
        [SerializeField] private GameObject rightAnchor;

        [SerializeField] private GameObject controller;
        [Header("Interaction")]
        [SerializeField] private LayerMask rayMask;

        private ECSIRayInteractable lastHit;

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

        private void Update()
        {
            if(Time.frameCount % 3 == 0)
            {
                RaycastHit raycastHit;
                if(Physics.Raycast(universalAnchor.transform.position,universalAnchor.transform.forward,out raycastHit,20.0f,rayMask))
                {
                    ECSIRayInteractable hit = raycastHit.transform.GetComponent<ECSIRayInteractable>();
                    if(lastHit != hit)
                    {
                        if (lastHit != null)
                            lastHit.OnRayExit();

                        lastHit = hit;
                        lastHit.OnRayEnter();
                    }

                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                        lastHit.OnRayTrigger();

                }
                else
                {
                    if (lastHit != null)
                        lastHit.OnRayExit();
                    lastHit = null;
                }
            }
        }
    }
}
