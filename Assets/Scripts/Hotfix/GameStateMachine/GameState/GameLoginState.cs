using CommonLogic.StateMachine;
using Hotfix.UI;

namespace Hotfix.GameState
{
    public class GameLoginState : StateBase
    {
        public GameLoginState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            // Log.Info($"【Game状态机】【Login】 开始运行...");
            UIManager.Inst.Show(UIPanelName.UILogin);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // Log.Info($"【Game状态机】【Login】 运行中...");
        }

        public override void OnLeave()
        {
            base.OnLeave();
            // Log.Info($"【Game状态机】【Login】 离开...");
            UIManager.Inst.Hide(UIPanelName.UILogin);
        }
    }
}