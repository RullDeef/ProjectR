using System.Collections;
using UnityEngine;

namespace Core.Fight
{
    public class FightPlayerController : FightUnitController
    {
        private bool moveHasEnded = false;

        public override IEnumerator MakeMove()
        {
            Debug.Log("Your move!");

            while (!moveHasEnded)
            {
                if (HexCellSelector.IsAnyCellSelected() && Input.GetKey(KeyCode.G))
                {
                    HexCell cell = HexCellSelector.GetSelectedCell();
                    if (cell.IsFree())
                    {
                        GoToCell(cell);
                        HexCellSelector.ClearSelection();
                        moveHasEnded = true;
                    }
                }
                yield return new WaitForEndOfFrame();
            }

            moveHasEnded = false;
            yield return null;
        }
        private void Update()
        {
            return;
            /*
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
            */
        }
    }
}