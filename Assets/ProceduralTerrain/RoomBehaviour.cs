using System;
using UnityEngine;

namespace DefaultNamespace.ProceduralTerrain
{
    public class RoomBehaviour : MonoBehaviour
    {
        // 0 -up 1-down 2-right 3-left
        public GameObject[] walls;
        public GameObject[] doors;

     

        public void UpdateRoom(bool[] status)
        {
            for (int i = 0; i < status.Length; i++)
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
            }
            
        }
    }
}