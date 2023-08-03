
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Hotfix.UI;
using TMPro;
using UnityEngine;

[UIBaseHandler(UIPanelName.UIMain)]
public partial class UIMainPanel
{
    public override void InitializeParams()
    {
        base.InitializeParams();
        layerId = UILayerId.Panel;
        ViewNameList ??= new List<string>();
        ViewNameList.Add(UIViewName.Func);
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

    #endregion
}
