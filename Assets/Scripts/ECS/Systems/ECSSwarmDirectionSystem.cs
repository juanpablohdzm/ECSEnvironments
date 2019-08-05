using System.Linq;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;


[UpdateBefore(typeof(ECSSwarmRotationSystem))]
public class ECSSwarmDirectionSystem : JobComponentSystem
{
    [ReadOnly] public float3 goalPos;

    [BurstCompile]
    struct SwarmDirectionJob : IJobForEachWithEntity<Translation, ECSSwarmRotationData>
    {
        public float3 goalPos;
        [DeallocateOnJobCompletion][ReadOnly] public NativeArray<Translation> translations;

        public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, ref ECSSwarmRotationData swarmRotationData)
        {
            float3 currentPosition = translation.Value;
            float3 direction = float3.zero;
            float3 otherPosition = float3.zero;

            if (math.length(goalPos - currentPosition) < 2.0f)
                direction = currentPosition - goalPos;
            else
                direction = goalPos - currentPosition;

            for (int i = 0; i < translations.Length; i++)
            {
                otherPosition = translations[i].Value;
                if (math.length(otherPosition-currentPosition) < 3.0f)
                {
                    direction = direction + (currentPosition - otherPosition);
                }
            }                                          
            
            swarmRotationData.direction = math.normalize(direction);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        EntityQuery entityQuery = GetEntityQuery(typeof(Translation));
        NativeArray<Translation> translations = entityQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

        SwarmDirectionJob job = new SwarmDirectionJob
        {
            goalPos = goalPos,
            translations = translations,
        };

        return job.Schedule(this, inputDeps);
    }
}