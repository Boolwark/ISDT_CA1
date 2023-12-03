namespace Enemy.Boss
{
    using Unity.Burst;
    using Unity.Collections;
    using Unity.Jobs;
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.Jobs;

    [BurstCompile]
    public struct HomingRocketMovementJob : IJobParallelForTransform
    {
        public Vector3 playerPosition;
        public float speed;
        public NativeArray<float> countdowns;
        public float deltaTime;
        public void Execute(int index, TransformAccess transform)
        {
         
            Vector3 direction = (playerPosition - transform.position).normalized;
            transform.position += direction * speed * deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed);

            // Update countdown
            countdowns[index] -= deltaTime;
        }
    }

}