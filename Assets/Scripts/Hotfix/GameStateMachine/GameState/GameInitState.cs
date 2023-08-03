using CommonLogic;
using CommonLogic.StateMachine;
using Hotfix.GameState;

public class GameInitState : StateBase
{
    public GameInitState(StateBaseMgr stateMgr) : base(stateMgr)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameEnterLoadingEvt evt = new GameEnterLoadingEvt();
        evt.EnterLoading = true;
        evt.FromState = EGameState.GameInit;
        evt.ToState = EGameState.GameLogin;
        EventManager.Inst.Send(evt);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}