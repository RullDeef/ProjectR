using System;
using System.Collections.Generic;

namespace Utils.FSM
{
    [System.Serializable]
    public class StateAlreadyExistsException : System.Exception
    {
        public StateAlreadyExistsException() { }
        public StateAlreadyExistsException(string message) : base(message) { }
        public StateAlreadyExistsException(string message, System.Exception inner) : base(message, inner) { }
        protected StateAlreadyExistsException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class StateDoesNotExistException : System.Exception
    {
        public StateDoesNotExistException() { }
        public StateDoesNotExistException(string message) : base(message) { }
        public StateDoesNotExistException(string message, System.Exception inner) : base(message, inner) { }
        protected StateDoesNotExistException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class ActiveStateNotSetException : System.Exception
    {
        public ActiveStateNotSetException() { }
        public ActiveStateNotSetException(string message) : base(message) { }
        public ActiveStateNotSetException(string message, System.Exception inner) : base(message, inner) { }
        protected ActiveStateNotSetException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class StateMachine
    {
        private List<State> states = new List<State>();
        private List<Transition> anyTransitions = new List<Transition>();

        private State activeState = null;

        public void AddState(string name, Action action)
        {
            foreach (State st in states)
                if (st.name == name)
                    throw new StateAlreadyExistsException();
            
            State state = new State();
            state.name = name;
            state.action = action;
            states.Add(state);
        }

        public void AddTransition(string stateFrom, string stateTo, Func<bool> condition)
        {
            Transition transition = new Transition();
            transition.stateTo = GetState(stateTo);
            transition.condition = condition;

            GetState(stateFrom).transitions.Add(transition);
        }

        public void AddAnyTransition(string stateTo, Func<bool> condition)
        {
            Transition transition = new Transition();
            transition.stateTo = GetState(stateTo);
            transition.condition = condition;

            anyTransitions.Add(transition);
        }

        public void SetActiveState(string name)
        {
            activeState = GetState(name);
        }

        public void Update()
        {
            if (activeState == null)
                throw new ActiveStateNotSetException();

            activeState.action();

            foreach (Transition transition in anyTransitions)
            {
                if (transition.condition())
                {
                    activeState = transition.stateTo;
                    return;
                }
            }

            foreach (Transition transition in activeState.transitions)
            {
                if (transition.condition())
                {
                    activeState = transition.stateTo;
                    return;
                }
            }
        }

        private State GetState(string name)
        {
            foreach (State state in states)
                if (state.name == name)
                    return state;
            throw new StateDoesNotExistException();
        }

        private class State
        {
            public string name;
            public Action action;

            public List<Transition> transitions;
        }

        private class Transition
        {
            public State stateTo;
            public Func<bool> condition;
        }
    }
}