using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CommonUnity.UI
{



    public class AreaImage : Image
    {
        void Update()
        {
            SetAllDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (transform.childCount == 0)
            {
                return;
            }

            Color32 color32 = color;
            vh.Clear();

            // 几何图形的顶点，本例中根据子节点坐标确定顶点
            foreach (Transform child in transform)
            {
                vh.AddVert(child.localPosition, color32, new Vector2(0f, 0f));
            }

            // 几何图形中的三角形
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        private PolygonCollider2D _polygon;

        PolygonCollider2D Polygon
        {
            get
            {
                if (_polygon == null)
                {
                    _polygon = GetComponent<PolygonCollider2D>();
                }
                return _polygon;
            }
        }

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector3 point);
            return Polygon.OverlapPoint(point);
        }
    }

}
