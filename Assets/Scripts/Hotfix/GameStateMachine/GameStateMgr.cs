using CommonLogic.StateMachine;
using Hotfix.GameState;

namespace Game
{
    public class GameStateMgr : StateBaseMgr
    {
        public EGameState Loading2State = EGameState.GameInit;
        
        public override void Init()
        {
            base.Init();
            GameInitState initState = new GameInitState(this);
            initState.AddTranslation((int)EGameStateTranslation.Init2Loading, new GameAny2LoadingTranslation(this),
                (int)EGameState.GameInit, (int)EGameState.GameLoading);

            GameLoadingState loadingState = new GameLoadingState(this);
            loadingState.AddTranslation((int)EGameStateTranslation.Loading2Login, new GameLoading2LoginTranslation(this),
                (int)EGameState.GameLoading, (int)EGameState.GameLogin);
            loadingState.AddTranslation((int)EGameStateTranslation.Loading2Outside, new GameLoading2OutsideTranslation(this),
                (int)EGameState.GameLoading, (int)EGameState.GameOutside);
            loadingState.AddTranslation((int)EGameStateTranslation.Loading2Inside, new GameLoading2InsideTranslation(this),
                (int)EGameState.GameLoading, (int)EGameState.GameInside);

            GameLoginState loginState = new GameLoginState(this);
            loginState.AddTranslation((int)EGameStateTranslation.Any2Loading, new GameAny2LoadingTranslation(this),
                (int)EGameState.GameLogin, (int)EGameState.GameLoading);

            GameInsideState insideState = new GameInsideState(this);
            insideState.AddTranslation((int)EGameStateTranslation.Any2Loading, new GameAny2LoadingTranslation(this),
                (int)EGameState.GameInside, (int)EGameState.GameLoading);

            GameOutsideState outsideState = new GameOutsideState(this);
            outsideState.AddTranslation((int)EGameStateTranslation.Any2Loading, new GameAny2LoadingTranslation(this),
                (int)EGameState.GameOutside, (int)EGameState.GameLoading);

            AddState((int)EGameState.GameInit, initState, true);
            AddState((int)EGameState.GameLoading, loadingState);
            AddState((int)EGameState.GameLogin, loginState);
            AddState((int)EGameState.GameInside, insideState);
            AddState((int)EGameState.GameOutside, outsideState);
        }
    }
}
