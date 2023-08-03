using System;
using System.Collections;
using CommonLogic;
using CommonLogic.Resource;
using Entitas;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

public class Entry : MonoBehaviour
{
    public static Entry Inst;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Inst = this;
        InitLog();
    }

    // Start is called before the first frame update
    void Start()
    {
        YooAssets.Initialize();
        GameObject obj = Resources.Load<GameObject>("UIEntry");
        GameObject instObj = Object.Instantiate(obj);
        
        YooAssetStateMgr yooAssetStateMgr = new YooAssetStateMgr();
        yooAssetStateMgr.Init();
        yooAssetStateMgr.Start();
        yooAssetStateMgr.OnHotfixOver = () =>
        {
            Destroy(instObj);
            AssetOperationHandle handle = YooAssets.LoadAssetSync<GameObject>("GameGlobal");
            GameObject obj = handle.AssetObject as GameObject;
            Instantiate(obj);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        YooAssets.Destroy();
        ReleaseLog();
    }
    
    private void InitLog()
    {
        Log.onDebug += Debug.Log;
        Log.onError += Debug.LogError;
        Log.onInfo += Debug.Log;
    }

    private void ReleaseLog()
    {
        Log.onDebug -= Debug.Log;
        Log.onError -= Debug.LogError;
        Log.onInfo -= Debug.Log;
    }
}
