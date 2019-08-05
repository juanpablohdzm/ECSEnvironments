using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Environment_",menuName ="Environment/Data")]
public class ECSEnvironmentInfoSO : ScriptableObject
{
    [OdinSerialize] public SceneField environmentScene;
    [OdinSerialize] public Color cameraBackgroundColor;
    [OdinSerialize] public AudioClip environmentSound;
}
