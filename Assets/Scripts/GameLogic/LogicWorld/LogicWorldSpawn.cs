using UnityEngine;

public class LogicWorldSpawn
{
    private Contexts _contexts;
    
    public LogicWorldSpawn(Contexts contexts)
    {
        _contexts = contexts;
        OnSpawnHero();
    }
    
    public void OnSpawnHero()
    {
        var entity = _contexts.game.CreateEntity();
    }
}