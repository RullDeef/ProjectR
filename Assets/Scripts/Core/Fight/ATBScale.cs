using System.Collections.Generic;
using UnityEngine;
using Core.Common;
using System.Linq;

namespace Core.Fight
{
    /**
     * @brief Контролирует последовательность ходов участников боя.
     */
    [System.Serializable]
    public class ATBScale
    {
        /// юниты, способные совершать новые ходы
        private List<UnitStats> activeUnits = new List<UnitStats>();

        /// Максимальное кол-во ходов, рассчитываемых наперёд
        public static int size = 5;

        /// Список ходов юнитов
        public Queue<UnitStats> unitsQueue = new Queue<UnitStats>();

        /// Только для отладки. Отслеживание текущего состояния в инспекторе
        [SerializeField]
        public List<UnitStats> inspectUnitsQueue = new List<UnitStats>();

        /**
         * @brief заполняет шкалу при создании.
         *
         * @param units Список статов активных юнитов
         */
        public ATBScale(List<UnitStats> units)
        {
            activeUnits.AddRange(units);

            while (unitsQueue.Count < size)
                AppendNewUnitMove();
        }

        /**
         * @brief Обновляет дэк с участниками боя.
         *
         * @param units Список статов активных юнитов
         */
        public void UpdateActiveUnits(List<UnitStats> units)
        {
            activeUnits.Clear();
            activeUnits.AddRange(units);

            unitsQueue = new Queue<UnitStats>(unitsQueue.Where(unit => activeUnits.Contains(unit)));
            inspectUnitsQueue = new List<UnitStats>(unitsQueue);

            while (unitsQueue.Count < size)
                AppendNewUnitMove();
        }

        /**
         * @brief Возвращает юнита, который сейчас ходит.
         */
        public UnitStats GetCurrentUnitStats()
        {
            return unitsQueue.Peek();
        }

        /**
         * @brief Удаляет первый элемент на шкале и добавляет новый в конец.
         */
        public void PropagateScale()
        {
            unitsQueue.Dequeue();
            inspectUnitsQueue.RemoveAt(0);
            AppendNewUnitMove();
        }

        /**
         * @brief Генерирует новый ход.
         *
         * Добавляет в конец очереди ссылку на статы участника боя
         * рандомным способом используя частоты ходов всех участников.
         */
        private void AppendNewUnitMove()
        {
            float[] probabilities = GetActiveUnitProbabilities();

            // select random active unit
            UnitStats unit = SelectRandomUnit(probabilities);

            // add it to queue
            unitsQueue.Enqueue(unit);
            inspectUnitsQueue.Add(unit);
        }

        /**
         * @brief Выбирает случайного юнита на основе набора вероятностей.
         */
        private UnitStats SelectRandomUnit(float[] probabilities)
        {
            int randomIndex = -1;
            float randomValue = Random.Range(0.0f, 1.0f);

            for (int i = 0; i < probabilities.Length; i++)
            {
                randomValue -= probabilities[i];
                if (randomValue <= 0.0f)
                {
                    randomIndex = i;
                    break;
                }
            }

            Debug.AssertFormat(randomIndex != -1, "Не удалось выбрать случайного участника боя.");
            return activeUnits[randomIndex];
        }

        /**
         * @brief Возвращает массив из вероятностей ходов активных участников боя.
         */
        private float[] GetActiveUnitProbabilities()
        {
            Debug.AssertFormat(activeUnits.Count > 0, "Число активных участников боя не должно быть нулевым.");

            float[] probabilities = new float[activeUnits.Count];
            float total = 0.0f;
            
            for (int i = 0; i < probabilities.Length; i++)
            {
                UnitStats unit = activeUnits[i];

                probabilities[i] = unit.GetFightMoveFrequency();
                Debug.AssertFormat(probabilities[i] >= 0.0f, "Частота хода не может быть отрицательной. Участник: %s", unit);

                total += probabilities[i];
            }

            Debug.AssertFormat(total > 0.0f, "Суммарная вероятность не должна быть нулевой.");

            for (int i = 0; i < probabilities.Length; i++)
                probabilities[i] /= total;

            return probabilities;
        }
    }
}
