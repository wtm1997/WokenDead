using CommonLogic.StateMachine;
using Hotfix.UI;

namespace Hotfix.GameState
{
    public class GameOutsideState : StateBase
    {
        public GameOutsideState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            // Log.Info($"【Game状态机】【OutSide】 开始运行...");
            UIManager.Inst.Show(UIPanelName.UIMain);
            GameGlobal.Inst.CreateLogicWorld();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // Log.Info($"【Game状态机】【OutSide】 运行中...");
            GameGlobal.Inst.UpdateLogicWorld();
        }

        public override void OnLeave()
        {
            base.OnLeave();
            // Log.Info($"【Game状态机】【OutSide】 离开...");
            GameGlobal.Inst.ReleaseLogicWorld();
        }
    }
}