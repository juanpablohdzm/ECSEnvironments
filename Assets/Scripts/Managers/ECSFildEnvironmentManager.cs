using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace ECSEnvironments.Managers
{
    public class ECSFildEnvironmentManager : ECSLevelManager
    {
        // Update is called once per frame
        void Update()
        {
            World.Active.GetExistingSystem<ECSSwarmDirectionSystem>().goalPos = new float3(goal.transform.position.x, goal.transform.position.y, goal.transform.position.z);
        }
    }
}