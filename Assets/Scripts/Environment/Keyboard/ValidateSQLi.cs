using CodeMonkey.Utils;
using TMPro;

using UnityEngine;
using UnityEngine.Events;

namespace Environment.Keyboard
{
    public class ValidateSQLi : MonoBehaviour
    {
        public TMP_InputField InputField;
        public GameObject successPanel, failPanel;
        public UnityEvent OnSucceed;
        public UnityEvent OnThreeFailedAttempts;
        private int nWrongAttempts = 0;
        public void OnInputEntered()
        {
            string input = InputField.text;
            Debug.Log("INput is "+input);
            string procInput = input.ToLower().Replace(" ", "");
            Debug.Log("Proc input is" + procInput);
            if (procInput.Contains("\"or1=1--"))
            {
                Debug.Log("Successful SQLi");
                DisplayResultPanel(true);
                nWrongAttempts = 0;
            }
            else
            {
                DisplayResultPanel(false);
                nWrongAttempts++;
                if (nWrongAttempts == 3)
                {
                    OnThreeFailedAttempts?.Invoke();
                }
            }
        }

        private void DisplayResultPanel(bool success = true)
        {
            var currentPanel = success ? successPanel : failPanel;
          
            currentPanel.SetActive(true);
                FunctionTimer.Create(() => { currentPanel.SetActive(false);}, 2f);
                if (success)
                {
                    OnSucceed?.Invoke();
                }
        }
    }
}