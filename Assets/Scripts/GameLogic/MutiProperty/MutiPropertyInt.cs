public class MutiPropertyInt : MutiProperty<int>
{
    public override void AddVal(int val)
    {
        RealValue += val;
    }

    public override void RemoveVal(int val)
    {
        RealValue -= val;
    }

    public override void ModifyVal(int val, int existVal)
    {
        RealValue = RealValue - existVal + val;
    }
}