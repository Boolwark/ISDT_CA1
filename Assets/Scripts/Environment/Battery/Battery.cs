using UnityEngine;

namespace Environment.Battery
{
    public class Battery : MonoBehaviour
    {
        private bool activated = false;
        public void OnSelect()
        {
            if (!activated)
            {
                BatteryManager.Instance.OnBatteryActivated();
            }
            activated = true;
        }
    }
}