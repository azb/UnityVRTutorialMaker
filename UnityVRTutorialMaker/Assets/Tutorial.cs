using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public enum Control {
        RightControllerButton0,
        RightControllerButton1,
        RightControllerJoystick,
        RightControllerTrigger,
        RightControllerGrip,
        LeftControllerButton0,
        LeftControllerButton1,
        LeftControllerJoystick,
        LeftControllerTrigger,
        LeftControllerGrip
    };

    public Transform 
        RightControllerButton0,
        RightControllerButton1,
        RightControllerJoystick,
        RightControllerTrigger,
        RightControllerGrip,
        LeftControllerButton0,
        LeftControllerButton1,
        LeftControllerJoystick,
        LeftControllerTrigger,
        LeftControllerGrip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
