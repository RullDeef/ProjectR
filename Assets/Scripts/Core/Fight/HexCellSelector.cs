using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fight
{
    public class HexCellSelector : MonoBehaviour
    {
        public HexCell selectedCell = null;
        private float maxSelectionDistance = 100.0f;

        public delegate void OnSelectCellHandlerType(HexCell selection);
        public delegate void OnDeselectCellHandlerTyle();
        public OnSelectCellHandlerType onSelectCellHandler = (HexCell) => { };
        public OnDeselectCellHandlerTyle onDeselectCellHandler = () => { };

        private void OnDrawGizmos()
        {
            // render debug info
            if (selectedCell != null)
                Gizmos.DrawWireSphere(selectedCell.transform.position, 1.0f);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                HandleClick();
        }

        private void HandleClick()
        {
            // deselect previous cell
            if (selectedCell != null)
            {
                onDeselectCellHandler();
                selectedCell = null;
            }

            // select new one
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, maxSelectionDistance))
            {
                selectedCell = hit.transform.GetComponentInParent<HexCell>();
                if (selectedCell != null)
                {
                    onSelectCellHandler.Invoke(selectedCell);
                    break;
                }
            }
        }
    }
}
