using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIColorBoardBar : MonoBehaviour,
    IPointerDownHandler, IDragHandler
{
    private RawImage _rawImage;
    private Texture2D _tex2d;
    private Color[,] _arrayColor;

    //长宽
    private int _texPixelWdith = 240;
    private int _texPixelHeight = 21;

    //自身的Transform
    private RectTransform _rt;

    //公共组件
    public Slider slider;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _rawImage = gameObject.GetComponent<RawImage>();

        //初始化颜色和Texture
        _arrayColor = new Color[_texPixelWdith, _texPixelHeight];
        _tex2d = new Texture2D(_texPixelWdith, _texPixelHeight, TextureFormat.RGB24, true);

        //计算颜色
        Color[] calcArray = CalcArrayColor();

        //展示出来
        _tex2d.SetPixels(calcArray);
        _tex2d.Apply();

        _rawImage.texture = _tex2d;
        _rawImage.texture.wrapMode = TextureWrapMode.Clamp;
    }

    //计算色相条上面需要展示的颜色数组
    Color[] CalcArrayColor()
    {
        //计算水平像素的等分增量
        int addValue = (_texPixelWdith - 1) / 3;

        for (int i = 0; i < _texPixelHeight; i++)
        {
            _arrayColor[0, i] = Color.red;
            _arrayColor[addValue, i] = Color.green;
            _arrayColor[addValue + addValue, i] = Color.blue;
            _arrayColor[_texPixelHeight - 1, i] = Color.red;
        }
        Color value = (Color.green - Color.red) / addValue;
        for (int i = 0; i < _texPixelHeight; i++)
        {
            for (int j = 0; j < addValue; j++)
            {
                _arrayColor[j, i] = Color.red + value * j;
            }
        }

        value = (Color.blue - Color.green) / addValue;
        for (int i = 0; i < _texPixelHeight; i++)
        {
            for (int j = addValue; j < addValue * 2; j++)
            {
                _arrayColor[j, i] = Color.green + value * (j - addValue);
            }
        }

        value = (Color.red - Color.blue) / ((_texPixelWdith - 1) - addValue - addValue);
        for (int i = 0; i < _texPixelHeight; i++)
        {
            for (int j = addValue * 2; j < _texPixelWdith - 1; j++)
            {
                _arrayColor[j, i] = Color.blue + value * (j - addValue * 2);
            }
        }

        List<Color> listColor = new List<Color>();
        for (int i = 0; i < _texPixelHeight; i++)
        {
            for (int j = 0; j < _texPixelWdith; j++)
            {
                listColor.Add(_arrayColor[j, i]);
            }
        }

        return listColor.ToArray();
    }

    public Color GetColorBySliderValue(float value)
    {
        float clampValue = Mathf.Clamp(value, 0.001f, 0.999f);
        Color getColor = _tex2d.GetPixel((int)((_texPixelWdith - 1) * clampValue), 0);
        return getColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
(_rt, eventData.position, eventData.pressEventCamera, out Vector2 localPos))
        {
            float width = _rt.sizeDelta.x;
            float currPos = localPos.x + width / 2;
            float currPer = currPos / width;

            slider.value = currPer;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
(_rt, eventData.position, eventData.pressEventCamera, out Vector2 localPos))
        {
            float width = _rt.sizeDelta.x;
            float currPos = localPos.x + width / 2;
            float currPer = currPos / width;

            slider.value = currPer;
        }
    }
}