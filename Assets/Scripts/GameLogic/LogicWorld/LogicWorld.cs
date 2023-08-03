using Entitas;

public class LogicWorld
{
    private LogicWorldSpawn _spawn;
    public LogicWorldSpawn Spawn => _spawn;
    
    private Contexts _contexts;
    private Systems _systems;

    public void Create()
    {
        _contexts = new Contexts();
        _spawn = new LogicWorldSpawn(_contexts);

        InitECS();
    }

    public void Update()
    {
        _systems.Execute();
    }

    public void Release()
    {
        _systems.TearDown();
        _systems.Cleanup();
    }

    private void InitECS()
    {
        _systems = new Systems();
        _systems.Initialize();
    }
}