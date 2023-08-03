using System;
using CommonLogic;
using Time = UnityEngine.Time;

namespace CommonUnity.Timer
{
    public class TimerData
    {
        public float Interval;
        public Action CallBack;
        public bool IsLoop;
        public float CurRuntime;

        public TimerData(float interval, Action callBack, bool isLoop)
        {
            this.Interval = interval;
            this.CallBack = callBack;
            this.IsLoop = isLoop;
            this.CurRuntime = 0;
        }
    }
    
    public class Timer : MonoSingleton<Timer>
    {
        private int _idx;
        private ListDictionary<int, TimerData> _timerDic = new ListDictionary<int, TimerData>();

        private void Awake()
        {
            _idx = 0;
        }

        private void Update()
        {
            for (int i = 0; i < _timerDic.Count; i++)
            {
                int timerId = _timerDic.KeyList[i];
                TimerData timerData = _timerDic.List[i];
                
                if (timerData.CurRuntime < timerData.Interval)
                {
                    timerData.CurRuntime += Time.deltaTime;
                }
                else
                {
                    timerData.CurRuntime = 0;
                    timerData.CallBack?.Invoke();
                    if(!timerData.IsLoop)
                        _timerDic.Remove(timerId);
                }
            }
        }

        private void OnDestroy()
        {
            _timerDic.Clear();
        }

        public int AddTimer(float interval, Action callBack, bool isLoop = false)
        {
            _idx++;
            TimerData timerData = new TimerData(interval, callBack, isLoop);
            _timerDic.Add(_idx, timerData);

            return _idx;
        }

        public void RemoveTimer(int id)
        {
            if(_timerDic.ContainsKey(id))
                _timerDic.Remove(id);
        }
    }
}