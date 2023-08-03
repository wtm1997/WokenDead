
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using System;
using CommonUnity.Timer;
using DG.Tweening;
using Hotfix.UI;
using TMPro;
using UnityEngine;

[UIBaseHandler(UIPanelName.UILoading)]
public partial class UILoadingPanel
{
    public override void InitializeParams()
    {
        base.InitializeParams();
        layerId = UILayerId.Panel;
        NeedBackToLastUI = false;
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
        Sequence seq = DOTween.Sequence();
        seq.Append(CircleImg.transform.DOLocalRotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360));
        seq.SetLoops(-1);
        seq.SetUpdate(true);
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
