using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;


public class RotationSystem : JobComponentSystem
{
    [BurstCompile]
    struct RotationJob : IJobForEach<Rotation, RotationData>
    {
        public float dt;

        public void Execute(ref Rotation rot, [ReadOnly] ref RotationData rotData)
        {
            rot.Value = math.mul(math.normalize(rot.Value), quaternion.AxisAngle(math.up(), math.radians(rotData.rotSpeed * dt)));
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        RotationJob job = new RotationJob
        {
            dt = Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}


