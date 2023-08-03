
namespace Hotfix.GameState
{
    public class GameStateEvt
    {
        public EGameState FromState;
        public EGameState ToState;
    }

    public class GameEnterInsideEvt : GameStateEvt
    {
        public bool EnterInside;
    }
    
    public class GameEnterLoginEvt : GameStateEvt
    {
        public bool EnterLogin;
    }
    
    public class GameEnterOutsideEvt : GameStateEvt
    {
        public bool EnterOutside;
    }

    public class GameEnterLoadingEvt : GameStateEvt
    {
        public EGameState FromState;
        public EGameState ToState;
        public bool EnterLoading;
    }
}