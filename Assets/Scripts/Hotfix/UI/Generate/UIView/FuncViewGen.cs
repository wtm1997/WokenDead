
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public partial class FuncView : UIViewBase
{
    #region UIElement声明
    
    [NonSerialized]
    public Image EquipImg;
    [NonSerialized]
    public Button EquipBtn;
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        
        EquipImg = this.transform.Find("t_Equip").GetComponent<Image>();
        EquipBtn = this.transform.Find("t_Equip").GetComponent<Button>();
        #endregion
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        #region 注册事件
        
        UIEventListener.OnClick(EquipBtn.gameObject).AddListener(OnClickEquipBtn);
        #endregion
    }
    
    public override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        #region 反注册事件
        
        UIEventListener.OnClick(EquipBtn.gameObject).RemoveListener(OnClickEquipBtn);
        #endregion
    }
}

