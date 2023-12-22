using UnityEngine;

namespace Effects
{
    public class DestroyOnDeath : MonoBehaviour
    {
        public int cutCascades = 5;
        public float explodeForce;
        public void Activate()
        {
            var md =  gameObject.AddComponent<MeshDestroy>();
            md.ExplodeForce = explodeForce;
            md.CutCascades = cutCascades;
            md.DestroyMesh();
        }
        public void OnSelectExit()
        {
            Activate();
        }
    }
}