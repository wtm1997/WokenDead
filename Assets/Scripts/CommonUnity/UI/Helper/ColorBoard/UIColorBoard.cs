using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIColorBoard : MonoBehaviour,
    IPointerDownHandler, IDragHandler
{
    private RawImage _rawImage;
    private Texture2D _tex2d;
    private Color[,] _arrayColor;

    //像素宽度高度256(默认值)
    public int texPixelLength = 256;
    public int texPixelHeight = 256;

    //自身的Transform
    private RectTransform _rt;
    //颜色聚焦点的圆圈
    public RectTransform circleRect;

    //公共组件
    public Slider slider;
    public UIColorBoardBar colorBoardBar;

    public delegate void ColorChangeDelegate(Color color);

    public ColorChangeDelegate onColorChanged;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _rt = GetComponent<RectTransform>();

        var sizeDelta = _rt.sizeDelta;
        texPixelLength = (int)sizeDelta.x;
        texPixelHeight = (int)sizeDelta.y;

        //初始化颜色数组
        _arrayColor = new Color[texPixelLength, texPixelHeight];
        //创建一个固定长宽的Texture
        _tex2d = new Texture2D(texPixelLength, texPixelHeight, TextureFormat.RGB24, true);
        //组件赋值图片
        _rawImage.texture = _tex2d;
        _rawImage.texture.wrapMode = TextureWrapMode.Clamp;

        //初始化设置板子的颜色为红色
        SetColorPanel(Color.red);

        slider.onValueChanged.AddListener(OnCRGBValueChanged);
    }

    //颜色放生变化的监听
    void OnCRGBValueChanged(float value)
    {
        Color endColor = colorBoardBar.GetColorBySliderValue(value);
        SetColorPanel(endColor);

        var color = GetColorByPosition(circleRect.anchoredPosition);
        onColorChanged?.Invoke(color);
    }

    //设置板子的颜色
    public void SetColorPanel(Color endColor)
    {
        Color[] calcArray = CalcArrayColor(endColor);

        //给颜色板子填入颜色，并且应用
        _tex2d.SetPixels(calcArray);
        _tex2d.Apply();
    }

    //通过一个最终颜色值，计算板子上所有像素点应该的颜色，并返回一个数组
    Color[] CalcArrayColor(Color endColor)
    {
        //计算最终值和白色的差值在水平方向的平均值，用于计算水平每个像素点的色值
        Color value = (endColor - Color.white) / (texPixelLength - 1);
        for (int i = 0; i < texPixelLength; i++)
        {
            _arrayColor[i, texPixelHeight - 1] = Color.white + value * i;
        }
        // 同理，垂直方向
        for (int i = 0; i < texPixelLength; i++)
        {
            value = (_arrayColor[i, texPixelHeight - 1] - Color.black) / (texPixelHeight - 1);
            for (int j = 0; j < texPixelHeight; j++)
            {
                _arrayColor[i, j] = Color.black + value * j;
            }
        }
        //返回一个数组，保存了所有颜色色值
        List<Color> listColor = new List<Color>();
        for (int i = 0; i < texPixelHeight; i++)
        {
            for (int j = 0; j < texPixelLength; j++)
            {
                listColor.Add(_arrayColor[j, i]);
            }
        }

        return listColor.ToArray();
    }

    Color GetColorByPosition(Vector2 pos)
    {
        Texture2D tempTex2d = (Texture2D)_rawImage.texture;
        Color getColor = tempTex2d.GetPixel((int)pos.x, (int)pos.y);
        return getColor;
    }

    Vector2 GetClampPosition(Vector2 touchPos)
    {
        Vector2 vector2 = new Vector2(touchPos.x, touchPos.y);
        vector2.x = Mathf.Clamp(vector2.x, 0.001f, _rt.sizeDelta.x);
        vector2.y = Mathf.Clamp(vector2.y, 0.001f, _rt.sizeDelta.y);
        return vector2;
    }

    public void SetCicleOffset(float s, float v)
    {
        var pos = new Vector2(texPixelLength * s, texPixelHeight * v);
        circleRect.anchoredPosition = pos;
    }

    //点击事件
    public void OnPointerDown(PointerEventData eventData)
    {
        //将UGUI的坐标转为世界坐标  
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rt,
            eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            circleRect.position = worldPos;
        }

        circleRect.anchoredPosition = GetClampPosition(circleRect.anchoredPosition);

        var color = GetColorByPosition(circleRect.anchoredPosition);
        onColorChanged?.Invoke(color);
    }

    //拖拽事件
    public void OnDrag(PointerEventData eventData)
    {
        //将UGUI的坐标转为世界坐标  
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rt,
            eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            circleRect.position = worldPos;
        }

        circleRect.anchoredPosition = GetClampPosition(circleRect.anchoredPosition);

        var color = GetColorByPosition(circleRect.anchoredPosition);
        onColorChanged?.Invoke(color);
    }
}
