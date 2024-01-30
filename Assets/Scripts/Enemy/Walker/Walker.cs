using UnityEngine;

namespace Enemy.Walker
{
    public class Walker : MeleeEnemyAI
    {
        public Material[] walkerSkins;

        new void Start()
        {
            
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = SelectRandomMaterial();
            base.Start();
            
        }
        private Material SelectRandomMaterial()
        {
            return walkerSkins[Random.Range(0, walkerSkins.Length)];
        }
        public GameObject tentacles;
        public void OnPlayerDetected()
        {
            tentacles.SetActive(true);
        }
    }
}