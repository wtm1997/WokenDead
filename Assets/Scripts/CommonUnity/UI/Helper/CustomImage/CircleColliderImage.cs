using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity.UI
{

    public class CircleColliderImage : Image
    {
        [SerializeField] public int range;

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out point);
            return point.magnitude <= range;
        }
    }

}
