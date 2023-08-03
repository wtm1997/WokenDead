namespace CommonLogic.StateMachine
{
    public class StateBaseMgr
    {
        public ListDictionary<int, StateBase> _stateType2StateBaseDic;
        protected StateBase CurrentState;

        public virtual void Init()
        {
        }

        /// <summary>
        /// 状态机开始
        /// </summary>
        public virtual void Start()
        {
            if (CurrentState == null)
            {
                Log.Error($"【状态机】当前状态机没有指定初始状态，请指定！");
                return;
            }

            CurrentState.OnEnter();
        }


        /// <summary>
        /// 状态机暂停
        /// </summary>
        public virtual void Pause()
        {
            if (CurrentState == null)
            {
                Log.Error($"【状态机】 当前状态机没有正在运行的状态，不需要暂停！");
                return;
            }

            CurrentState.OnPause();
        }

        /// <summary>
        /// 从暂停中恢复
        /// </summary>
        public virtual void Resume()
        {
            if (CurrentState == null)
            {
                Log.Error($"【状态机】 当前状态机没有正在运行的状态，恢复不了！");
                return;
            }

            CurrentState.OnResume();
        }

        public virtual void Stop()
        {
            if (CurrentState == null)
            {
                Log.Error($"【状态机】 当前状态机没有正在运行的状态，无法关闭！");
                return;
            }

            CurrentState.OnLeave();
        }

        public virtual void Release()
        {
            if (CurrentState != null)
                CurrentState.OnLeave();
            if (_stateType2StateBaseDic != null)
            {
                for (int i = 0; i < _stateType2StateBaseDic.Count; i++)
                {
                    _stateType2StateBaseDic.List[i].Release();
                }

                _stateType2StateBaseDic.Clear();
                _stateType2StateBaseDic = null;
            }
        }

        public virtual void Update()
        {
            if (CurrentState != null)
                CurrentState.OnUpdate();
        }

        public void AddState(int stateType, StateBase stateBase, bool isDefault = false)
        {
            _stateType2StateBaseDic ??= new ListDictionary<int, StateBase>();
            if (_stateType2StateBaseDic.TryGetValue(stateType, out StateBase existStateBase))
            {
                Log.Info($"【状态机】当前状态机已经添加过State = {stateType}，请检查原因！");
            }
            else
            {
                stateBase.Init();
                _stateType2StateBaseDic.Add(stateType, stateBase);
            }

            if (isDefault)
                SetDefaultState(stateBase);
        }

        public void SetDefaultState(StateBase defaultState)
        {
            CurrentState = defaultState;
        }

        public void TranslationState(int stateType)
        {
            CurrentState?.OnLeave();
            if (_stateType2StateBaseDic == null)
            {
                Log.Info($"【状态机】当前状态机没有状态，无法Translation！");
                return;
            }

            if (_stateType2StateBaseDic.TryGetValue(stateType, out StateBase existState))
            {
                existState.OnEnter();
                CurrentState = existState;
            }
            else
            {
                Log.Info($"【状态机】当前状态机没有State = {stateType}， 请检查原因！");
            }
        }
    }
}