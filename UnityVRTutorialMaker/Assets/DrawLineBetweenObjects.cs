using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBetweenObjects : MonoBehaviour
{
    public Transform object1, object2;

    public Vector3 offset1, offset2;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, object1.position + offset1);
        line.SetPosition(1, object2.position + offset2);
    }
}
