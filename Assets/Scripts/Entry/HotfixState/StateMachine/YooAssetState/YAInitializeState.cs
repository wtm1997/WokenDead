using System.Collections;
using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
    public class YAInitializeState : StateBase
    {
        public YAInitializeState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void OnEnter()
        {
	        base.OnEnter();
	        Log.Debug("YooAsset Init...");
	        Entry.Inst.StartCoroutine(InitPackage());
        }

        private IEnumerator InitPackage()
		{
			yield return new WaitForSeconds(1f);

			var playMode = EPlayMode.EditorSimulateMode;

			// 创建默认的资源包
			string packageName = "DefaultPackage";
			var package = YooAssets.TryGetPackage(packageName);
			if (package == null)
			{
				package = YooAssets.CreatePackage(packageName);
				YooAssets.SetDefaultPackage(package);
			}

			// 编辑器下的模拟模式
			InitializationOperation initializationOperation = null;
			if (playMode == EPlayMode.EditorSimulateMode)
			{
				var createParameters = new EditorSimulateModeParameters();
				createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(packageName);
				initializationOperation = package.InitializeAsync(createParameters);
			}

			// 单机运行模式
			if (playMode == EPlayMode.OfflinePlayMode)
			{
				var createParameters = new OfflinePlayModeParameters();
				// createParameters.DecryptionServices = new GameDecryptionServices();
				initializationOperation = package.InitializeAsync(createParameters);
			}

			// 联机运行模式
			if (playMode == EPlayMode.HostPlayMode)
			{
				// var createParameters = new HostPlayModeParameters();
				// // createParameters.DecryptionServices = new GameDecryptionServices();
				// createParameters.QueryServices = new GameQueryServices();
				// createParameters.DefaultHostServer = GetHostServerURL();
				// createParameters.FallbackHostServer = GetHostServerURL();
				// initializationOperation = package.InitializeAsync(createParameters);
			}

			yield return initializationOperation;
			if (package.InitializeStatus == EOperationStatus.Succeed)
			{
				_stateMgr.TranslationState((int) EYooAssetState.UpdateVersion);
			}
			else
			{
				Log.Error($"{initializationOperation.Error}");
			}
		}

		/// <summary>
		/// 获取资源服务器地址
		/// </summary>
		private string GetHostServerURL()
		{
			//string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
			string hostServerIP = "http://127.0.0.1";
			string gameVersion = "v1.0";

#if UNITY_EDITOR
			if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
				return $"{hostServerIP}/CDN/Android/{gameVersion}";
			else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
				return $"{hostServerIP}/CDN/IPhone/{gameVersion}";
			else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
				return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
			else
				return $"{hostServerIP}/CDN/PC/{gameVersion}";
#else
		if (Application.platform == RuntimePlatform.Android)
			return $"{hostServerIP}/CDN/Android/{gameVersion}";
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			return $"{hostServerIP}/CDN/IPhone/{gameVersion}";
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
			return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{gameVersion}";
#endif
		}

		/// <summary>
		/// 内置文件查询服务类
		/// </summary>
		// private class GameQueryServices : IQueryServices
		// {
		// 	public bool QueryStreamingAssets(string fileName)
		// 	{
		// 		string buildinFolderName = YooAssets.GetStreamingAssetBuildinFolderName();
		// 		return StreamingAssetsHelper.FileExists($"{buildinFolderName}/{fileName}");
		// 	}
		//
		// 	public bool QueryStreamingAssets(string packageName, string fileName)
		// 	{
		// 		string buildinFolderName = YooAssets.GetStreamingAssetBuildinFolderName();
		// 		return StreamingAssetsHelper.FileExists($"{buildinFolderName}/{fileName}");
		// 	}
		//
		// 	public bool QueryDeliveryFiles(string packageName, string fileName)
		// 	{
		// 		throw new System.NotImplementedException();
		// 	}
		//
		// 	public DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName)
		// 	{
		// 		throw new System.NotImplementedException();
		// 	}
		// }
    }
}