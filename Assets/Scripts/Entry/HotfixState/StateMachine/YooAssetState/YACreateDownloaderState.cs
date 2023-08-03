using System.Collections;
using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YACreateDownloaderState : StateBase
    {
        public YACreateDownloaderState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug("YooAsset Create Downloader...");
            Entry.Inst.StartCoroutine(CreateDownloader());
        }
        
        IEnumerator CreateDownloader()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            YooAssetStateMgr yooAssetStateMgr = _stateMgr as YooAssetStateMgr;
            yooAssetStateMgr.Downloader = downloader;

            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("Not found any download files !");
                _stateMgr.TranslationState((int) EYooAssetState.DownloadOver);
            }
            else
            {
                //A total of 10 files were found that need to be downloaded
                Debug.Log($"Found total {downloader.TotalDownloadCount} files that need download ！");

                // 发现新更新文件后，挂起流程系统
                // 注意：开发者需要在下载前检测磁盘空间不足
                int totalDownloadCount = downloader.TotalDownloadCount;
                long totalDownloadBytes = downloader.TotalDownloadBytes;
            }
        }
    }
}