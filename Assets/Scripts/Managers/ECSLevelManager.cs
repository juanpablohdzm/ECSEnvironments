using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace ECSEnvironments.Managers
{
    public class ECSLevelManager : MonoBehaviour, IDeclareReferencedPrefabs
    {
        [Header("Spawn data")]
        [SerializeField] protected GameObject spawnPrefab;
        [SerializeField] protected int amount = 10;
        [Space]
        [Header("Play data")]
        [SerializeField] protected GameObject goal;
        [SerializeField] protected float rotationSpeed = 30.0f;
        [SerializeField] protected float moveSpeed = 5.0f;


        public GameObject Goal { get { return goal; } set { goal = value; } }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(spawnPrefab);
        }


        protected virtual void SpawnEntities(int amount)
        {
            Entity prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(spawnPrefab, World.Active);

            EntityManager eManager = World.Active.EntityManager;
            NativeArray<Entity> objects = new NativeArray<Entity>(amount, Allocator.Temp);
            eManager.Instantiate(prefab, objects);

            for (int i = 0; i < amount; i++)
            {
                float randomSpeed = UnityEngine.Random.Range(rotationSpeed - 5.0f, rotationSpeed + 10.0f);
                float xVal = UnityEngine.Random.Range(-50.0f, 50.0f);
                float zVal = UnityEngine.Random.Range(-50.0f, 50.0f);
                float yVal = UnityEngine.Random.Range(10.0f, 50.0f);
                eManager.SetComponentData(objects[i], new Translation { Value = new float3(xVal, yVal, zVal) });
                eManager.SetComponentData(objects[i], new Rotation { Value = quaternion.identity });
                eManager.AddComponentData(objects[i], new ECSSwarmRotationData { rotSpeed = randomSpeed, direction = new float3(0.0f, 0.0f, 1.0f) });
                eManager.AddComponentData(objects[i], new SpotTag { });
            }
            objects.Dispose();
        }

        private void Awake()
        {
            StartCoroutine("SpawnEntitiesCoroutine");
            World.Active.GetExistingSystem<ECSSwarmMoveForwardSystem>().speed = moveSpeed;
        }

        private IEnumerator SpawnEntitiesCoroutine()
        {
            WaitForSeconds seconds = new WaitForSeconds(0.2f);
            int totalEntities = 0;
            while (totalEntities < amount)
            {
                SpawnEntities(10);
                totalEntities += 10;
                yield return seconds;
            }

        }

    }
}
