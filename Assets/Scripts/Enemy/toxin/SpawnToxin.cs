using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.toxin
{
    public class SpawnToxin : MonoBehaviour
    {

        public GameObject toxinPrefab;
       
        public void Activate()
        {
            var toxin = ObjectPoolManager.SpawnObject(toxinPrefab, transform.position, Quaternion.identity);
            
        }
        
    }
}