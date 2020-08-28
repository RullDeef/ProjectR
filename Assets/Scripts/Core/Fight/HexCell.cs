using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    private List<HexCell> neighbors;

    private void Awake()
    {
        FindNeighbors();
    }

    private void FindNeighbors()
    {
        neighbors = new List<HexCell>();

        foreach (HexCell cell in transform.parent.GetComponentsInChildren<HexCell>())
        {
            if (cell != this && Vector3.Distance(transform.position, cell.transform.position) <= 2.1f)
            {
                neighbors.Add(cell.GetComponent)
            }
        }
    }
}
