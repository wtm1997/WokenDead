using CommonLogic.StateMachine;
using Game;

namespace Hotfix.GameState
{
    public class GameLoading2InsideTranslation : StateTranslation
    {
        private bool _enterInside;

        public override void Init(int fromState, int toState)
        {
            base.Init(fromState, toState);
            EventManager.Inst.RegEvt<GameEnterInsideEvt>(HandleEnterInside);
        }

        void HandleEnterInside(GameEnterInsideEvt evt)
        {
            _enterInside = evt.EnterInside;
        }

        public override bool Satisfied()
        {
            return _enterInside;
        }

        public override void Release()
        {
            base.Release();
            EventManager.Inst.UnRegEvt<GameEnterInsideEvt>(HandleEnterInside);
        }
        
        public override void Reset()
        {
            base.Reset();
            _enterInside = false;
        }

        public GameLoading2InsideTranslation(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }
    }
}