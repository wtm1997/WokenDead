using System.Collections;
using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YAUpdateVersionState : StateBase
    {
        public YAUpdateVersionState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Update Version...");
            Entry.Inst.StartCoroutine(GetStaticVersion());
        }
        
        private IEnumerator GetStaticVersion()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            var package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageVersionAsync();
            yield return operation;

            if (operation.Status == EOperationStatus.Succeed)
            {
                _stateMgr.TranslationState((int) EYooAssetState.UpdateManifest);
            }
            else
            {
                Log.Error(operation.Error);
            }
        }
    }
}