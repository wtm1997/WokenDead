using CommonLogic.StateMachine;

namespace CommonLogic.Resource
{
    public class YAPrepareState : StateBase
    {
        public YAPrepareState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Prepare...");
        }
    }
}