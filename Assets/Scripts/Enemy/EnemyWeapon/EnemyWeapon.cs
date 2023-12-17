using UnityEngine;

namespace Enemy
{
    public abstract class  EnemyWeapon : MonoBehaviour
    {
        public abstract void Activate();
        public abstract bool IsReady();

    }
}