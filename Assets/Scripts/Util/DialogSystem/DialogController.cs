using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Util.DialogSystem
{
    public class DialogController : MonoBehaviour
    {
        public Dialog dialog;
        public float sentenceDuration;
        public Ease sentenceTransitionEase;
        public void PlayDialog(TMP_Text textUI)
        {
            StartCoroutine(DialogSequence(textUI));
        }

        private IEnumerator DialogSequence(TMP_Text textUI)
        {
            for (int i = 0; i < dialog.dialogs.Count; i++)
            {
                textUI.transform.localScale = Vector3.zero;
                textUI.text = dialog.dialogs[i];
                textUI.transform.DOScale(Vector3.one, 1f).SetEase(sentenceTransitionEase);
                yield return new WaitForSeconds(sentenceDuration);
            }
        }
    }
}