using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Util
{
    public class PortalManager: Singleton<PortalManager>
    {
        private GameObject[] portals;
        private int currentIndex = 0;

        private void Start()
        {
            portals = GameObject.FindGameObjectsWithTag("Portal");
            var player = FindObjectOfType<Player.Player>();
            portals = portals.OrderBy(portal => Vector3.Distance(portal.transform.position, player.transform.position)).ToArray();
            foreach (var portal in portals)
            {portal.GetComponent<TeleportationAnchor>().teleporting.AddListener(OnPortalActivated);
                portal.SetActive(false);
            }
        
        }

        public void OnPortalActivated(TeleportingEventArgs args)
        {
            if (currentIndex >= portals.Length) return;
            portals[currentIndex].SetActive(false);
            if (currentIndex + 1 < portals.Length)
            {
                portals[currentIndex+1].SetActive(true);
            }

            currentIndex += 1;
        }
        public void ActivateCurrentPortal() 
        {
            portals[currentIndex].SetActive(true);
        }
    }
}