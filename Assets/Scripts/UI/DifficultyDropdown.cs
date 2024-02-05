using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI
{
    public class DifficultyDropdown : MonoBehaviour
    {

        public Dropdown Dropdown;
        public void DropdownSample()
        {
            int index = Dropdown.value;
            switch (index)
            {
                case 0:
                    GameManager.Instance.chosenDifficulty = GameManager.Difficulty.EASY;
                    break;
                case 1:
                    GameManager.Instance.chosenDifficulty = GameManager.Difficulty.NORMAL;
                    break;
                case 2:
                    GameManager.Instance.chosenDifficulty = GameManager.Difficulty.NIGHTMARE;
                    break;
            }
            Debug.Log("Current game difficulty: " + GameManager.Instance.chosenDifficulty);
        }
    }
}