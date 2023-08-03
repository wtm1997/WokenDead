using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity.UI
{

    public class CircleImage : Image
    {
        //分割的片段数
        public int segements = 3;

        /// <summary>
        /// UI元素生成顶点数据时调用
        /// </summary>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            //圆点
            var pivot = rectTransform.pivot;
            var rect = rectTransform.rect;
            Vector2 center = new Vector2((rect.width * (0.5f - pivot.x)), (rect.height * (0.5f - pivot.y)));
            vh.AddVert(CreateUIVertex(center, new Vector2(0.5f, 0.5f), color));

            float radius = rect.width / 2f;
            //每段的弧度
            float radians = Mathf.PI * 2 / segements;
            int count = segements;
            //每一个圆弧上的顶点
            for (int i = 0; i < count; i++)
            {
                float angdeg = radians * i;
                float x = center.x + radius * Mathf.Cos(angdeg);
                float y = center.y + radius * Mathf.Sin(angdeg);
                float uvX = (radius * Mathf.Cos(angdeg) + radius) / (2 * radius);
                float uvY = (radius * Mathf.Sin(angdeg) + radius) / (2 * radius);
                vh.AddVert(CreateUIVertex(new Vector3(x, y), new Vector2(uvX, uvY), color: this.color));
            }

            for (int i = 0; i < count; i++)
            {
                int id0 = 0;
                int id1 = i + 2;
                if (id1 > count)
                {
                    id1 %= count;
                }
                int id2 = i + 1;
                vh.AddTriangle(id0, id1, id2);
            }
        }

        public UIVertex CreateUIVertex(Vector3 position, Vector2 uv0, Color color)
        {
            UIVertex uiVertex = new UIVertex()
            {
                position = position,
                uv0 = uv0,
                color = color
            };

            return uiVertex;
        }
    }

}
