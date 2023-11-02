using TMPro;
using UnityEngine;
using Util;

namespace UI
{
    public class DifficultyDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text difficultyText;

        public void DropdownSample(int index)
        {
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
        }
    }
}