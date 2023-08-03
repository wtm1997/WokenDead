
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public partial class UILoginPanel : UIPanelBase
{
    #region UIElement声明
    
    [NonSerialized]
    public Image LoginImg;
    [NonSerialized]
    public Button LoginBtn;
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        
        LoginImg = this.transform.Find("Bg/t_Login").GetComponent<Image>();
        LoginBtn = this.transform.Find("Bg/t_Login").GetComponent<Button>();
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        
        UIEventListener.OnClick(LoginBtn.gameObject).AddListener(OnClickLoginBtn);
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        
        UIEventListener.OnClick(LoginBtn.gameObject).RemoveListener(OnClickLoginBtn);
        #endregion
    }
}

