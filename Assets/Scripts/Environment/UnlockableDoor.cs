using DG.Tweening;
using Effects;
using UnityEngine;

namespace Environment
{
    public class UnlockableDoor : MonoBehaviour
    {
        public float duration = 2f;
        public Vector3 finalScale = new Vector3(2, -0.036824476f, 2);
        public void UnlockSlidingDoor()
        {
            transform.DOScale(finalScale, duration);
        }

        public void UnlockTurningDoor()
        {
            transform.DORotate(transform.rotation.eulerAngles +new Vector3(0, 90, 0),2);
        }

   
    }
}