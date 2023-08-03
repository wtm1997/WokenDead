using Entitas;

[Game]
public class ComProperty : IComponent
{
    public MutiProperty<int> Hp;
    public MutiProperty<int> MaxHp;
}