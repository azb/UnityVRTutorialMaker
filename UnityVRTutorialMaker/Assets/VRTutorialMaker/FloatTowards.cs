using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTowards : MonoBehaviour
{
    public Transform target1, target2;

    public Vector3 offset;

    public float speed;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (target1.position + target1.forward * distance + target2.position + target2.forward * distance) / 2f + offset;

        //transform.position = Vector3.Lerp( 
        //    transform.position,
        //    (target1.position + target1.forward * distance + target2.position + target2.forward * distance) / 2f + offset,
        //    Time.deltaTime * speed
        //    );
    }
}
