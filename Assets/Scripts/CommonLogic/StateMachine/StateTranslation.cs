using System;

namespace CommonLogic.StateMachine
{
    public class StateTranslation : ICondition
    {
        protected StateBaseMgr _stateMgr;
        
        public int FromState;
        public int ToState;

        public StateTranslation(StateBaseMgr stateMgr)
        {
            this._stateMgr = stateMgr;
        }
        
        public virtual bool Satisfied()
        {
            return false;
        }

        public virtual void Init(int fromState, int toState)
        {
            FromState = fromState;
            ToState = toState;
        }

        public virtual void Release()
        {
        }

        public virtual void Reset()
        {
            
        }
    }
}