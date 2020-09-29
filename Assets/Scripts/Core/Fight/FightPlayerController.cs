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
                        yield return StartCoroutine(GoToCell(cell));
                        HexCellSelector.ClearSelection();
                        moveHasEnded = true;
                    }
                }
                yield return new WaitForFixedUpdate();
            }

            moveHasEnded = false;
            yield return null;
        }
    }
}