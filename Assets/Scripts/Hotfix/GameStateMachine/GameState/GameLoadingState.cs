using System.Collections;
using CommonLogic.StateMachine;
using CommonUnity;
using Game;
using Hotfix.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hotfix.GameState
{
    public class GameLoadingState : StateBase
    {
        private GameStateMgr _gameStateMgr => _stateMgr as GameStateMgr;
        
        public GameLoadingState(StateBaseMgr stateMgr) : base(stateMgr)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            // Log.Info($"【Game状态机】【Loading】 开始运行...");
            UIManager.Inst.Show(UIPanelName.UILoading);
            switch (_gameStateMgr.Loading2State)
            {
                case EGameState.GameLogin:
                    GameEnterLoginEvt loginEvt = new GameEnterLoginEvt();
                    loginEvt.EnterLogin = true;
                    EventManager.Inst.Send(loginEvt);
                    break;
                case EGameState.GameOutside:
                    TaskManager.Inst.StartTask(LoadScene());
                    break;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // Log.Info($"【Game状态机】【Loading】 运行中...");
        }

        #region GameOutside

        private IEnumerator LoadScene()
        {
            AsyncOperation handler = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
            yield return handler;
            while (!handler.isDone)
            {
                yield return null;
            }

            GameEnterOutsideEvt evt = new GameEnterOutsideEvt();
            evt.FromState = EGameState.GameLoading;
            evt.EnterOutside = true;
            EventManager.Inst.Send(evt);
        }

        #endregion

        public override void OnLeave()
        {
            base.OnLeave();
            // Log.Info($"【Game状态机】【Loading】 离开...");
            UIManager.Inst.Hide(UIPanelName.UILoading);
        }
    }
}