using System.Collections.Generic;
using UnityEngine;
using Core.Fight;
using Core.Common;
using System.Linq;

namespace Core
{
    [RequireComponent(typeof(HexMapGenerator))]
    public class FightManager : MonoBehaviour
    {
        private static FightManager instance;

        [SerializeField]
        public MapGenerationParams mapGenerationParams;
        public HexMap currentMap;


        // for debugging 2 hardcoded units - player and enemy box
        // first unit must always be player stats!!
        public List<UnitStats> startingUnits;


        public List<Transform> playableUnits;

        [SerializeField]
        public ATBScale atbScale;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            // for debugging
            InitFight(new List<UnitStats>(startingUnits));
        }

        public static void InitFight(List<UnitStats> fightingUnits)
        {
            // instance.startingUnits = fightingUnits;

            instance.GenerateMap();

            // place player unit and enemy units
            instance.PlacePlayerUnit(fightingUnits[0]);
            for (int i = 1; i < fightingUnits.Count; i++)
                instance.PlaceEnemyUnitInRandomPlace(fightingUnits[i]);
            
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

        private void PlacePlayerUnit(UnitStats unitStats)
        {
            Transform playableUnit = FightUnitFactory.CreatePlayerUnit(unitStats);

            HexCell cell = currentMap.GetRandomFreeCell();
            cell.controller = playableUnit.GetComponent<FightUnitController>();
            playableUnit.GetComponent<FightUnitController>().cell = cell;
            playableUnit.position = cell.transform.position;

            // add playable to list of playables
            playableUnits.Add(playableUnit);

            // add it to game manager tracking
            GameManager.SetPlayer(playableUnit);
        }

        private void PlaceEnemyUnitInRandomPlace(UnitStats unitStats)
        {
            Transform playableUnit = FightUnitFactory.CreatePlayableUnit(unitStats);

            HexCell cell = currentMap.GetRandomFreeCell();
            cell.controller = playableUnit.GetComponent<FightUnitController>();
            playableUnit.GetComponent<FightUnitController>().cell = cell;
            playableUnit.position = cell.transform.position;
            
            // add playable to list of playables
            playableUnits.Add(playableUnit);
        }

        private void InitFightQueue()
        {
            atbScale = new ATBScale(playableUnits.ConvertAll(playable => playable.GetComponent<FightUnitController>().stats));
        }

        private Coroutine MakeCurrentUnitMove()
        {
            UnitStats unitStats = atbScale.GetCurrentUnitStats();
            Transform playableUnit = playableUnits.Where(unit => unit.GetComponent<FightUnitController>().stats == unitStats).First();

            FightUnitController controller = playableUnit.GetComponent<FightUnitController>();

            Debug.Log("Preparing for unit to move...");
            return StartCoroutine(controller.MakeMove());
        }

        private System.Collections.IEnumerator FightLoopCoroutine()
        {
            bool endFight = false;
            while (!endFight)
            {
                yield return MakeCurrentUnitMove();

                // update ATB scale
                atbScale.UpdateActiveUnits(playableUnits.ConvertAll(playable => playable.GetComponent<FightUnitController>().stats));
                atbScale.PropagateScale();

                yield return new WaitForEndOfFrame();
            }

            yield return null; 
        }
    }
}