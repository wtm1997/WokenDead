
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using System;
using Hotfix.UI;
using TMPro;
using UnityEngine;

[UIBaseHandler(UIViewName.Func)]
public partial class FuncView
{
    public override void InitializeParams()
    {
        base.InitializeParams();
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

        public void OnClickEquipBtn(GameObject obj)
        {
            
        }
        
        
    #endregion
}
