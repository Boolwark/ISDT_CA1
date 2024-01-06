using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

namespace Environment.Keyboard
{
    public class ShowKeyboard : MonoBehaviour
    {
        private TMP_InputField inputField;

        void Start()
        {
            inputField = GetComponent<TMP_InputField>();
            inputField.onSelect.AddListener(x=>OpenKeyboard());
        }

        public void OpenKeyboard()
        {
            Debug.Log("Opening keyboard");
            NonNativeKeyboard.Instance.InputField = inputField;
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);  
    
        }
    }
}