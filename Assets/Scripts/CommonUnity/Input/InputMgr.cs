using UnityEngine;

public class InputMgr : MonoBehaviour
{
    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x == 0 && y == 0) return;

        InputEvt inputEvt = new InputEvt();
        inputEvt.X = x;
        inputEvt.Y = y;
        EventManager.Inst.Send<InputEvt>(inputEvt);
    }
}
