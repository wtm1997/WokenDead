using CommonLogic.StateMachine;
using Game;

namespace Hotfix.GameState
{
    public class GameAny2LoadingTranslation : StateTranslation
    {
        private bool _enterLoading;
        private bool _loadingOver;
        
        public override void Init(int fromState, int toState)
        {
            base.Init(fromState, toState);
            EventManager.Inst.RegEvt<GameEnterLoadingEvt>(HandleEnterLoading);
        }

        void HandleEnterLoading(GameEnterLoadingEvt evt)
        {
            GameStateMgr gameStateMgr = _stateMgr as GameStateMgr;
            gameStateMgr.Loading2State = evt.ToState;
            _enterLoading = evt.EnterLoading && evt.FromState == (EGameState)FromState;
        }

        public override bool Satisfied()
        {
            return _enterLoading;
        }

        public override void Release()
        {
            base.Release();
            EventManager.Inst.UnRegEvt<GameEnterLoadingEvt>(HandleEnterLoading);
        }

        public override void Reset()
        {
            base.Reset();
            _enterLoading = false;
        }

        public GameAny2LoadingTranslation(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
    }
}