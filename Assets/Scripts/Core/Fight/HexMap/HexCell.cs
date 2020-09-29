using System.Collections.Generic;
using UnityEngine;

namespace Core.Fight
{
    public enum HexCellType
    {
        Empty,
        Ground,
        Wind /* ??? */
    }

    public class HexCell : MonoBehaviour
    {
        [SerializeField]
        public HexMap hexMap;
        public bool walkable = true; // true if any unit can be placed inside this cell
        public FightUnitController controller = null;

        private List<HexCell> neighbors;

        public bool IsFree()
        {
            return controller == null;
        }

        public List<HexCell> GetNeighbors()
        {
            return neighbors;
        }

        private void Awake()
        {
            FindNeighbors();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, 1);
        }

        private void FindNeighbors()
        {
            neighbors = new List<HexCell>();

            foreach (HexCell cell in transform.parent.GetComponentsInChildren<HexCell>())
            {
                if (cell != this && Vector3.Distance(transform.position, cell.transform.position) <= 2.1f)
                {
                    neighbors.Add(cell);
                }
            }
        }

        public override string ToString()
        {
            return transform.position.ToString();
        }
    }
}