using System.Security.Cryptography;
using DefaultNamespace.ObjectPooling;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class DissolveManager : Singleton<DissolveManager>
    {
        public Material dissolveMaterial;
        public void DissolveObject(GameObject target, float dissolveSpeed)
        { 
            DissolveController controller = target.AddComponent<DissolveController>();
            if (target.TryGetComponent(out MeshRenderer renderer))
            {
                renderer.material = dissolveMaterial;
                controller.dissolveSpeed = dissolveSpeed;
                controller.SetDissolveMaterial(dissolveMaterial,true);
                controller.ReverseDissolving();
            }
            else
            {
                ObjectPoolManager.ReturnObjectToPool(target);
            }
          
            //controller.OnFadeOutEnd.AddListener(() => { Destroy(target,1f);});
        }
    }
}