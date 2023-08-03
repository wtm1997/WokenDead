public static class UsingCodeDefine
{
    public static string Using =
        @"
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
#代码#
";
}

public static class PanelGenCodeDefine
{
    public static string Class =
        @"
public partial class #类名# : UIPanelBase
{
    #region UIElement声明
    #声明#
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        #初始化声明#
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        #注册事件#
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        #反注册事件#
        #endregion
    }
}
";
}

public static class ViewGenCodeDefine
{
    public static string Class =
        @"
public partial class #类名# : UIViewBase
{
    #region UIElement声明
    #声明#
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        #初始化声明#
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        #注册事件#
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        #反注册事件#
        #endregion
    }
}
";
}

public static class PanelViewEditCodeDefine
{
    public static string Class = 
        @"
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public partial class #类名#
{
    public override void InitializeParams()
    {
        base.InitializeParams();
        layerId = UILayerId.Panel;
        // TODO ： 如果没有view，下面这行删除
        ViewNameList ??= new List<string>();
    }
    
    public override IEnumerator Prepare()
    {
        yield break;
    }
    
    public override void onCreate()
    {
        base.onCreate();
    }

    public override void onShow()
    {
        base.onShow();
    }

    public override void onHide()
    {
        base.onHide();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    
    #region 事件实现  
#事件#
    #endregion
}";
}


public static class AdapterCodeDefine
{
    public static string Class =
        @"
public partial class #类名# : BaseScrollDelegateAdapter
{
    // 设置每个Cell大小
    public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 0;
    }
    
    // 获取CellView，可以看下Mission和Main是怎么实现的
    public override EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        return null;
    }
    
    // CellPrefabNameList Add之后，base中实现了初始化Cell的操作
    public override void OnInitCellPrefab()
    {
        base.OnInitCellPrefab();
    }
    
    #region 生命周期
    public override void OnCreate()
    {
        base.OnCreate();
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    #endregion
}
";
}

public static class CellGenCodeDefine
{
    public static string Class =
        @"
public partial class #类名# : EnhancedScrollerCellView
{
    #region UIElement声明
    #声明#
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        #初始化声明#
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        #注册事件#
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        #反注册事件#
        #endregion
    }
}
";
}

public static class CellEditCodeDefine
{
    public static string Class = 
        @"
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public partial class #类名#
{
    public override void onCreate()
    {
        base.onCreate();
    }

    public override void onShow()
    {
        base.onShow();
    }

    public override void onHide()
    {
        base.onHide();
    }

    public override void onDestroy()
    {
        base.onDestroy();
    }

    #region 事件实现
#事件#
    #endregion
}";
}