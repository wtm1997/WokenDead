using UnityEngine;

namespace CommonUnity.Extension
{

    public static partial class Extension //RectTransform
    {
        public static Vector2 GetCenterOffset(this RectTransform trans)
        {
            return Vector2.Scale(trans.rect.size, Vector2.one * 0.5f - trans.pivot);
        }

        public static Vector2 GetCenterPosition(this RectTransform trans)
        {
            return (Vector2)trans.position + trans.GetCenterOffset();
        }

        public static Vector2 GetCenterLocalPosition(this RectTransform trans)
        {
            return (Vector2)trans.localPosition + trans.GetCenterOffset();
        }

        public static void SetCenterPosition(this RectTransform trans, Vector2 position)
        {
            trans.position = position + trans.GetCenterOffset();
        }

        public static void SetCenterLocalPosition(this RectTransform trans, Vector2 position)
        {
            trans.localPosition = (Vector3)(position + trans.GetCenterOffset());
        }
    }

}
