using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{ 
public class RotateTowardsObject : MonoBehaviour
{
    public string targetTag;
    Transform target;

    public Vector3 offset;

    public bool x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform; //TransformUtils.FindTransform(targetTag);
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
        
        if (!x)
            xdir = 0;

        float ydir = Mathf.Atan2( 
            target.position.z - transform.position.z,
            target.position.x - transform.position.x
            ) * Mathf.Rad2Deg;
        
        if (!y)
            ydir = 0;

        transform.rotation = Quaternion.Euler(
            xdir + offset.x,
            -ydir + offset.y,
            offset.z
            );
    }
}
}