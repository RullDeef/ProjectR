using UnityEngine;

namespace Core.Fight
{
    public class FightPlayerController : FightUnitController
    {
        private void Update()
        {
            return;
            
            if (!motor.isMoving)
            {
                HexCell randomCell = null;

                int iteration = 0, maxIterationsCount = 5;
                while (randomCell == null || randomCell == cell)
                {
                    if (iteration++ == maxIterationsCount)
                    {
                        Debug.LogError("Max iterations count reached!");
                        return;
                    }

                    randomCell = cell.hexMap.GetRandomCell();
                }

                GoToCell(randomCell);
            }
        }
    }
}