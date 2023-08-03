using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class UIBaseHandlerAttribute : Attribute
{
    public string PrefabName;

    public UIBaseHandlerAttribute(string prefabName)
    {
        PrefabName = prefabName;
    }
}