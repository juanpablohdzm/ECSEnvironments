using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateAfter(typeof(RotationSystem))]
public class MoveForwardSystem : JobComponentSystem
{
    public bool bShouldStop = false;

    [BurstCompile]
    struct MoveForwardJob : IJobForEach<Translation, Rotation>
    {
        public float dt;
        public float speed;
        public void Execute(ref Translation translation,[ReadOnly] ref Rotation rotation)
        {
            float3 forward = math.rotate(rotation.Value, new float3(0.0f, 0.0f, 1.0f));
            forward = forward * speed * dt + translation.Value;
            translation.Value = forward;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (bShouldStop) return inputDeps;

        MoveForwardJob job = new MoveForwardJob
        {
            dt = Time.deltaTime,
            speed = 5.0f
        };

        return job.Schedule(this, inputDeps);
    }
}



