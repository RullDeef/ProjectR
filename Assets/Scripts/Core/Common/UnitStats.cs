using UnityEngine;

namespace Core.Common
{
    /**
     * @brief Статы юнита.
     *
     * Предполагается использовать эту структуру для хранения информации о
     * динамических параметрах активных юнитов (игрока, миньонов, врагов и тп).
     */
    [CreateAssetMenu(fileName = "UnitStats", menuName = "ProjectR/UnitStats", order = 0)]
    public class UnitStats : ScriptableObject
    {
        /// Оставшееся здоровье у юнита
        public float healthPoints;
        public float manaPoints;
        public float enegry;
        public float defence;

        /**
         * @brief Расчитывает частоту совершения ходов юнитом.
         *
         * @return неотрицательное число - частота совершения ходов
         */
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