using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;


public class SwarmRotationSystem : JobComponentSystem
{
    

    [BurstCompile]
    struct SwarmRotationJob : IJobForEach<Rotation, SwarmRotationData>
    {
        public float dt;

        public void Execute(ref Rotation rot, [ReadOnly] ref SwarmRotationData rotData)
        {           
            rot.Value = math.slerp(rot.Value, quaternion.LookRotation(rotData.direction, math.up()), math.radians(dt * rotData.rotSpeed));
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        SwarmRotationJob job = new SwarmRotationJob
        {
            dt = Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}


