using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName ="EnvironmentInfoHolder",menuName ="Environment/InfoHolder")]
public class ECSEnvironmentInfoHolderSO : SerializedScriptableObject
{
    [OdinSerialize] private Dictionary<string, ECSEnvironmentInfoSO> environments = new Dictionary<string, ECSEnvironmentInfoSO>();

    public ECSEnvironmentInfoSO GetInfo(string name)
    {
        return environments[name.ToLower()];
    }
}
