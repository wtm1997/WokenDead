using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity.UI
{

    [RequireComponent(typeof(PolygonCollider2D))]
    public class CustomColliderImage : Image
    {
        private PolygonCollider2D polygon;

        PolygonCollider2D Polygon
        {
            get
            {
                if (polygon == null)
                {
                    polygon = GetComponent<PolygonCollider2D>();
                }
                return polygon;
            }
        }

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector3 point);
            return Polygon.OverlapPoint(point);
        }
    }

}
