using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class OVRButtonPositioner : MonoBehaviour
{
    // public variable that can be set to LTouch or RTouch in the Unity Inspector
    public Controller controller;

    public OVRInput.Button button;

    public enum Axis { X, Y, Z };

    public Axis buttonMovementAxis;
    
    public Vector3 moveScale;
    
// returns a float of the Hand Trigger’s current state on the Oculus Touch controller
// specified by the controller variable.

    Vector3 startLocalPosition;


    // Start is called before the first frame update
    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    Vector2 input = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.GetDown(button, controller))
        {
            //Debug.Log("button pressed: "+button+" , "+" controller: "+controller+" gameobject: "+gameObject.name);
            
            input.x = 1f;
        }
        if (OVRInput.GetUp(button, controller))
        {
            //Debug.Log("button released: "+button+" , "+" controller: "+controller+" gameobject: "+gameObject.name);
            input.x = 0f;
        }
                
        float xoff = 0, yoff = 0, zoff = 0;

        switch(buttonMovementAxis)
        {
            case Axis.X:
                xoff = input.x;
                break;
            case Axis.Y:
                yoff = input.x;
                break;
            case Axis.Z:
                zoff = input.x;
                break;
        }

        Vector3 inputOffset = new Vector3(xoff, yoff, zoff);
        inputOffset.Scale(moveScale);
        
        transform.localPosition = startLocalPosition + inputOffset;
    }
}
