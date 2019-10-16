using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTowards : MonoBehaviour
{
    public Transform target;

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
        transform.position = Vector3.Lerp( 
            transform.position,
            target.position + target.forward * distance + offset,
            Time.deltaTime * speed
            );
    }
}
