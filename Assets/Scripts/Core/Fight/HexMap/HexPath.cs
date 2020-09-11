using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fight
{
    [Serializable]
    public class HexPath
    {
        public List<HexCell> cells = new List<HexCell>();
        
        [SerializeField]
        private HexCell currentCell = null;

        [SerializeField]
        private int currentIndex = -1;

        public HexPath(IEnumerable<HexCell> cells)
        {
            this.cells.AddRange(cells);
            currentCell = this.cells[0];
            currentIndex = 0;
        }

        public int GetPathLength()
        {
            return cells.Count;
        }
        
        public HexCell GetCurrentCell()
        {
            return currentCell;
        }

        public HexCell GetDestinationCell()
        {
            return cells[cells.Count - 1];
        }

        public int GetCurrentIndex()
        {
            return currentIndex;
        }

        public void GoToNextCell()
        {
            if (currentIndex + 1 < cells.Count)
            {
                currentIndex++;
                currentCell = cells[currentIndex];
            }
            else
            {
                currentIndex = cells.Count;
                currentCell = null;
            }
        }

        public bool IsCompleted()
        {
            return currentIndex == cells.Count;
        }

        public bool IsNotCompleted()
        {
            return currentIndex < cells.Count;
        }

        public override string ToString()
        {
            string result = cells[0].ToString();

            for (int i = 1; i < cells.Count; i++)
                result += "," + cells[i].ToString();

            return $"{{{result}}}";
        }
    }
}