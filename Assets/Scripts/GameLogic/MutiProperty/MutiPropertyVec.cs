using UnityEngine;

public class MutiPropertyVec : MutiProperty<Vector3>
{
    public override void AddVal(Vector3 val)
    {
        RealValue += val;
    }

    public override void RemoveVal(Vector3 val)
    {
        RealValue -= val;
    }

    public override void ModifyVal(Vector3 val, Vector3 existVal)
    {
        RealValue = RealValue - existVal + val;
    }
}
