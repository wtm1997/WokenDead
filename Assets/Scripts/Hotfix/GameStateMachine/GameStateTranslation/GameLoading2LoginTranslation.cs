using CommonLogic.StateMachine;
using Game;

namespace Hotfix.GameState
{
    public class GameLoading2LoginTranslation : StateTranslation
    {
        private bool _enterLogin;

        public override void Init(int fromState, int toState)
        {
            base.Init(fromState, toState);
            EventManager.Inst.RegEvt<GameEnterLoginEvt>(HandleEnterLogin);
        }

        void HandleEnterLogin(GameEnterLoginEvt evt)
        {
            _enterLogin = evt.EnterLogin;
        }

        public override bool Satisfied()
        {
            return _enterLogin;
        }

        public override void Release()
        {
            base.Release();
            EventManager.Inst.UnRegEvt<GameEnterLoginEvt>(HandleEnterLogin);
        }
        
        public override void Reset()
        {
            base.Reset();
            _enterLogin = false;
        }

        public GameLoading2LoginTranslation(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
    }
}