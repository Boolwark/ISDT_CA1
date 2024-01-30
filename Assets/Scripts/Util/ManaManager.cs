using System;
using UnityEngine.UI;

namespace Util
{
    public class ManaManager : Singleton<ManaManager>
    {
        public Slider manaUI;
        private float currentMana = 100f;
        private float maxMana;

        private void Start()
        {
            maxMana = currentMana;
        }

        public void ChangeMana(float change)
        {
            currentMana += change;
            manaUI.value = (float)currentMana / maxMana;
        }

        public bool HasEnoughMana(float manaCost)
        {
            return manaCost <= currentMana;
        }
    }
}