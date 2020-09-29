using UnityEngine;

namespace Core.Common
{
    [CreateAssetMenu(fileName = "UnitStats", menuName = "ProjectR/UnitStats", order = 0)]
    public class UnitStats : ScriptableObject
    {
        public float healthPoints;
        public float manaPoints;
        public float enegry;
        public float defence;

        public float GetFightMoveFrequency()
        {
            return 1.0f;
        }
    }
}