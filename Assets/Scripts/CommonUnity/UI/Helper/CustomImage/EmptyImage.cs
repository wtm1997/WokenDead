using UnityEngine.UI;

namespace CommonUnity.UI
{

    public class EmptyImage : Image
    {
        public bool m_debug = false;

        protected override void OnPopulateMesh(VertexHelper vbo)
        {
            vbo.Clear();

#if UNITY_EDITOR
            if (m_debug)
            {
                base.OnPopulateMesh(vbo);
            }
#endif
        }
    }

}
