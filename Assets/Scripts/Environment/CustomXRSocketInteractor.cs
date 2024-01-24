using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Environment
{
    public class CustomXRSocketInteractor : MonoBehaviour
    {
        public Transform[] attachTransforms;
        private Dictionary<string, int> nameToIndex = new();
        private int index = 0;

        public void AttachToTransform(Transform obj)
        {
    
            index = ClampIndex(index);
            if (nameToIndex.ContainsKey(obj.name))
            {
                return;
            }
            nameToIndex.Add(obj.name,index);
            obj.parent = attachTransforms[index];
            obj.localPosition = Vector3.zero;
            obj.localRotation = Quaternion.identity;
            index++;
        }

        public void OnDetach(string objName)
        {
          
            if (nameToIndex.ContainsKey(objName))
            {
                Debug.Log("releasing"+objName + "from INVENTORY");
                nameToIndex.Remove(objName);
                index--;
            }
       
        }

        private int ClampIndex(int index)
        {
            return Math.Max(0, Math.Min(index, attachTransforms.Length - 1));
        }
    }
}