using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ability.Abilities
{
    [CreateAssetMenu]
    public class DashAbility : Ability
    {
        public float dashVelocity;
        public float normalVelocity = 10;


        private ActionBasedContinuousMoveProvider _actionBasedContinuousMoveProvider;
        public override void Activate(GameObject parent)
        {
            if (!_actionBasedContinuousMoveProvider)
            {
                _actionBasedContinuousMoveProvider = parent.GetComponentInChildren<ActionBasedContinuousMoveProvider>();
            }
            _actionBasedContinuousMoveProvider.moveSpeed = dashVelocity;
            Debug.Log("dashing");
        }

        public override void BeginCooldown(GameObject parent)
        {
            if (!_actionBasedContinuousMoveProvider)
            {
                _actionBasedContinuousMoveProvider = parent.GetComponentInChildren<ActionBasedContinuousMoveProvider>();
            }
            _actionBasedContinuousMoveProvider.moveSpeed = normalVelocity;
            Debug.Log("Returning to normal speed");
        }
    }
}