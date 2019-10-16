using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance( 
            new Vector2(
                transform.position.x,
                transform.position.z),
            new Vector2(
                target.position.x,
                target.position.z)
            );

        float xdir = Mathf.Atan2( 
            target.position.y - transform.position.y,
            dist
            ) * Mathf.Rad2Deg;

        float ydir = Mathf.Atan2( 
            target.position.z - transform.position.z,
            target.position.x - transform.position.x
            ) * Mathf.Rad2Deg;

        Debug.Log("ydir = "+ydir);

        transform.rotation = Quaternion.Euler(
            xdir + offset.x,
            -ydir + offset.y,
            offset.z
            );
    }
}
