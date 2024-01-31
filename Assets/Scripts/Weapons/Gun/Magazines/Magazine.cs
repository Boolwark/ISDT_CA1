namespace Weapons.Magazines
{
    using UnityEngine;

    namespace Weapons.Magazines
    {
        [CreateAssetMenu(fileName = "NewMagazine", menuName = "Weapons/Magazines/Magazine")]
        public class Magazine : ScriptableObject
        {
            [Header("Magazine Settings")]
            public GameObject bulletPrefab;
            public int ammoCount;
            public float attachRange;
        }
    }

}