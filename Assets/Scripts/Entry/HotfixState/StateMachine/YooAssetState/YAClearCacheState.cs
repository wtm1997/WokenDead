using System;
using System.Collections;
using System.IO;
using CommonLogic.StateMachine;
using UnityEngine;
using YooAsset;

namespace CommonLogic.Resource
{
	public class YAClearCacheState : StateBase
	{
		private ClearUnusedCacheFilesOperation _operation;
		
		public YAClearCacheState(StateBaseMgr stateMgr) : base(stateMgr)
		{

		}

		public override void OnEnter()
		{
			base.OnEnter();
			Log.Debug("YooAsset Clear Cache...");
			var package = YooAsset.YooAssets.GetPackage("DefaultPackage");
			_operation = package.ClearUnusedCacheFilesAsync();
			_operation.Completed += Operation_Completed;
		}
		
		private void Operation_Completed(YooAsset.AsyncOperationBase obj)
		{
			_stateMgr.TranslationState((int) EYooAssetState.Done);
		}

		public override void OnLeave()
		{
			base.OnLeave();
			_operation.Completed -= Operation_Completed;
		}
	}
}