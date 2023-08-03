using System;
using System.Collections.Generic;
using CommonLogic;

public class EventManager : Singleton<EventManager>
{
    interface IRegisterations
    {
        
    }

    class Registerations<T> : IRegisterations
    {
        public Action<T> OnReceives = obj => { };
    }

    private static Dictionary<Type, IRegisterations> _evtDic = new Dictionary<Type, IRegisterations>();

    public void RegEvt<T>(Action<T> onRecv)
    {
        Type type = typeof(T);
        if (_evtDic.TryGetValue(type, out IRegisterations registerations))
        {
            Registerations<T> reg = registerations as Registerations<T>;
            reg.OnReceives += onRecv;
        }
        else
        {
            Registerations<T> newReg = new Registerations<T>();
            newReg.OnReceives += onRecv;
            _evtDic.Add(type, newReg);
        }
    }

    public void UnRegEvt<T>(Action<T> onRecv)
    {
        Type type = typeof(T);
        if (_evtDic.TryGetValue(type, out IRegisterations registerations))
        {
            Registerations<T> reg = registerations as Registerations<T>;
            reg.OnReceives -= onRecv;
        }
    }

    public void Send<T>(T t)
    {
        Type type = typeof(T);
        if (_evtDic.TryGetValue(type, out IRegisterations registerations))
        {
            Registerations<T> reg = registerations as Registerations<T>;
            reg.OnReceives(t);
        }
    }
}
