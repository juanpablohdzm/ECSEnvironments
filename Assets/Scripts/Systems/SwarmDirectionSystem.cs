using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateBefore(typeof(SwarmRotationSystem))]
public class SwarmDirectionSystem : ComponentSystem
{
    [ReadOnly] public float3 goalPos;
    [ReadOnly] public float groupDistance;
    // 
    //     [BurstCompile]
    //     struct SwarmDirectionJob : IJobForEach<Translation, SwarmRotationData>
    //     {
    //         public float3 goalPos;
    //         public void Execute([ReadOnly] ref Translation translation, ref SwarmRotationData rotationData)
    //         {
    //             float3 direction = goalPos - translation.Value;
    //             direction = math.length(direction) < 2.0f ? math.normalize(translation.Value - goalPos) : math.normalize(direction);
    //            
    // 
    //             rotationData.direction = direction;
    //         }
    //     }

    //     protected override JobHandle OnUpdate(JobHandle inputDeps)
    //     {
    //         SwarmDirectionJob job = new SwarmDirectionJob
    //         {
    //             goalPos = goalPos
    //         };
    // 
    //         return job.Schedule(this, inputDeps);
    //     }
    protected override void OnUpdate()
    {
        Entities.WithAll<SpotTag>().ForEach((Entity entity, ref  Translation translation, ref SwarmRotationData swarmRotation) =>
        {
            float3 currentPosition = translation.Value;

            float3 direction = goalPos - currentPosition;
            float3 avoidDirection = float3.zero;
           
            float distance = 0.0f; 

            Entities.WithAll<SpotTag>().ForEach((Entity otherEntity, ref Translation otherTranslation) =>
            {
                distance = math.distance(currentPosition, otherTranslation.Value);
                if ( distance < 3.0f)
                {                                       
                   avoidDirection = avoidDirection + (currentPosition - otherTranslation.Value);                   
                }
            });

            if (math.length(direction) < 5.0f)
                direction = currentPosition - goalPos;

            direction = direction + avoidDirection;

            swarmRotation.direction = math.normalize(direction);


        });
    }
}