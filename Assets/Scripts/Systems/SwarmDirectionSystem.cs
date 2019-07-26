
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;


[UpdateBefore(typeof(SwarmRotationSystem))]
public class SwarmDirectionSystem : JobComponentSystem
{
    [ReadOnly] public float3 goalPos;
    [ReadOnly] public float radius;
    [ReadOnly] public float totalAmount;


    [BurstCompile]
    struct SwarmDirectionJob : IJobForEachWithEntity<Translation, SwarmRotationData>
    {
        public float3 goalPos;
        [ReadOnly] public float time;
        [ReadOnly] public float radius;
        [ReadOnly] public float totalAmount;

        public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, ref SwarmRotationData swarmRotationData)
        {
            float3 spherePosition = float3.zero;

            float longitude = 2 * math.PI *((float)index/totalAmount)* time;
            float latitude = math.acos(2 * ((float)index/totalAmount) - 1) * time;

            spherePosition.x = radius * math.sin(latitude) * math.cos(longitude ) + goalPos.x;
            spherePosition.y = radius * math.cos(latitude) + goalPos.y;
            spherePosition.z = radius * math.sin(latitude) * math.sin(longitude) + goalPos.z;

            swarmRotationData.direction = spherePosition - translation.Value;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        SwarmDirectionJob job = new SwarmDirectionJob
        {
            time = Time.time,
            goalPos = goalPos,
            radius = radius,
            totalAmount = totalAmount,
        };

        return job.Schedule(this, inputDeps);
    }
}