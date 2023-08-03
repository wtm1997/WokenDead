using System;
using System.Collections.Generic;
using UnityEngine;

public class MutiProperty<T> where T : struct
{
    public T RealValue;

    public T BaseValue;

    protected Dictionary<EMutiPropertySource, T> _mutiProperty = null;

    /// <summary>
    /// 新增属性来源
    /// </summary>
    /// <param name="source">来源枚举</param>
    /// <param name="val">属性值</param>
    public void AddSource(EMutiPropertySource source, T val)
    {
        if (_mutiProperty == null) _mutiProperty = new Dictionary<EMutiPropertySource, T>();
        if (_mutiProperty.TryGetValue(source, out T hasVal))
        {
            Debug.LogWarning($"【多重属性】【重复添加】如果是想修改属性，那请使用ModifySource属性");
        }
        else
        {
            _mutiProperty.Add(source, val);
            AddVal(val);
        }
    }

    public void RemoveSource(EMutiPropertySource source)
    {
        if (_mutiProperty == null)
        {
            Debug.LogWarning($"【多重属性】【源头不存在】当前想要删除的属性来源不存在 Source = {source.ToString()}");
            return;
        }

        RemoveVal(_mutiProperty[source]);
        _mutiProperty.Remove(source);
    }

    public void ModifySource(EMutiPropertySource source, T val)
    {
        if (_mutiProperty.TryGetValue(source, out T existVal))
        {
            _mutiProperty[source] = existVal;
        }
        else
        {
            Debug.LogWarning($"【多重属性】【源头不存在】你想移除的数据已经不在了");
        }
        ModifyVal(val, existVal);
    }

    /// <summary>
    /// 添加属性的时候使用，+ - 均用一个
    /// </summary>
    public virtual void AddVal(T val)
    {
    }

    /// <summary>
    /// 移除属性的时候使用
    /// </summary>
    public virtual void RemoveVal(T val)
    {
        
    }

    /// <summary>
    /// 修改属性的时候使用
    /// </summary>
    public virtual void ModifyVal(T val, T existVal)
    {
        
    }
}