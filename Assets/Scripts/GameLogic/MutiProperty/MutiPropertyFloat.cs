public class MutiPropertyFloat : MutiProperty<float>
{
    public override void AddVal(float val)
    {
        RealValue += val;
    }

    public override void RemoveVal(float val)
    {
        RealValue -= val;
    }

    public override void ModifyVal(float val, float existVal)
    {
        RealValue = RealValue - existVal + val;
    }
}
