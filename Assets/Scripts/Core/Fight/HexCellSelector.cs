using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fight
{
    public class HexCellSelector : MonoBehaviour
    {
        private static HexCellSelector instance = null;

        public bool selectionEnabled = true;
        public HexCell selectedCell = null;
        private float maxSelectionDistance = 100.0f;

        public delegate void OnSelectCellHandlerType(HexCell selection);
        public delegate void OnDeselectCellHandlerTyle();
        public OnSelectCellHandlerType onSelectCellHandler = (HexCell) => { };
        public OnDeselectCellHandlerTyle onDeselectCellHandler = () => { };

        private void Awake()
        {
            instance = this;
        }

        public static void DisableSelection()
        {
            instance.selectionEnabled = false;
        }

        public static void EnableSelection()
        {
            instance.selectionEnabled = true;
        }

        public static bool IsAnyCellSelected()
        {
            return instance.selectedCell != null;
        }

        public static HexCell GetSelectedCell()
        {
            return instance.selectedCell;
        }

        public static void ClearSelection()
        {
            instance.selectedCell = null;
        }

        private void OnDrawGizmos()
        {
            // render debug info
            if (selectedCell != null)
                Gizmos.DrawWireSphere(selectedCell.transform.position, 1.0f);
        }

        private void Update()
        {
            if (selectionEnabled && Input.GetMouseButtonDown(0))
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
