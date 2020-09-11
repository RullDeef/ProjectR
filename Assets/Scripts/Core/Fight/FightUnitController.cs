using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.FSM;

namespace Core.Fight
{
    [Serializable]
    public class FightUnitShape
    {
        public List<Vector3Int> cells;
    }

    [RequireComponent(typeof(FightUnitMotor))]
    public class FightUnitController : MonoBehaviour
    {
        public HexCell cell; // current cell

        [SerializeField]
        public FightUnitShape shape;

        private StateMachine fsm;

        protected FightUnitMotor motor;

        void Awake()
        {
            fsm = new StateMachine();
            motor = GetComponent<FightUnitMotor>();

            fsm.AddState("wait", () => { });
            fsm.AddState("going", () => { });

            fsm.AddTransition("going", "wait", () => !motor.isMoving);

            fsm.SetActiveState("wait");
        }

        public void GoToCell(HexCell targetCell)
        {
            HexPath path = cell.hexMap.ConstructPath(cell, targetCell);
            motor.MoveWithPath(path);
        }

        public bool CanBePlacedInCell(HexCell targetCell)
        {
            throw new NotImplementedException();
        }

        public List<HexCell> GetOcupiedCells()
        {
            throw new NotImplementedException();
        }
    }
}