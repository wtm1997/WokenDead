using CommonLogic.StateMachine;

namespace CommonLogic.Resource
{
    public class YADownloadOverState : StateBase
    {
        public YADownloadOverState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Download Over...");
            _stateMgr.TranslationState((int) EYooAssetState.ClearCache);
        }
    }
}