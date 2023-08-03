
using System.Collections;
using Hotfix.GameState;
using Hotfix.UI;
using UnityEngine;

[UIBaseHandler(UIPanelName.UILogin)]
public partial class UILoginPanel
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

        public void OnClickLoginBtn(GameObject obj)
        {
            GameEnterLoadingEvt evt = new GameEnterLoadingEvt();
            evt.FromState = EGameState.GameLogin;
            evt.ToState = EGameState.GameOutside;
            evt.EnterLoading = true;
            EventManager.Inst.Send(evt);
        }
        
        
    #endregion
}
