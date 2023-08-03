using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CommonUnity.Extension
{
    public static partial class Extension //Transform
    {
        public static void SetParentGracefully(this Transform selfTrans,
                                               Transform parent, bool worldPositionStays = true)
        {
            if (selfTrans.parent != parent)
                selfTrans.SetParent(parent, worldPositionStays);
        }

        public static void SetLocalY(this Transform selfTrans, float value)
        {
            var localPos = selfTrans.localPosition;
            localPos.y = value;
            selfTrans.localPosition = localPos;
        }

        public static void ResetAll(this Transform selfTrans)
        {
            selfTrans.localPosition = Vector3.zero;
            selfTrans.localEulerAngles = Vector3.zero;
            selfTrans.localScale = Vector3.one;
        }

        public static void SetParentAndResetAll(this Transform selfTrans,
                                                Transform parent, bool worldPositionStays = true)
        {
            selfTrans.SetParentGracefully(parent, worldPositionStays);
            selfTrans.localPosition = Vector3.zero;
            selfTrans.localEulerAngles = Vector3.zero;
            selfTrans.localScale = Vector3.one;
        }
    }
}
