using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts.NPC
{
    public class NPCInteractable : MonoBehaviour
    {
        private Animator animator;
        public TextMeshProUGUI dialogText;
        public float interactionDist;
        public string whatToSay;
        private Transform player;
        public Canvas uiCanvas;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").transform;
         
        }

        private void Update()
        {
         
         
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f);
            
        }
        public void Interact()
        {
            dialogText.text = whatToSay;
        }
    }
}