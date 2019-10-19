using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class VRController : MonoBehaviour
{
    public enum Platform {
        Steam,
        OVR
    }

    public enum VRDevice {
        RightController,
        LeftController
    }

    public enum VRInput {
        Button0,
        Button1,
        Joystick,
        Trigger,
        Grip,
        MenuButton,
        HomeButton
    }

    const Platform platform = Platform.OVR;

    Hashtable VRInputToOVRButton;
    
    public Hashtable leftVRInputToTransform, rightVRInputToTransform;
    
    Transform 
        RightControllerButton0,
        RightControllerButton1,
        RightControllerJoystick,
        RightControllerTrigger,
        RightControllerGrip,
        LeftControllerButton0,
        LeftControllerButton1,
        LeftControllerJoystick,
        LeftControllerTrigger,
        LeftControllerGrip,
        MenuButton,
        HomeButton;

    public Transform
        leftController,
        rightController;
    
    MeshRenderer rend;

    Transform FindTransform(string name)
    {
        Transform foundTransform = GameObject.Find(name).transform;

        if (foundTransform == null)
        {
            Debug.LogError("Couldn't find a Transform named "+name);
        }

        return foundTransform;
    }

    public void Vibrate(VRDevice device, float duration, float frequency, float amplitude)
    {
        if (platform == Platform.OVR)
        { 
        OVRInput.Controller controller = OVRInput.Controller.LTouch;
            if (device == VRDevice.LeftController)
                controller = OVRInput.Controller.LTouch;
            if (device == VRDevice.RightController)
                controller = OVRInput.Controller.RTouch;

        OVRInput.SetControllerVibration (frequency, amplitude, controller);
        }
        if (duration > 0)
        StopVibrationTimer(device, duration, frequency, amplitude);
    }
    
    void StopVibrationTimer(VRDevice device, float timeRemaining, float frequency, float amplitude)
    {
        IEnumerator timer = Timer(device, timeRemaining, frequency, amplitude);
        StartCoroutine(timer);
    }
    IEnumerator Timer(VRDevice device, float timeRemaining, float frequency, float amplitude)
    {
        float waitTime = Mathf.Min(timeRemaining, 2f);
        yield return new WaitForSeconds(waitTime);
        //Do something
        timeRemaining -= waitTime;
        if (timeRemaining > 0)
        {
            Vibrate(device, timeRemaining, frequency, amplitude);
        }
        else
        {
            Vibrate(device, 0, 0, 0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        RightControllerButton0 = FindTransform("AButton");
        RightControllerButton1 = FindTransform("BButton");
        RightControllerJoystick = FindTransform("RightJoystickCenter");
        RightControllerTrigger = FindTransform("RightTriggerCenter");
        RightControllerGrip = FindTransform("RightGripCenter");

        LeftControllerButton0 = FindTransform("XButton");
        LeftControllerButton1 = FindTransform("YButton");
        LeftControllerJoystick = FindTransform("LeftJoystickCenter");
        LeftControllerTrigger = FindTransform("LeftTriggerCenter");
        LeftControllerGrip = FindTransform("LeftGripCenter");

        
        MenuButton = FindTransform("MenuButton");
        HomeButton = FindTransform("HomeButton");
        
        leftController = FindTransform("LeftControllerAnchor");
        rightController = FindTransform("RightControllerAnchor");

        rightVRInputToTransform = new Hashtable();
        rightVRInputToTransform.Add(VRInput.Button0, RightControllerButton0);
        rightVRInputToTransform.Add(VRInput.Button1, RightControllerButton1);
        rightVRInputToTransform.Add(VRInput.Joystick, RightControllerJoystick);
        rightVRInputToTransform.Add(VRInput.Trigger, RightControllerTrigger);
        rightVRInputToTransform.Add(VRInput.Grip, RightControllerGrip);
        rightVRInputToTransform.Add(VRInput.MenuButton, HomeButton);
        
        leftVRInputToTransform = new Hashtable();
        leftVRInputToTransform.Add(VRInput.Button0, LeftControllerButton0);
        leftVRInputToTransform.Add(VRInput.Button1, LeftControllerButton1);
        leftVRInputToTransform.Add(VRInput.Joystick, LeftControllerJoystick);
        leftVRInputToTransform.Add(VRInput.Trigger, LeftControllerTrigger);
        leftVRInputToTransform.Add(VRInput.Grip, LeftControllerGrip);
        leftVRInputToTransform.Add(VRInput.MenuButton, MenuButton);

        VRInputToOVRButton = new Hashtable();
        VRInputToOVRButton.Add(VRInput.Button0, OVRInput.Button.One);
        VRInputToOVRButton.Add(VRInput.Button1, OVRInput.Button.Two);
        VRInputToOVRButton.Add(VRInput.Joystick, OVRInput.Button.PrimaryThumbstick);
        VRInputToOVRButton.Add(VRInput.Trigger, OVRInput.Button.PrimaryIndexTrigger);
        VRInputToOVRButton.Add(VRInput.Grip, OVRInput.Button.PrimaryHandTrigger);
        VRInputToOVRButton.Add(VRInput.MenuButton, OVRInput.Button.Start);

        
        //rend = LeftControllerButton0.GetComponentInChildren<MeshRenderer>(); 
        //rend.material = highlight; //.SetColor("Albedo", Color.yellow);
        
    }

    public Transform VRInputToTransform(VRDevice vrDevice, VRInput vrInput)
    {
        Transform result;

        if (vrDevice == VRDevice.LeftController)
            result = (Transform) leftVRInputToTransform[vrInput];
        else
            result = (Transform) rightVRInputToTransform[vrInput];

        Debug.Log("VRInputToTransform("+vrDevice+","+vrInput+") result = "+result.name);
        
        return result;
    }

    public bool InputActive(VRDevice vrDevice, VRInput input)
    {
        if (platform == Platform.OVR)
        {
            //Debug.Log("input = "+input);
            
            OVRInput.Button button = (OVRInput.Button) VRInputToOVRButton[input];
            OVRInput.Controller controller = OVRInput.Controller.LTouch;

            if (vrDevice == VRDevice.LeftController)
            {
                controller = OVRInput.Controller.LTouch;
            }
            else
            if (vrDevice == VRDevice.RightController)
            {
                controller = OVRInput.Controller.RTouch;
            }
            
            return OVRInput.Get(button, controller);
        }
    }


}
