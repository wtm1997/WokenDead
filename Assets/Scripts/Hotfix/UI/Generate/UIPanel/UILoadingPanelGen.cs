
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public partial class UILoadingPanel : UIPanelBase
{
    #region UIElement声明
    
    [NonSerialized]
    public Image CircleImg;
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        
        CircleImg = this.transform.Find("Bg/t_Circle").GetComponent<Image>();
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

