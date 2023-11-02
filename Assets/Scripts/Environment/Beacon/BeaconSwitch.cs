namespace Environment
{
    using UnityEngine;

    public class BeaconSwitch : MonoBehaviour
    { // Assign the beacon you want this switch to activate

        public void ToggleBeacons()
        {
         
            BeaconManager.Instance.ToggleAllBeacons();
        }
    }

}