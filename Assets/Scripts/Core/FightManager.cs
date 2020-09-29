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

        public List<UnitStats> units;
        public ATBScale atbScale;

        private void Awake()
        {
            instance = this;

            // for debugging
            InitFight(new List<UnitStats>());
        }

        public static void InitFight(List<UnitStats> fightingUnits)
        {
            instance.units = fightingUnits;

            instance.GenerateMap();

            foreach (UnitStats unit in fightingUnits)
                instance.PlaceEnemyUnitInRandomPlace(unit);

            // for debugging 2 hardcoded units - player and enemy box
            // ...
            
            instance.InitFightQueue();
            instance.StartCoroutine(instance.FightLoopCoroutine());
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
            atbScale = new ATBScale(units);
        }

        private void MakeCurrentUnitMove()
        {
            UnitStats unit = atbScale.GetCurrentUnit();

            // do actions with unit:
            // 1. get controller component
            // 2. supply it with EndAction callback function
            // 3. end this function
        }

        private System.Collections.IEnumerator FightLoopCoroutine()
        {
            bool endFight = false;
            while (!endFight)
            {
                MakeCurrentUnitMove();

                yield return new WaitForEndOfFrame();
            }

            yield return null; 
        }
    }
}