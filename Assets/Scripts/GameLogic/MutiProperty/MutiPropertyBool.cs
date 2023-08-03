public class MutiPropertyBool : MutiProperty<bool>
{
    public override void AddVal(bool val)
    {
        RealValue &= val;
    }

    public override void RemoveVal(bool val)
    {
        RealValue &= !val;
    }

    public override void ModifyVal(bool val, bool existVal)
    {
        RealValue &= !existVal;
        RealValue &= val;
    }
}
