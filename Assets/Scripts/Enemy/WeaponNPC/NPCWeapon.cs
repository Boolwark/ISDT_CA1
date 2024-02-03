using UnityEngine;

namespace Enemy
{
    public abstract class  NPCWeapon : MonoBehaviour
    {
        public abstract void Activate();
        public abstract bool IsReady();
        public abstract void SetTarget(Transform newTarget);

    }
}