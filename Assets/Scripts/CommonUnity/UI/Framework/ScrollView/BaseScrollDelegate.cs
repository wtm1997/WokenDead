using System;
using EnhancedUI.EnhancedScroller;
using UnityEditor;
using UnityEngine;

public class BaseScrollDelegate : MonoBehaviour, IEnhancedScrollerDelegate
{
    public BaseScrollDelegateAdapter Adapter;

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if (Adapter == null) return 0;
        return Adapter.ItemCount;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return Adapter.GetCellViewSize(scroller, dataIndex);
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        return Adapter.GetCellView(scroller, dataIndex, cellIndex);
    }
}