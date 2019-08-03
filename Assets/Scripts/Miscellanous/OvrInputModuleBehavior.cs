using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OvrInputModuleBehavior : MonoBehaviour, IAnchorDependent
{
    [SerializeField] private OVRInputModule inputModule;

    public void SetAnchor(GameObject HandAnchor)
    {
        inputModule.rayTransform = HandAnchor.transform;
    }
   
}
