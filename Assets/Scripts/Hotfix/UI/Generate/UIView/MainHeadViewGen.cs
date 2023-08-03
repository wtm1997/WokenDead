
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public partial class MainHeadView : UIViewBase
{
    #region UIElement声明
    
    [NonSerialized]
    public TextMeshProUGUI LvlTxt;
    [NonSerialized]
    public TextMeshProUGUI ExpTxt;
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        
        LvlTxt = this.transform.Find("t_Lvl").GetComponent<TextMeshProUGUI>();
        ExpTxt = this.transform.Find("t_Exp").GetComponent<TextMeshProUGUI>();
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        
        #endregion
    }
}

