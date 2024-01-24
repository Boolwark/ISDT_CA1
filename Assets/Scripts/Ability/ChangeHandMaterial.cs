using System;
using UnityEngine;

namespace Ability
{
    public class ChangeHandMaterial : MonoBehaviour
    {
        private GameObject leftHand, rightHand;

        private void Start()
        {
            leftHand = GameObject.FindGameObjectWithTag("LeftHand");
            rightHand = GameObject.FindGameObjectWithTag("RightHand");
        }

        public void Activate(Material newMaterial)
        {
            leftHand.GetComponent<SkinnedMeshRenderer>().material = newMaterial;
            rightHand.GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        }
    }
}