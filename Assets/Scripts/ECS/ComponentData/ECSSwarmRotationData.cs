using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[SerializeField]
public struct ECSSwarmRotationData : IComponentData
{
    public float rotSpeed;
    public float3 direction;
}
