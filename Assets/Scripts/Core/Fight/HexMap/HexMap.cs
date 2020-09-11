using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core.Fight
{
    [Serializable]
    public class HexMap
    {
        public HashSet<HexCell> cells = new HashSet<HexCell>();

        public HexCell GetRandomCell()
        {
            return cells.ElementAt(UnityEngine.Random.Range(0, cells.Count));
        }

        public HexPath ConstructPath(HexCell startingCell, HexCell endingCell)
        {
            HashSet<HexCellWrapper> visitedCells = new HashSet<HexCellWrapper>();
            HashSet<HexCellWrapper> nextSurroundingCells = new HashSet<HexCellWrapper>();
            HashSet<HexCellWrapper> surroundingCells = new HashSet<HexCellWrapper>()
                { new HexCellWrapper(endingCell) };

            HexCellWrapper staringCellWrapper = null;
            bool isPathConstructed = false;

            int iteration = 0;
            int maxIterationsCount = 50;

            while (!isPathConstructed)
            {
                if (iteration++ == maxIterationsCount)
                {
                    Debug.LogError("Max iterations count reached!");
                    return null;
                }

                if (surroundingCells.Count == 0)
                    return null; // path cannot be constructed

                // for each cell in surrounding cells
                foreach (HexCellWrapper cellWrapper in surroundingCells)
                {
                    // if was visited - continue
                    if (visitedCells.Contains(cellWrapper))
                        continue;

                    // if cell is start cell break and reverse path
                    if (cellWrapper.cell == startingCell)
                    {
                        staringCellWrapper = cellWrapper;
                        isPathConstructed = true;
                        break;
                    }

                    // add next cells to visit
                    foreach (HexCell neighbor in cellWrapper.cell.GetNeighbors())
                        nextSurroundingCells.Add(new HexCellWrapper(neighbor, cellWrapper));

                    // add it to visited
                    visitedCells.Add(cellWrapper);
                }

                // shift surrounding cell layers
                surroundingCells = nextSurroundingCells;
                nextSurroundingCells = new HashSet<HexCellWrapper>();
            }

            // reverse path
            List<HexCell> path = new List<HexCell>();
            HexCellWrapper currentWrapper = staringCellWrapper;
            while (currentWrapper.next != null && startingCell != endingCell)
            {
                path.Add(currentWrapper.cell);
                currentWrapper = currentWrapper.next;
            }

            // add last cell
            path.Add(currentWrapper.cell);
            return new HexPath(path);
        }

        public HexPath ConstructPath(HexCell startingCell, HexCell endingCell, FightUnitShape unitShape)
        {
            throw new NotImplementedException();
        }

        private class HexCellWrapper
        {
            public HexCell cell;
            public HexCellWrapper next;

            public HexCellWrapper(HexCell cell, HexCellWrapper next = null)
            {
                this.cell = cell;
                this.next = next;
            }

            public override int GetHashCode()
            {
                return cell.GetHashCode();
            }
        }
    }
}