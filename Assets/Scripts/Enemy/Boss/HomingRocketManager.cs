using DefaultNamespace.ObjectPooling;
using Enemy.Boss;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Util;


public class HomingRocketManager : Singleton<HomingRocketManager>
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private int maxRocketCount = 100;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 10f;

    private TransformAccessArray transformArray;
    private NativeArray<float> countdowns;
    private int activeRocketCount = 0;

    void Start()
    {
        player = Camera.main.transform;
        transformArray = new TransformAccessArray(maxRocketCount);
        countdowns = new NativeArray<float>(maxRocketCount, Allocator.Persistent);
    }

    public void FireRocket(Vector3 position, Quaternion rotation)
    {
        if (activeRocketCount < maxRocketCount)
        {
            var rocket = ObjectPoolManager.SpawnObject(rocketPrefab, position, rotation);
            transformArray.Add(rocket.transform);
            countdowns[activeRocketCount] = 5f; // Set countdown for the new rocket
            activeRocketCount++;
        }
        else
        {
            Debug.Log("Maximum rocket count reached");
        }
    }

    void FixedUpdate()
    {
        if (activeRocketCount > 0)
        {
            var job = new HomingRocketMovementJob
            {
                playerPosition = player.position,
                speed = speed,
                countdowns = countdowns,
                deltaTime = Time.deltaTime
            };

            JobHandle handle = job.Schedule(transformArray);
            handle.Complete();
        }
    }

    void OnDestroy()
    {
        transformArray.Dispose();
        countdowns.Dispose();
    }

    // Call this method from rockets when they are no longer active
    public void OnRocketDestroyed()
    {
        if (activeRocketCount > 0)
            activeRocketCount--;
    }
}