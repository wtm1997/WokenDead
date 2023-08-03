
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public partial class MainSkillView : UIViewBase
{
    #region UIElement声明
    
    [NonSerialized]
    public Image Skill0Img;
    [NonSerialized]
    public Image Skill1Img;
    [NonSerialized]
    public Image Skill2Img;
    [NonSerialized]
    public Image Skill3Img;
    [NonSerialized]
    public Image Skill4Img;
    [NonSerialized]
    public Image Skill5Img;
    [NonSerialized]
    public Image Skill6Img;
    #endregion

    public override void BindUI()
    {
        base.BindUI();

        #region 初始化UIElement
        
        Skill0Img = this.transform.Find("t_Skill0").GetComponent<Image>();
        Skill1Img = this.transform.Find("t_Skill1").GetComponent<Image>();
        Skill2Img = this.transform.Find("t_Skill2").GetComponent<Image>();
        Skill3Img = this.transform.Find("t_Skill3").GetComponent<Image>();
        Skill4Img = this.transform.Find("t_Skill4").GetComponent<Image>();
        Skill5Img = this.transform.Find("t_Skill5").GetComponent<Image>();
        Skill6Img = this.transform.Find("t_Skill6").GetComponent<Image>();
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

