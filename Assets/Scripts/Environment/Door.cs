using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour,IDestructible
    {
        public void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}