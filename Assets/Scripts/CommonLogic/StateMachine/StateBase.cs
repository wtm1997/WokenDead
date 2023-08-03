using System.Collections.Generic;
using UnityEngine;

namespace CommonLogic.StateMachine
{
    public class StateBase
    {
        private ListDictionary<int, StateTranslation> _translationType2StateTranslation;

        protected StateBaseMgr _stateMgr;
        
        public StateBase(StateBaseMgr stateMgr)
        {
            _stateMgr = stateMgr;
        }

        public virtual void Init()
        {
            
        }

        public void AddTranslation(int translationType, StateTranslation stateTranslation, int fromState, int toState)
        {
            _translationType2StateTranslation ??= new ListDictionary<int, StateTranslation>();
            if (_translationType2StateTranslation.TryGetValue(translationType,
                    out StateTranslation existStateTranslation))
            {
                Log.Info($"【状态机】当前状态已经添加过这个Translation = {translationType}了，检查是否还需要添加！");
            }
            else
            {
                stateTranslation.Init((int)fromState, (int)toState);
                _translationType2StateTranslation.Add(translationType, stateTranslation);
            }
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnPause()
        {
            
        }

        public virtual void OnResume()
        {
            
        }

        public virtual void OnUpdate()
        {
            if (_translationType2StateTranslation == null) return;
            for (int i = 0; i < _translationType2StateTranslation.Count; i++)
            {
                if (_translationType2StateTranslation.List[i].Satisfied())
                {
                    _stateMgr.TranslationState((int)_translationType2StateTranslation.List[i].ToState);
                    return;
                }
            }
        }

        public virtual void OnLeave()
        {
            if (_translationType2StateTranslation == null) return;
            for (int i = 0; i < _translationType2StateTranslation.Count; i++)
            {
                _translationType2StateTranslation.List[i].Reset();
            }
        }

        public virtual void Release()
        {
            if (_translationType2StateTranslation == null) return;
            for (int i = 0; i < _translationType2StateTranslation.Count; i++)
            {
                _translationType2StateTranslation.List[i].Release();
            }
            _translationType2StateTranslation.Clear();
            _translationType2StateTranslation = null;
        }
    }
}