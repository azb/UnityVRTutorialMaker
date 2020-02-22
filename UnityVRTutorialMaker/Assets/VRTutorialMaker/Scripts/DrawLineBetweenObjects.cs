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
        if (object1 != null)
        {
            line.SetPosition(0, object1.position + offset1);
        }
        else
        {
            Debug.LogError("object1 is null");
        }

        if (object2 != null)
        {
            line.SetPosition(1, object2.position + offset2);
        }
        else
        {
            Debug.LogError("object2 is null");
        }
    }

    public void OnEnable()
    {
        if (line != null)
        {
            line.enabled = true;
        }
    }
    public void OnDisable()
    {
        if (line != null)
        {
            line.enabled = false;
        }
    }
}
