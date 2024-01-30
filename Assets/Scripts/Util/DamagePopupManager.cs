using CodeMonkey.Utils;
using DefaultNamespace.ObjectPooling;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class DamagePopupManager : Singleton<DamagePopupManager>
    {
        public float offsetAmt,duration;
        public GameObject popUpPrefab;
        
        public void CreatePopUp(string value,Vector3 position, Quaternion rotation)
        {
            Vector3 offset = Random.insideUnitSphere * offsetAmt;
            var popup = ObjectPoolManager.SpawnObject(popUpPrefab, position + offset, rotation);
            TextMeshProUGUI textUI = popup.GetComponentInChildren<TextMeshProUGUI>();
            textUI.text = value;
            FunctionTimer.Create(() =>
            {
                ObjectPoolManager.ReturnObjectToPool(popup);
            }, duration);
        }
    }
}