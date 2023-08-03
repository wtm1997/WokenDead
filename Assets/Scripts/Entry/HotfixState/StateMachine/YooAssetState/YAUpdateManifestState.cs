using System.Collections;
using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YAUpdateManifestState : StateBase
    {
        public YAUpdateManifestState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Update Manifest...");
            Entry.Inst.StartCoroutine(UpdateManifest());
        }
        
        private IEnumerator UpdateManifest()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            bool savePackageVersion = true;
            var package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageManifestAsync("1.0.0", savePackageVersion);
            yield return operation;

            if(operation.Status == EOperationStatus.Succeed)
            {
                _stateMgr.TranslationState((int) EYooAssetState.CreateDownloader);
            }
            else
            {
                Log.Error(operation.Error);
            }
        }
    }
}