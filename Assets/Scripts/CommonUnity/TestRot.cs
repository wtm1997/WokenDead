using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRot : MonoBehaviour
{
    public Transform target;

    public Transform gunRoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.transform.position - this.transform.position;
        Vector3 cross=Vector3.Cross(target.transform.position, this.transform.position);
        var angle = Vector2.Angle(new Vector2(target.transform.position.x, target.transform.position.z), new Vector2(transform.position.x, transform.position.z));
        angle = cross.y < 0 ? -angle : angle;
        angle -= 180;
        Debug.Log(angle);
        this.gunRoot.transform.localRotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
