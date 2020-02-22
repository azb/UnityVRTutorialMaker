using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRTutorializer
{
    [DefaultExecutionOrder(-100)]
    public class TutorialStep : MonoBehaviour
    {
        public enum ActivationType { Enabled, CloseToPlayer };

        public Transform pointAt;

        public Tutorial tutorial;

        //Use this audio clip to have narration for your tutorial
        public AudioClip audioClip;

        //The tutorial text for this tutorial step
        [TextArea]
        public string text;

        public Sprite[] sprite;

        public AnimationClip animationClip;

        //The controller on which the user must press a button to complete this tutorial step
        //public OVRInput.Controller controller;

        //The button the user must press to complete this tutorial step
        //public OVRInput.Button button;

        [Header("Which controller should be highlighted in this tutorial step?")]
        public VRController.VRDevice vrDeviceToHighlight;
        [Header("Which button, joystick, or trigger should be highlighted in this tutorial step?")]
        public VRController.VRInput vrInputToHighlight;

        public VRController.VRInputAction action;

        [Header("Complete this tutorial step when the user triggers this input?")]
        public bool completeStepOnInput = true;
        
        [Header("What actions should be executed when this tutorial step is enabled?")]
        public UnityEvent executeOnEnable;

        [Header("What actions should be executed when the user completes this tutorial step?")]
        public UnityEvent executeOnInputComplete;

        public GameObject[] objectsToActivateOnEnable;

        bool userCompletedTutorialActionEventTriggered = false;

        //The executeOnCompletion event will be executed when the user presses the 
        //correct button to complete this tutorial step
        //public UnityEvent executeOnCompletion;

        Hashtable vrInputToOVRInput;

        bool stepCompleted;

        public int requiredCompletions = 1;
        int completions;

        public TutorialScriptableObject tutorialScriptableObject;

        bool started;

        void Start()
        {
            if (VR.GetPlatform() == VR.Platform.OculusGo)
            {
                text = text.Replace("Press A", "Press the thumb pad");
            }
            started = true;
        }

        public void ActivateStep()
        {
            if (pointAt == null)
            {
                tutorial.SetTargetTransform(vrDeviceToHighlight, vrInputToHighlight);
            }
            else
            {
                tutorial.SetTargetTransform(pointAt);
            }

            executeOnEnable.Invoke();

            int count = objectsToActivateOnEnable.Length;

            for (int i = 0; i < count; i++)
            {
                objectsToActivateOnEnable[i].SetActive(true);
            }

            tutorialScriptableObject.ShowJoystickArrows(vrDeviceToHighlight, vrInputToHighlight);
        }

        void OnEnable()
        {
            if (!started)
                return;

            int count = objectsToActivateOnEnable.Length;

            for (int i = 0; i < count; i++)
            {
                objectsToActivateOnEnable[i].SetActive(true);
            }

            tutorialScriptableObject.ShowJoystickArrows(vrDeviceToHighlight, vrInputToHighlight);
        }

        void OnDisable()
        {
            if (!started)
                return;

            int count = objectsToActivateOnEnable.Length;

            for (int i = 0; i < count; i++)
            {
                objectsToActivateOnEnable[i].SetActive(false);
            }

            tutorialScriptableObject.HideAllJoystickArrows();
        }

        void OnInputComplete()
        {
            executeOnInputComplete.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            bool userCompletedTutorialAction = false;
            
            if (completeStepOnInput)
            {
                if (vrDeviceToHighlight != VRController.VRDevice.None && vrInputToHighlight != VRController.VRInput.None)
                {
                    if (tutorialScriptableObject.vrController.InputActive(vrDeviceToHighlight, vrInputToHighlight, action))
                    {
                        userCompletedTutorialAction = true;
                    }
                }
            }

            if (userCompletedTutorialActionEventTriggered)
            {
                userCompletedTutorialAction = true;
                userCompletedTutorialActionEventTriggered = false;
            }


            if (userCompletedTutorialAction)
            {
                completions++;
                tutorial.Next(completions, requiredCompletions);
                userCompletedTutorialAction = false;
                if (completions >= requiredCompletions)
                    OnInputComplete();
            }
        }

        public void CompleteTutorialStep()
        {
            userCompletedTutorialActionEventTriggered = true;
        }
    }
}