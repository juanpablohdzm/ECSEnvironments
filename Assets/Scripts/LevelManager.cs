using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class LevelManager : MonoBehaviour, IDeclareReferencedPrefabs 
{
    [Header("Spawn data")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int amount;
    [Space]
    [Header("Play data")]
    [SerializeField] private GameObject goal;
    [SerializeField] private float rotationSpeed = 30.0f;
    [SerializeField] private float moveSpeed = 5.0f;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(spawnPrefab);
    }


    // Start is called before the first frame update
    void Start()
    {
        Entity prefab =  GameObjectConversionUtility.ConvertGameObjectHierarchy(spawnPrefab, World.Active);
        EntityManager eManager = World.Active.EntityManager;


        NativeArray<Entity> objects = new NativeArray<Entity>(amount, Allocator.Temp);
        eManager.Instantiate(prefab, objects);

        for (int i = 0; i < amount; i++)
        {
            float xVal = UnityEngine.Random.Range(-50.0f, 50.0f);
            float zVal = UnityEngine.Random.Range(-50.0f, 50.0f);
            float yVal = UnityEngine.Random.Range(-50.0f, 50.0f);
            eManager.SetComponentData(objects[i], new Translation { Value = new float3(xVal, yVal, zVal) });
            eManager.SetComponentData(objects[i], new Rotation { Value = quaternion.identity });
            eManager.AddComponentData(objects[i], new SwarmRotationData { rotSpeed = rotationSpeed, direction = new float3(0.0f, 0.0f, 1.0f) });
            eManager.AddComponentData(objects[i], new SpotTag { });
        }
        objects.Dispose();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            World.Active.GetExistingSystem<SwarmMoveForwardSystem>().bShouldStop = !World.Active.GetExistingSystem<SwarmMoveForwardSystem>().bShouldStop;

        World.Active.GetExistingSystem<SwarmMoveForwardSystem>().speed = moveSpeed;
        World.Active.GetExistingSystem<SwarmDirectionSystem>().goalPos = new float3(goal.transform.position.x, goal.transform.position.y, goal.transform.position.z);
    }
    

}
