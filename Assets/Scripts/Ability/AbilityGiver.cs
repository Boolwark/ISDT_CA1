using System;
using UnityEngine;

namespace Ability
{
    public class AbilityGiver : MonoBehaviour
    {
        private AbilityHolder _abilityHolder;
        private void Start()
        {
            _abilityHolder = FindObjectOfType<AbilityHolder>();
        }

        public Ability ability;
        public void GiveAbility()
        {
            _abilityHolder.ability = ability;
            Debug.Log("Gave player new ability!");
        }
    }
}