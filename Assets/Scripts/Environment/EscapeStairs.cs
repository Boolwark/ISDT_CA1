using System;
using UnityEngine;
using Util;

namespace Environment
{
    public class EscapeStairs : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                LevelManager.Instance.ChangeSceneDirect("ThirdScene");
            }
        }
    }
}