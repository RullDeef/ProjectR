using System.Collections.Generic;
using UnityEngine;
using Core.Fight;
using Core.Common;

namespace Core
{
    [RequireComponent(typeof(HexMapGenerator))]
    public class FightManager : MonoBehaviour
    {
        private static FightManager instance;

        [SerializeField]
        public MapGenerationParams mapGenerationParams;

        private void Awake()
        {
            instance = this;
        }

        public static void InitFight(List<UnitStats> units)
        {
            instance.GenerateMap();

            foreach (UnitStats unit in units)
                instance.PlaceEnemyUnitInRandomPlace(unit);
            
            instance.InitFightQueue();
        }

        private void GenerateMap()
        {
            HexMapGenerator generator = GetComponent<HexMapGenerator>();
            generator.Generate(mapGenerationParams);
        }

        private void PlaceEnemyUnitInRandomPlace(UnitStats unitStats)
        {
            throw new System.NotImplementedException();
        }

        private void InitFightQueue()
        {
            throw new System.NotImplementedException();
        }
    }
}