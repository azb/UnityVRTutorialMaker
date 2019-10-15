using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        float ydir = Mathf.Atan2( 
            target.position.z - transform.position.z,
            target.position.x - transform.position.x
            ) * Mathf.Rad2Deg;

        Debug.Log("ydir = "+ydir);

        transform.rotation = Quaternion.Euler(0,-ydir,0);
    }
}
