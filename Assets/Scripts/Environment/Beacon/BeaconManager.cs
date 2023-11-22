using System.Collections.Generic;
using Util;

namespace Environment
{
    public class BeaconManager : Singleton<BeaconManager>
    {



        private Beacon[] beacons;
        private int numBeacons;
        protected override void Awake()
        {
            beacons = FindObjectsOfType<Beacon>();
            numBeacons = beacons.Length;
            base.Awake();
        }

        public void ToggleAllBeacons()
        {
            foreach (var beacon in beacons)
            {
                beacon.ToggleBeacon();
            }
        }
        
    }
}