using CommonLogic.StateMachine;
using Game;

namespace Hotfix.GameState
{
    public class GameLoading2OutsideTranslation : StateTranslation
    {
        private bool _enterOutside;

        public override void Init(int fromState, int toState)
        {
            base.Init(fromState, toState);
            EventManager.Inst.RegEvt<GameEnterOutsideEvt>(HandleEnterOutside);
        }

        void HandleEnterOutside(GameEnterOutsideEvt evt)
        {
            _enterOutside = evt.EnterOutside;
        }

        public override bool Satisfied()
        {
            return _enterOutside;
        }

        public override void Release()
        {
            base.Release();
            EventManager.Inst.UnRegEvt<GameEnterOutsideEvt>(HandleEnterOutside);
        }
        
        public override void Reset()
        {
            base.Reset();
            _enterOutside = false;
        }

        public GameLoading2OutsideTranslation(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
    }
}