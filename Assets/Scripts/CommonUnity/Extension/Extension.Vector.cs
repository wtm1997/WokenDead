using UnityEngine;

namespace CommonUnity.Extension
{
    public static partial class Extension //Vector
    {
        public static Vector3 ToV3(this Vector2 pos)
        {
            return new Vector3(pos.x, pos.y, 0f);
        }

        public static Vector2 ParseToV2(this string value)
        {
            var valueArr = value.Split('_');
            var x = float.Parse(valueArr[0]);
            var y = float.Parse(valueArr[1]);
            return new Vector2(x, y);
        }

        public static Vector3 ParseToV3(this string value)
        {
            var valueArr = value.Split('_');
            var x = float.Parse(valueArr[0]);
            var y = float.Parse(valueArr[1]);
            var z = float.Parse(valueArr[2]);
            return new Vector3(x, y, z);
        }

        public static Vector2 Clamp(this Vector2 pos, Vector2 min, Vector2 max)
        {
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            return pos;
        }

        public static Vector2 Clamp(this Vector2 pos, float minX, float maxX, float minY, float maxY)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            return pos;
        }

        public static Vector3 Clamp(this Vector3 pos, Vector3 min, Vector3 max)
        {
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            pos.z = Mathf.Clamp(pos.z, min.z, max.z);
            return pos;
        }
    }
}