using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class OVRButtonRotater : MonoBehaviour
{
    // public variable that can be set to LTouch or RTouch in the Unity Inspector
    public Controller controller;

    public enum InputType { Axis1D, Axis2D };

    public InputType inputType;
    
    public OVRInput.Axis1D axis1d;
    public OVRInput.Axis2D axis2d;
    
    public Vector3 rotateScale;
    
    public enum Axis { X, Y, Z };

    public Axis xAxis = Axis.X, yAxis = Axis.Y;

    
    public Axis buttonRotationAxisX, buttonRotationAxisY;
    
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
        
        switch(inputType)
        {

            case InputType.Axis1D:

                input.x = OVRInput.Get(axis1d, controller);

                break;

            case InputType.Axis2D:
                
                input = OVRInput.Get(axis2d, controller);
                
                break;
        }

        float xrot = 0, yrot = 0, zrot = 0;
        
        switch(buttonRotationAxisX)
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
        
        switch(buttonRotationAxisY)
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
