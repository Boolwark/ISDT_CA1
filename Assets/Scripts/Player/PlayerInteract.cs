using Assets.Scripts.NPC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private InputActionProperty interactAction;
        [SerializeField] private float interactRange = 2f;
        
        void Update()
        {
            if (interactAction.action.ReadValue<float>() > 0.5f)
            {
                float interactRange = 2f;
                Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                    {
                        npcInteractable.Interact();
                    }
                }
            }
        }
    }
}