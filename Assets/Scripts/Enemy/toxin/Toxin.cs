using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.toxin
{
    public class Toxin : MonoBehaviour
    {
        public float spawnDuration,offset;
        public List<GameObject> limbs;
        private void Start()
        {
            foreach (var limb in limbs)
            {
                PlaySpawnEffect(limb.transform);
            }

        }
        private void PlaySpawnEffect(Transform limb)
        {
            Vector3 ogPos = limb.position;
            Vector3 newPos = ogPos + Random.insideUnitSphere * offset;
            limb.position = newPos;
            limb.transform.DOMove(ogPos, spawnDuration);
        }
    }
}