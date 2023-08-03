namespace CommonLogic.Resource
{
    public enum EYooAssetState
    {
        Prepare,           // 准备
        Initialize,        // 初始化
        UpdateVersion,     // 检查版本更新
        UpdateManifest,    // 检查更新内容
        CreateDownloader,  // 创建下载器
        DownloadFiles,     // 下载文件
        DownloadOver,      // 下载结束
        ClearCache,        // 清理缓存
        Done               // 结束
    }
}