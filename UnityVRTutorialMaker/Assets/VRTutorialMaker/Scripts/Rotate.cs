using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
public class Rotate : MonoBehaviour {

    public Vector3 rotationsPerSecond;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(
            rotationsPerSecond.x * 360 * Time.deltaTime, 
            rotationsPerSecond.y * 360 * Time.deltaTime, 
            rotationsPerSecond.z * 360 * Time.deltaTime
            );
	}
}
}