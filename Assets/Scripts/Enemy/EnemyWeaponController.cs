using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyWeaponController : MonoBehaviour
    {
        public EnemyWeapon[] weapons;
        public float fovAngle;
        private Transform target;

        private void Start()
        {
            target = Camera.main.transform;
        }

        private bool IsTargetInFOV(Transform target)
        {
            if (target==null ) return false;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float angleBetweenEnemyAndTarget = Vector3.Angle(transform.forward, directionToTarget);

            return angleBetweenEnemyAndTarget < fovAngle / 2f;
        }


        private void Update()
        {
            if (!IsTargetInFOV(target))
            {
                return;
            }

            foreach (EnemyWeapon enemyWeapon in weapons)
            {
                if (!enemyWeapon.IsReady()) continue;
                Debug.Log("Firing enemy weapon: "+ enemyWeapon.name);
                enemyWeapon.Activate();
            }
        }
        
    }
}