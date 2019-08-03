using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class UnderwaterEnvironmentManager : LevelManager
{    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        World.Active.GetExistingSystem<SwarmDirectionSystem>().goalPos = new float3(goal.transform.position.x, goal.transform.position.y, goal.transform.position.z);
    }  
}
