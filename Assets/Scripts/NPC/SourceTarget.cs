using UnityEngine;

namespace Assets.Scripts.NPC
{
    public class SourceTarget : MonoBehaviour
    {
        private void Start()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                transform.parent = player.transform;
            }
        }
    }
}