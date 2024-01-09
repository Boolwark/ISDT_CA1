using System.Collections.Generic;
using UnityEngine;

namespace Util.DialogSystem
{
    [CreateAssetMenu(menuName = "NPC",fileName = "Dialog")]
    public class Dialog : ScriptableObject
    {
        public List<string> dialogs = new List<string>();
    }
}