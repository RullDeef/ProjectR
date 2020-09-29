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

        public override string ToString()
        {
            return $"{{{healthPoints},{manaPoints},{enegry},{defence}}}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is UnitStats u)
                return u.GetHashCode() == GetHashCode();
            else
                return false;
        }
    }
}