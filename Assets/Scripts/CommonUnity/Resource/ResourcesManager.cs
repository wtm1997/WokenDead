using System;
using System.Collections;
using CommonLogic;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

namespace CommonUnity.Resource
{
    public class ResourcesManager : Singleton<ResourcesManager>
    {
        public GameObject LoadObj(string address)
        {
            GameObject obj = LoadAssets<GameObject>(address);
            GameObject instObj = Object.Instantiate(obj);
            return instObj;
        }
        
        public T LoadAssets<T>(string address) where T : UnityEngine.Object
        {
            AssetOperationHandle handle =  YooAssets.LoadAssetSync<T>(address);
            T t = handle.AssetObject as T;
            return t;
        }

        public IEnumerator LoadAssetsAsync<T>(string address, Action<T> callBack) where T : Object
        {
            AssetOperationHandle handle =  YooAssets.LoadAssetAsync<T>(address);
            yield return handle;

            T t = handle.AssetObject as T;
            callBack?.Invoke(t);
        }
    }
}