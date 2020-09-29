using UnityEngine;

namespace Core.Common
{
    [CreateAssetMenu(fileName = "ArmorStats", menuName = "ProjectR/ArmorStats", order = 0)]
    public class ArmorStats : ScriptableObject
    {
        public float healthPoints;
        public float defencePoints; // amount of damage it can take during single atack
    }
}