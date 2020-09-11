using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fight
{
    [Serializable]
    public class MapGenerationParams
    {

    }

    public class HexMapGenerator : MonoBehaviour
    {
        public Transform hexMapContainer;

        // loads cells from hexMapContainer
        public HexMap LoadFromScene()
        {
            HexMap map = new HexMap();

            foreach (HexCell cell in hexMapContainer.GetComponentsInChildren<HexCell>())
            {
                map.cells.Add(cell);
                cell.hexMap = map;
            }

            return map;
        }

        public HexMap Generate(MapGenerationParams genParams)
        {
            HexMap map = new HexMap();

            // generate cells here

            return map;
        }
    }
}