using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Linq;

public interface IAnchorDependent
{
    void SetAnchor(GameObject HandAnchor);
}

public class HandDependencyManager : SerializedMonoBehaviour
{

    [SerializeField] private GameObject LeftHandAnchor;
    [SerializeField] private GameObject RightHandAnchor;
    [OdinSerialize,ShowInInspector] private List<IAnchorDependent> dependents = new List<IAnchorDependent>();

    public static GameObject HandAnchor { get; private set; }
    public static HandDependencyManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
                GameObject.Destroy(this.gameObject);
        }

        OVRPlugin.Handedness dominantHand = OVRPlugin.GetDominantHand();
        switch (dominantHand)
        {
            case OVRPlugin.Handedness.Unsupported:
                HandAnchor = RightHandAnchor;
                break;
            case OVRPlugin.Handedness.LeftHanded:
                HandAnchor = LeftHandAnchor;
                break;
            case OVRPlugin.Handedness.RightHanded:
                HandAnchor = RightHandAnchor;
                break;
            default:
                throw new NotImplementedException();
        }

        SetDependencies();
    }  
    
    private void SetDependencies()
    {
        for(int i=0; i< dependents.Count; i++)
        {
            dependents[i].SetAnchor(HandAnchor);
        }
    }
}
