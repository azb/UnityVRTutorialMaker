using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//The tutorial object
//There should only be one per scene
//Tutorial steps there can be multiple of

namespace VRTutorializer
{
[DefaultExecutionOrder(-10)]
public class Tutorial : MonoBehaviour
{
    public TextMeshPro tutorialText;

    public Material highlight, normal;

    DrawLineBetweenObjects tutorialLine;

    //Tutorial steps parent transform should contain game objects with TutorialStep component 
    //attached to children, where each game object represents another step in the tutorial
    public Transform tutorialStepsParent;

    TutorialStep[] tutorialSteps;

    int currentStep = 0;

    //[HideInInspector]

    FloatTowards floatTowards;

    AudioSource audioSource;
    
    public TutorialScriptableObject tutorialScriptableObject;

    TutorialActivator tutorialActivator;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Tutorial Start");


        tutorialActivator = GetComponentInParent<TutorialActivator>();
        
        tutorialLine = GetComponentInChildren<DrawLineBetweenObjects>();

        audioSource = GetComponent<AudioSource>();

        tutorialSteps = tutorialStepsParent.GetComponentsInChildren<TutorialStep>();

        floatTowards = GetComponent<FloatTowards>();
        
        floatTowards.target1 = TransformUtils.FindTransform("UITarget");

        int count = tutorialSteps.Length;

        for (int i = 0; i < count; i++)
        {
            tutorialSteps[i].tutorial = this;
        }

        EnableCurrentTutorialStep();
        SetTargetTransform(
            tutorialSteps[currentStep].vrDeviceToHighlight,
            tutorialSteps[currentStep].vrInputToHighlight
            );

        StartTutorialVibrationTimer();
    }

    void Update()
    {
        tutorialScriptableObject.UpdateFlash();
    }
    
    void StartTutorialVibrationTimer()
    {
        IEnumerator timer = TutorialVibrationTimer(1f);
        StartCoroutine(timer);
    }
    IEnumerator TutorialVibrationTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Do something
        if (currentStep < tutorialSteps.Length)
        {
            tutorialScriptableObject.vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);
            StartTutorialVibrationTimer();
        }
    }

    public void Next(int completions, int maxCompletions)
    {
        if (currentStep < tutorialSteps.Length)
        {
            tutorialScriptableObject.vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);

            if (completions >= maxCompletions)
            {
                currentStep++;

                if (currentStep < tutorialSteps.Length)
                {
                    tutorialScriptableObject.vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);
                    VRController.VRDevice vrDevice = tutorialSteps[currentStep].vrDeviceToHighlight;
                    VRController.VRInput vrInput = tutorialSteps[currentStep].vrInputToHighlight;
                    SetTargetTransform(vrDevice, vrInput);
                }
                else
                {
                    ResetInputHighlightMeshRenderer();
                    tutorialScriptableObject.HideAllJoystickArrows();

                    tutorialScriptableObject.SetTutorialOpen(null);

                    if (tutorialActivator.reoccuring)
                    {
                    currentStep = 0;
                    }
                    else
                    {
                    tutorialActivator.enabled = false;
                    }

                    gameObject.SetActive(false);
                }

                EnableCurrentTutorialStep();
            }
            else
            {
                tutorialText.text = tutorialSteps[currentStep].text + "\n" + completions + " / " + maxCompletions;
            }
        }
    }

    void EnableCurrentTutorialStep()
    {
        int count = tutorialSteps.Length;

        for (int i = 0; i < count; i++)
        {
            tutorialSteps[i].gameObject.SetActive(i == currentStep);
        }

        if (currentStep >= 0 && currentStep < count)
        {
            tutorialText.text = tutorialSteps[currentStep].text;
            tutorialSteps[currentStep].ActivateStep();
        }
    }

    public void SetTargetTransform(VRController.VRDevice vrDevice, VRController.VRInput vrInput)
    {
        if (vrDevice == VRController.VRDevice.LeftController)
        {
            floatTowards.target2 = tutorialScriptableObject.vrController.leftController;
        }
        else
        {
                Debug.Log("GetsHere11");


                Debug.Log("tutorialScriptableObject.vrController = "+tutorialScriptableObject.vrController);
                Debug.Log("tutorialScriptableObject.vrController.rightController = "+tutorialScriptableObject.vrController.rightController);


            floatTowards.target2 = tutorialScriptableObject.vrController.rightController;
        }

        ResetInputHighlightMeshRenderer();

        Transform newTarget = tutorialScriptableObject.vrController.VRInputToTransform(vrDevice, vrInput);

        tutorialScriptableObject.FlashObject(newTarget);

        if (newTarget != null)
            tutorialLine.object1 = newTarget;
        else
            Debug.LogError("newTarget is null");
    }

    void ResetInputHighlightMeshRenderer()
    {
        tutorialScriptableObject.ResetInputHighlight();

    }

}
}
