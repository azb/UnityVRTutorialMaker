using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class OVRTriggerRotater : MonoBehaviour
{
    // public variable that can be set to LTouch or RTouch in the Unity Inspector
    public Controller controller;
    
    public Axis1D inputTrigger;
    
    public enum Axis { X, Y, Z };

    public float rotateScale;
    
    public Axis rotationAxis;
    
    Quaternion startLocalRotation;

    // Start is called before the first frame update
    void Start()
    {
        startLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Get(inputTrigger, controller);

        float xrot = 0, yrot = 0, zrot = 0;
        
        switch(rotationAxis)
        {
            case Axis.X:
                xrot = input;
                break;
            case Axis.Y:
                yrot = input;
                break;
            case Axis.Z:
                zrot = input;
                break;
        }
        
        
        transform.localRotation = 
            startLocalRotation 
            * Quaternion.Euler(
                xrot * rotateScale,
                yrot * rotateScale,
                zrot * rotateScale
                );
    }
}
