using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class SlidingDoor : MonoBehaviour
    {
        public float duration = 2f;
        public Vector3 finalScale = new Vector3(2, -0.036824476f, 2);
        public void UnlockDoor()
        {
            transform.DOScale(finalScale, duration);
        }
    }
}