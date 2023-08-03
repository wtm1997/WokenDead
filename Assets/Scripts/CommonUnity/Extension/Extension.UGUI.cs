using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity.Extension
{
    public static partial class Extension //UGUI
    {
        public static void SetR(this Graphic graphic, float value)
        {
            var col = graphic.color;
            col.r = value;
            graphic.color = col;
        }
        
        public static void SetAlpha(this Graphic graphic, float value)
        {
            var col = graphic.color;
            col.a = value;
            graphic.color = col;
        }
    }
}