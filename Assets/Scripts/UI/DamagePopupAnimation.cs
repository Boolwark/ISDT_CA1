using TMPro;
using UnityEngine;

namespace UI
{
    public class DamagePopupAnimation : MonoBehaviour
    {
        public AnimationCurve opacityCurve;
        public AnimationCurve scaleCurve;
        public AnimationCurve heightCurve;
        private Vector3 origin;
        private TextMeshProUGUI tmp;
        private float time = 0;

        void Awake()
        {
            tmp = GetComponentInChildren<TextMeshProUGUI>();
            origin = transform.position;
        }

        private void Update()
        {
            tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
            transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
            transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
            time += Time.deltaTime;
        }
    }
}