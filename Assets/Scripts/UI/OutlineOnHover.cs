using System;
using System.Security.Cryptography;
using UnityEngine;

namespace UI
{
    public class OutlineOnHover : MonoBehaviour
    {
        public Color onHoverOutlineColor = Color.blue;
        private Color initColor;

        public void Start()
        {
            initColor = GetComponent<Outline>().OutlineColor;
        }


        public void OnHover()
        {
            if (TryGetComponent(out Outline outline))
            {
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = onHoverOutlineColor;
                outline.OutlineWidth = 5f;
            }
        }

        public void OnHoverExit()
        {
            if (TryGetComponent(out Outline outline))
            {
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = initColor;
                outline.OutlineWidth = 5f;
            }
        }

    
    }
}