using UnityEngine;
using UnityEngine.InputSystem;

namespace Ability
{
    public class AbilityHolder : MonoBehaviour
    {
        public Ability ability;
        private float cooldownTime;
        private float activeTime;

        enum AbilityState
        {
            ready,
            active,
            cooldown
        }

        private AbilityState state = AbilityState.ready;
        public InputActionProperty activateAbilityAction;

        void Update()
        {
            
            switch (state)
            {
                case AbilityState.ready:
                    if (activateAbilityAction.action.ReadValue<float>() > 0.5)
                    {
                        ability.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = ability.activeTime;
                    } 
                    break;
                case AbilityState.active:
                    if (activeTime > 0f)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        ability.BeginCooldown(gameObject);
                        state = AbilityState.cooldown;
                        cooldownTime = ability.cooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime > 0f)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;

            }
        }
        
    }
}