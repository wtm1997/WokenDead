using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YADoneState : StateBase
    {
        public YADoneState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _stateMgr.Release();
        }
    }
}