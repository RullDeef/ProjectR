using UnityEngine;

namespace Core.Common
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "ProjectR/WeaponStats", order = 0)]
    public class WeaponStats : ScriptableObject
    {
        public float healthPoints;
        public float atackDamage;
    }
}