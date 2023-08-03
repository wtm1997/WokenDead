using System.Collections;
using CommonLogic.StateMachine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YADownloadFilesState: StateBase
    {
        public YADownloadFilesState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Download Files...");
            Entry.Inst.StartCoroutine(BeginDownload());
        }
        
        private IEnumerator BeginDownload()
        {
            YooAssetStateMgr yooAssetStateMgr = _stateMgr as YooAssetStateMgr;
            
            var downloader = yooAssetStateMgr.Downloader;

            // 注册下载回调
            downloader.OnDownloadErrorCallback = (fileName, error) => { };
            downloader.OnDownloadProgressCallback = (totalDownloadCount, currentDownloadCount, totalDownloadBytes, currentDownloadBytes) => { };
            downloader.BeginDownload();
            yield return downloader;

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;

            _stateMgr.TranslationState((int) EYooAssetState.Done);
        }
    }
}