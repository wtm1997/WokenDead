using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity
{

    [AddComponentMenu("UI/CustomEffects/Gradient")]
    public class Gradient : BaseMeshEffect
    {
        [SerializeField]
        private Color32 topColor = Color.white;
        [SerializeField]
        private Color32 bottomColor = Color.black;

        public void SetGradientColor(Color32 c1, Color32 c2)
        {
            topColor = c1;
            bottomColor = c2;
            if (graphic != null)
            {
                graphic.SetVerticesDirty();
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }
            List<UIVertex> vertexList = new List<UIVertex>();

            vh.GetUIVertexStream(vertexList);

            int count = vertexList.Count;
            if (count > 0)
            {
                float bottomY = vertexList[0].position.y;
                float topY = vertexList[0].position.y;

                for (int i = 1; i < count; i++)
                {
                    float y = vertexList[i].position.y;
                    if (y > topY)
                    {
                        topY = y;
                    }
                    else if (y < bottomY)
                    {
                        bottomY = y;
                    }
                }

                float uiElementHeight = topY - bottomY;

                for (int i = 0; i < count; i++)
                {
                    UIVertex uiVertex = vertexList[i];
                    Color o = uiVertex.color;
                    Color32 b = new Color32((byte)(bottomColor.r * o.r), (byte)(bottomColor.g * o.g), (byte)(bottomColor.b * o.b), (byte)(bottomColor.a * o.a));
                    Color32 t = new Color32((byte)(topColor.r * o.r), (byte)(topColor.g * o.g), (byte)(topColor.b * o.b), (byte)(topColor.a * o.a));

                    uiVertex.color = Color32.Lerp(b, t, (uiVertex.position.y - bottomY) / uiElementHeight);
                    vertexList[i] = uiVertex;
                }
                vh.Clear();
                vh.AddUIVertexTriangleStream(vertexList);
            }
        }
    }

}
