using System;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    public class AddOutlineOnStart : MonoBehaviour
    {
        public Color outlineColor = Color.green;
        private void Start()
        {
            var currentOutline = gameObject.AddComponent<Outline>();
            currentOutline.OutlineMode = Outline.Mode.OutlineAll;
            currentOutline.OutlineColor = outlineColor;
            currentOutline.OutlineWidth = 5f;
        }
    
        
    }
}