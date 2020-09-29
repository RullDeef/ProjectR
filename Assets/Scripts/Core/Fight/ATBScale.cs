using System.Collections.Generic;
using UnityEngine;
using Core.Common;
using System.Linq;

namespace Core.Fight
{
    public class ATBScale
    {
        private const int size = 5;

        private List<UnitStats> activeUnits = new List<UnitStats>();
        public Queue<UnitStats> unitsQueue = new Queue<UnitStats>();

        // заполняет шкалу при создании 
        public ATBScale(List<UnitStats> units)
        {
            activeUnits.AddRange(units);

            while (unitsQueue.Count < size)
                AppendNewUnitMove();
        }

        /**
         * Обновляет дэк с участниками боя.
         */
        public void UpdateActiveUnits(List<UnitStats> units)
        {
            activeUnits.Clear();
            activeUnits.AddRange(units);

            unitsQueue = new Queue<UnitStats>(unitsQueue.Where(unit => activeUnits.Contains(unit)));

            while (unitsQueue.Count < size)
                AppendNewUnitMove();
        }

        public UnitStats GetCurrentUnit()
        {
            return unitsQueue.Peek();
        }

        /**
         * Удаляет первый элемент на шкале и добавляет новый в конец. 
         */
        public void PropagateScale()
        {
            unitsQueue.Dequeue();
            AppendNewUnitMove();
        }

        /**
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
        }

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
         * Возвращает массив из вероятностей ходов активных участников боя.
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
