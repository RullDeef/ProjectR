using UnityEngine;

namespace Core.Fight
{
    public static class FightUnitFactory
    {
        public static Transform CreatePlayableUnit(Core.Common.UnitStats unitStats)
        {
            Transform playableUnit = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<Transform>();

            FightUnitController controller = playableUnit.gameObject.AddComponent<FightUnitController>();
            controller.stats = unitStats;
            controller.GetComponent<FightUnitMotor>().controller = controller;

            return playableUnit;
        }

        public static Transform CreatePlayerUnit(Core.Common.UnitStats unitStats)
        {
            Transform playableUnit = GameObject.CreatePrimitive(PrimitiveType.Capsule).GetComponent<Transform>();

            FightPlayerController controller = playableUnit.gameObject.AddComponent<FightPlayerController>();
            controller.stats = unitStats;
            controller.GetComponent<FightUnitMotor>().controller = controller;

            return playableUnit;
        }
    }
}
