using System.Collections;
using System.Collections.Generic;
using CommonLogic;

namespace CommonUnity
{
    public class TaskManager : MonoSingleton<TaskManager>
    {
        private ListDictionary<int, IEnumerator> _enumeratorList = new ListDictionary<int, IEnumerator>();
        
        private List<CustomTask> _taskList = new List<CustomTask>();

        private int _idx = -1;

        private void Awake()
        {
            _idx = -1;
        }

        public void StartTask(IEnumerator enumerator)
        {
            if (_enumeratorList.List.Contains(enumerator))
                return;
            _idx++;
            _enumeratorList.Add(_idx, enumerator);
            CustomTask task = new CustomTask(_idx, enumerator);
            _taskList.Add(task);
        }

        public void PauseTask(IEnumerator enumerator)
        {
            if (!_enumeratorList.List.Contains(enumerator))
                return;

            int taskId = GetTaskId(enumerator);
            if (taskId == -1)
            {
                Log.Error($"{enumerator} not register?");
                return;
            }
            for (int i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i].Id == taskId)
                {
                    _taskList[i].Pause();
                    break;
                }
            }
        }
        
        public void ResumeTask(IEnumerator enumerator)
        {
            if (!_enumeratorList.List.Contains(enumerator))
                return;

            int taskId = GetTaskId(enumerator);
            if (taskId == -1)
            {
                Log.Error($"{enumerator} not register?");
                return;
            }
            for (int i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i].Id == taskId)
                {
                    _taskList[i].Resume();
                    break;
                }
            }
        }

        public void StopTask(IEnumerator enumerator)
        {
            if (!_enumeratorList.List.Contains(enumerator))
                return;

            int taskId = GetTaskId(enumerator);
            if (taskId == -1)
            {
                Log.Error($"{enumerator} not register?");
                return;
            }
            for (int i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i].Id == taskId)
                {
                    _taskList[i].Stop();
                    break;
                }
            }
        }

        private int GetTaskId(IEnumerator enumerator)
        {
            int taskId = -1;
            for (int i = 0; i < _enumeratorList.Count; i++)
            {
                if (_enumeratorList.List[i] == enumerator)
                {
                    taskId = _enumeratorList.KeyList[i];
                    break;
                }
            }

            return taskId;
        }

        public void ClearTask()
        {
            _enumeratorList.Clear();
            for (int i = 0; i < _taskList.Count; i++)
            {
                _taskList[i].Stop();
            }
            _taskList.Clear();
        }

        private void RemoveTask(int idx)
        {
            if (_enumeratorList.TryGetValue(idx, out IEnumerator enumerator))
            {
                _enumeratorList.Remove(idx);
            }

            for (int i = 0; i < _taskList.Count; i++)
            {
                if (_taskList[i].Id == idx)
                {
                    _taskList.Remove(_taskList[i]);
                    return;
                }
            }
        }

        public void LateUpdate()
        {
            for (int i = 0; i < _taskList.Count; i++)
            {
                if (!_taskList[i].IsRunning)
                {
                    _taskList[i].Start();
                    StartCoroutine(_taskList[i].CallWrapper());
                }
                
                if (_taskList[i].CheckIsOver())
                {
                    RemoveTask(_taskList[i].Id);
                }
            }
        }
    }
}