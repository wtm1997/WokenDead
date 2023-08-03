using System;
using CommonLogic.StateMachine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YooAssetStateMgr : StateBaseMgr
    {
        public ResourceDownloaderOperation Downloader { set; get; }
        public Action OnHotfixOver = null;

        public override void Init()
        {
            base.Init();
            AddState((int) EYooAssetState.Initialize, new YAInitializeState(this), true);
            AddState((int) EYooAssetState.UpdateVersion, new YAUpdateVersionState(this));
            AddState((int) EYooAssetState.UpdateManifest, new YAUpdateManifestState(this));
            AddState((int) EYooAssetState.CreateDownloader, new YACreateDownloaderState(this));
            AddState((int) EYooAssetState.DownloadFiles, new YADownloadFilesState(this));
            AddState((int) EYooAssetState.DownloadOver, new YADownloadOverState(this));
            AddState((int) EYooAssetState.Done, new YADoneState(this));
            AddState((int) EYooAssetState.ClearCache, new YAClearCacheState(this));
            
        }

        public override void Release()
        {
            base.Release();
            OnHotfixOver?.Invoke();
        }
    }
}