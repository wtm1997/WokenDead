using CommonLogic.StateMachine;

namespace Hotfix.GameState
{
    public class GameInsideState : StateBase
    {
        public GameInsideState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            // Log.Info($"【Game状态机】【Inside】 开始运行...");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // Log.Info($"【Game状态机】【Inside】 运行中...");
        }

        public override void OnLeave()
        {
            base.OnLeave();
            // Log.Info($"【Game状态机】【Inside】 离开...");
        }
    }
}