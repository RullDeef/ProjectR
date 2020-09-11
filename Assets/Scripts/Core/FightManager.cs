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
        public HexMap currentMap;

        private void Awake()
        {
            instance = this;

            // for debugging
            InitFight(new List<UnitStats>());
        }

        public static void InitFight(List<UnitStats> units)
        {
            instance.GenerateMap();

            foreach (UnitStats unit in units)
                instance.PlaceEnemyUnitInRandomPlace(unit);

            // for debugging 2 hardcoded units - player and enemy box
            // ...
            
            instance.InitFightQueue();
        }

        public static HexMap GetMap()
        {
            return instance.currentMap;
        }

        private void GenerateMap()
        {
            HexMapGenerator generator = GetComponent<HexMapGenerator>();
            // generator.Generate(mapGenerationParams);

            // for debugging now
            currentMap = generator.LoadFromScene();
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