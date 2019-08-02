using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandDependencyManager : MonoBehaviour
{

    [SerializeField] private GameObject LeftHandAnchor;
    [SerializeField] private GameObject RightHandAnchor;

    public static GameObject HandAnchor { get; private set; }
    public static HandDependencyManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            if (Instance != this)
            GameObject.Destroy(this.gameObject);

        OVRPlugin.Handedness dominantHand = OVRPlugin.GetDominantHand();
        switch (dominantHand)
        {
            case OVRPlugin.Handedness.Unsupported:
                throw new NotSupportedException();
            case OVRPlugin.Handedness.LeftHanded:
                HandAnchor = LeftHandAnchor;
                break;
            case OVRPlugin.Handedness.RightHanded:
                HandAnchor = RightHandAnchor;
                break;
            default:
                throw new NotImplementedException();
        }
    }   
}
