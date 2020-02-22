using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
    [DefaultExecutionOrder(-400)]
    public class VRController : MonoBehaviour
    {
        public enum Platform
        {
            Steam,
            OVR
        }

        public enum VRDevice
        {
            RightController,
            LeftController,
            None
        }

        public enum VRInput
        {
            Button0,
            Button1,
            JoystickClick,
            Trigger,
            Grip,
            MenuButton,
            HomeButton,
            JoystickHorizontal,
            JoystickVertical,
            None
        }

        public enum VRInputAction
        {
            Pressed,
            Held,
            Released
        }

        const Platform platform = Platform.OVR;

        Hashtable VRInputToOVRButton, VRInputToOVRButtonAlternate;

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

        public void Vibrate(VRDevice device, float duration, float frequency, float amplitude)
        {
            if (platform == Platform.OVR)
            {
                OVRInput.Controller controller = OVRInput.Controller.LTouch;
                if (device == VRDevice.LeftController)
                    controller = OVRInput.Controller.LTouch;
                if (device == VRDevice.RightController)
                    controller = OVRInput.Controller.RTouch;

                if (device != VRDevice.None)
                {
                    OVRInput.SetControllerVibration(frequency, amplitude, controller);
                }
            }
            if (duration > 0)
            {
                StopVibrationTimer(device, duration, frequency, amplitude);
            }
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
            if (VR.GetPlatform() == VR.Platform.None)
            {
                this.enabled = false;
                return;
            }

            Debug.Log("GetsHere17 " + gameObject.name);


            RightControllerButton0 = TransformUtils.FindTransform("AButton");
            RightControllerButton1 = TransformUtils.FindTransform("BButton");
            RightControllerJoystick = TransformUtils.FindTransform("RightJoystickCenter");
            RightControllerTrigger = TransformUtils.FindTransform("RightTriggerCenter");
            RightControllerGrip = TransformUtils.FindTransform("RightGripCenter");

            LeftControllerButton0 = TransformUtils.FindTransform("XButton");
            LeftControllerButton1 = TransformUtils.FindTransform("YButton");
            LeftControllerJoystick = TransformUtils.FindTransform("LeftJoystickCenter");
            LeftControllerTrigger = TransformUtils.FindTransform("LeftTriggerCenter");
            LeftControllerGrip = TransformUtils.FindTransform("LeftGripCenter");

            MenuButton = TransformUtils.FindTransform("MenuButton");
            HomeButton = TransformUtils.FindTransform("HomeButton");

            leftController = TransformUtils.FindTransform("LeftControllerAnchor");
            rightController = TransformUtils.FindTransform("RightControllerAnchor");

            transform.parent = rightController;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            rightVRInputToTransform = new Hashtable();

            rightVRInputToTransform.Add(VRInput.Button0, RightControllerButton0);
            rightVRInputToTransform.Add(VRInput.Button1, RightControllerButton1);
            rightVRInputToTransform.Add(VRInput.JoystickClick, RightControllerJoystick);
            rightVRInputToTransform.Add(VRInput.JoystickHorizontal, RightControllerJoystick);
            rightVRInputToTransform.Add(VRInput.JoystickVertical, RightControllerJoystick);
            rightVRInputToTransform.Add(VRInput.Trigger, RightControllerTrigger);
            rightVRInputToTransform.Add(VRInput.Grip, RightControllerGrip);
            rightVRInputToTransform.Add(VRInput.MenuButton, HomeButton);

            leftVRInputToTransform = new Hashtable();

            leftVRInputToTransform.Add(VRInput.Button0, LeftControllerButton0);
            leftVRInputToTransform.Add(VRInput.Button1, LeftControllerButton1);
            leftVRInputToTransform.Add(VRInput.JoystickClick, LeftControllerJoystick);
            leftVRInputToTransform.Add(VRInput.JoystickHorizontal, LeftControllerJoystick);
            leftVRInputToTransform.Add(VRInput.JoystickVertical, LeftControllerJoystick);
            leftVRInputToTransform.Add(VRInput.Trigger, LeftControllerTrigger);
            leftVRInputToTransform.Add(VRInput.Grip, LeftControllerGrip);
            leftVRInputToTransform.Add(VRInput.MenuButton, MenuButton);

            //Create hashtable linking generic vrinputs to OVRInputs
            VRInputToOVRButton = new Hashtable();
            VRInputToOVRButton.Add(VRInput.Button0, OVRInput.Button.One);
            VRInputToOVRButton.Add(VRInput.Button1, OVRInput.Button.Two);
            VRInputToOVRButton.Add(VRInput.JoystickClick, OVRInput.Button.PrimaryThumbstick);
            VRInputToOVRButton.Add(VRInput.JoystickHorizontal, OVRInput.Button.PrimaryThumbstickRight);
            VRInputToOVRButton.Add(VRInput.JoystickVertical, OVRInput.Button.PrimaryThumbstickUp);
            VRInputToOVRButton.Add(VRInput.Trigger, OVRInput.Button.PrimaryIndexTrigger);
            VRInputToOVRButton.Add(VRInput.Grip, OVRInput.Button.PrimaryHandTrigger);
            VRInputToOVRButton.Add(VRInput.MenuButton, OVRInput.Button.Start);

            VRInputToOVRButtonAlternate = new Hashtable();
            VRInputToOVRButtonAlternate.Add(VRInput.Button0, OVRInput.Button.One);
            VRInputToOVRButtonAlternate.Add(VRInput.Button1, OVRInput.Button.Two);
            VRInputToOVRButtonAlternate.Add(VRInput.JoystickClick, OVRInput.Button.PrimaryThumbstick);
            VRInputToOVRButtonAlternate.Add(VRInput.JoystickHorizontal, OVRInput.Button.PrimaryThumbstickLeft);
            VRInputToOVRButtonAlternate.Add(VRInput.JoystickVertical, OVRInput.Button.PrimaryThumbstickDown);
            VRInputToOVRButtonAlternate.Add(VRInput.Trigger, OVRInput.Button.PrimaryIndexTrigger);
            VRInputToOVRButtonAlternate.Add(VRInput.Grip, OVRInput.Button.PrimaryHandTrigger);
            VRInputToOVRButtonAlternate.Add(VRInput.MenuButton, OVRInput.Button.Start);
        }

        public Transform VRInputToTransform(VRDevice vrDevice, VRInput vrInput)
        {
            Transform result = null;

            if (vrDevice == VRDevice.LeftController)
            {
                result = (Transform)leftVRInputToTransform[vrInput];
            }

            if (vrDevice == VRDevice.RightController)
            {
                result = (Transform)rightVRInputToTransform[vrInput];
            }

            //Debug.Log("VRInputToTransform("+vrDevice+","+vrInput+") result = "+result.name);

            return result;
        }

        public bool InputActive(VRDevice vrDevice, VRInput input, VRInputAction action)
        {
            if (vrDevice == VRDevice.None || input == VRInput.None)
            {
                //Debug.LogError("Checking if no input device is active.");
                return false;
            }

            if (platform == Platform.OVR)
            {
                OVRInput.Button button = (OVRInput.Button)VRInputToOVRButton[input];
                OVRInput.Button buttonAlternate = (OVRInput.Button)VRInputToOVRButtonAlternate[input];

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


                if (action == VRInputAction.Pressed)
                {
                    return OVRInput.GetDown(button, controller) || OVRInput.GetDown(buttonAlternate, controller);
                }
                else if (action == VRInputAction.Held)
                {
                    return OVRInput.Get(button, controller) || OVRInput.Get(buttonAlternate, controller);
                }
                else //implicitly if (action == VRInputAction.Released)
                {
                    return OVRInput.GetUp(button, controller) || OVRInput.GetUp(buttonAlternate, controller);
                }
            }
        }
    }
}