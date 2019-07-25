using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn data")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int amount;
    [SerializeField] private float rotationSpeed = 30.0f;
    [Space]
    [Header("Play data")]
    [SerializeField] private GameObject goal;
    

    // Start is called before the first frame update
    void Start()
    {
        Entity prefab =  GameObjectConversionUtility.ConvertGameObjectHierarchy(spawnPrefab, World.Active);
        EntityManager eManager = World.Active.EntityManager;


        NativeArray<Entity> objects = new NativeArray<Entity>(amount, Allocator.Temp);
        eManager.Instantiate(prefab, objects);
        for (int i = 0; i < amount; i++)
        {
            float xVal = UnityEngine.Random.Range(-10.0f,10.0f);
            float zVal = UnityEngine.Random.Range(-10.0f,10.0f);
            float yVal = UnityEngine.Random.Range(-10.0f, 10.0f);
            eManager.SetComponentData(objects[i], new Translation { Value = new float3(xVal, yVal, zVal) });
            eManager.SetComponentData(objects[i], new Rotation { Value = quaternion.identity });
            eManager.AddComponentData(objects[i], new SwarmRotationData { rotSpeed = rotationSpeed, direction = new float3(0.0f,0.0f,1.0f) }) ;
            eManager.AddComponentData(objects[i], new SpotTag{}) ;

           

        }
        objects.Dispose();

        World.Active.GetExistingSystem<SwarmDirectionSystem>().groupDistance = 20.0f;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            World.Active.GetExistingSystem<MoveForwardSystem>().bShouldStop = !World.Active.GetExistingSystem<MoveForwardSystem>().bShouldStop;

        Vector3 position = goal.transform.position;
        World.Active.GetExistingSystem<SwarmDirectionSystem>().goalPos = new float3(position.x, position.y, position.z);
    }

}
