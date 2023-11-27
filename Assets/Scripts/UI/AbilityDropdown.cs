using System;
using Ability;
using Ability.Abilities;

namespace UI
{
    using TMPro;
    using UnityEngine;
    using Util;

    namespace UI
    {
        public class AbilityDropdown : MonoBehaviour
        {
           
            private AbilityHolder _abilityHolder;
            public WebAbility webAbility;
            public GrapplingAbility grappleAbility;
            public DashAbility dashAbility;
            public ReflexAbility reflexAbility;
            private void Start()
            {
                _abilityHolder = GameObject.FindObjectOfType<AbilityHolder>();
            }

            public void DropdownSample(int index)
            {
                switch (index)
                {
                    case 0:
                        _abilityHolder.ability = webAbility;
                        break;
                    case 1:
                        _abilityHolder.ability = grappleAbility;
                        break;
                    case 2:
                        _abilityHolder.ability = reflexAbility;
                        break;
                    case 3:
                        _abilityHolder.ability = dashAbility;
                        break;
                }
            }
        }
    }
}