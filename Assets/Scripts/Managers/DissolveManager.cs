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
            target.GetComponent<MeshRenderer>().material = dissolveMaterial;
            controller.dissolveSpeed = dissolveSpeed;
            controller.SetDissolveMaterial(dissolveMaterial,true);
            controller.ReverseDissolving();
            //controller.OnFadeOutEnd.AddListener(() => { Destroy(target,1f);});
        }
    }
}