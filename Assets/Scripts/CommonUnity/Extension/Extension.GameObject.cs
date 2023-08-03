using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CommonUnity.Extension
{

    public static partial class Extension //GameObject
    {
        public static GameObject SetActiveGracefully(this GameObject selfObj, bool value)
        {
            if (selfObj.activeSelf != value)
                selfObj.SetActive(value);
            return selfObj;
        }

        public static void DestroyAllChild(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                Object.Destroy(child);
            }
        }

        public static GameObject SetLayer(this GameObject selfObj, int layer)
        {
            selfObj.layer = layer;
            foreach (Transform child in selfObj.transform)
            {
                SetLayer(child.gameObject, layer);
            }
            return selfObj;
        }

        public static T EnsureComponent<T>(this Component selfObj) where T : Component
        {
            var comp = selfObj.GetComponent<T>();
            return comp ? comp : selfObj.gameObject.AddComponent<T>();
        }

        public static T EnsureComponent<T>(this GameObject selfObj) where T : Component
        {
            var comp = selfObj.GetComponent<T>();
            return comp ? comp : selfObj.AddComponent<T>();
        }

        public static Component EnsureComponent(this GameObject selfObj, Type type)
        {
            var comp = selfObj.GetComponent(type);
            return comp ? comp : selfObj.AddComponent(type);
        }
    }

}
