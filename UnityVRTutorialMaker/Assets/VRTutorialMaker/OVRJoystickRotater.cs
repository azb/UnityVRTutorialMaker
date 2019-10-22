using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class OVRJoystickRotater : MonoBehaviour
{
    // public variable that can be set to LTouch or RTouch in the Unity Inspector
    public Controller controller;
    
    public OVRInput.Axis2D inputJoystick;
    
    public enum Axis { X, Y, Z };

    public Vector3 rotateScale;
    
    public Axis inputXMapsToRotation, inputYMapsToRotation;
    
    Quaternion startLocalRotation;

    // Start is called before the first frame update
    void Start()
    {
        startLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        
        input = Get(inputJoystick, controller);

        float xrot = 0, yrot = 0, zrot = 0;
        
        switch(inputXMapsToRotation)
        {
            case Axis.X:
                xrot = input.x;
                break;
            case Axis.Y:
                yrot = input.x;
                break;
            case Axis.Z:
                zrot = input.x;
                break;
        }
        
        switch(inputYMapsToRotation)
        {
            case Axis.X:
                xrot = input.y;
                break;
            case Axis.Y:
                yrot = input.y;
                break;
            case Axis.Z:
                zrot = input.y;
                break;
        }
        
        transform.localRotation = 
            startLocalRotation 
            * Quaternion.Euler(
                xrot * rotateScale.x,
                yrot * rotateScale.y,
                zrot * rotateScale.z
                );
    }
}
