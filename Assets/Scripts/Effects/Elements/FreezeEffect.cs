using Unity.VisualScripting;
using UnityEditor.SceneManagement;

namespace Effects.Elements
{
    public class FreezeEffect : ElementalEffect
    {

        private MeshDestroy _meshDestroy;
        public int cutCascades = 5;
        public float breakForce;

        public override void Activate()
        {
          
            _meshDestroy = transform.parent.AddComponent<MeshDestroy>();
            _meshDestroy.CutCascades = cutCascades;
            _meshDestroy.ExplodeForce = breakForce;
            base.Activate();
            base.sm.TakeDamage(damage);
        }
        protected override void DeathEffect()
        {
            _meshDestroy.DestroyMesh();
            base.DeathEffect();
        }

        void Start()
        {
            Activate();
        }
    }
}