using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using Object = UnityEngine.Object;

public class BaseScrollDelegateAdapter
{
    public int ItemCount;

    public List<string> CellPrefabNameList;

    public EnhancedScroller Scroller;

    protected Dictionary<string, GameObject> _cellName2CellViewDic;

    public Action RefreshList;

    public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 0;
    }

    public virtual EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        return null;
    }

    public virtual void OnInitCellPrefab()
    {
        if (CellPrefabNameList == null) return;
        _cellName2CellViewDic ??= new Dictionary<string, GameObject>();
        _cellName2CellViewDic.Clear();
        for (int i = 0; i < CellPrefabNameList.Count; i++)
        {
            if (_cellName2CellViewDic.TryGetValue(CellPrefabNameList[i], out GameObject cellViewObj))
            {
                Debug.LogError($"{CellPrefabNameList[i]} has already inst! Pls check reason!");
            }
            else
            {
                GameObject newObj = Resources.Load<GameObject>(CellPrefabNameList[i]);
                _cellName2CellViewDic.Add(CellPrefabNameList[i], newObj);
            }
        }
    }
    
    public virtual void OnCreate(){}
    public virtual void OnShow(){}
    public virtual void OnHide(){}

    public virtual void OnDestroy()
    {
        CellPrefabNameList?.Clear();
        CellPrefabNameList = null;
    }
}