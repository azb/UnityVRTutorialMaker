using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//The tutorial object
//There should only be one per scene
//Tutorial steps there can be multiple of

namespace VRTutorializer
{
[DefaultExecutionOrder(-200)]
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

    SpriteRenderer spriteRenderer;

    [Header("Sound effect to play when a substep of a tutorial step is completed")] 
    public AudioClip subObjectiveCompleteSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

        EnableCurrentTutorialStep(0,1); //tutorialSteps[0].requiredCompletions);

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
                ResetInputHighlightMeshRenderer();
                
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

            }
            else
            {
                audioSource.PlayOneShot(subObjectiveCompleteSoundEffect);
            }
            
            EnableCurrentTutorialStep(completions, maxCompletions);
        }
    }

    void EnableCurrentTutorialStep(int completions, int maxCompletions)
    {
        int count = tutorialSteps.Length;

        for (int i = 0; i < count; i++)
        {
            tutorialSteps[i].gameObject.SetActive(i == currentStep);
        }

        if (completions >= maxCompletions || maxCompletions <= 1)
            {
            if (currentStep >= 0 && currentStep < count)
                {
                tutorialText.text = tutorialSteps[currentStep].text;
                tutorialSteps[currentStep].ActivateStep();
                
                if (tutorialSteps[currentStep].audioClip != null)
                    {
                    audioSource.Stop();
                    Debug.Log("Playing audio clip: "+tutorialSteps[currentStep].audioClip.name);
                    audioSource.PlayOneShot(tutorialSteps[currentStep].audioClip);
                    }
                }
            }
        else
            {
            tutorialText.text = tutorialSteps[currentStep].text + "\n" + completions + " / " + maxCompletions;
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
            floatTowards.target2 = tutorialScriptableObject.vrController.rightController;
        }

        ResetInputHighlightMeshRenderer();

        if (vrDevice != VRController.VRDevice.None && vrInput != VRController.VRInput.None)
            {
            Transform newTarget = tutorialScriptableObject.vrController.VRInputToTransform(vrDevice, vrInput);
                
            tutorialScriptableObject.FlashObject(newTarget);

            if (newTarget != null)
                {
                tutorialLine.enabled = true;
                tutorialLine.object1 = newTarget;
                }
            else
                {
                tutorialLine.enabled = false;
                tutorialLine.object1 = null;
                }
            }
        else
            {
                tutorialLine.enabled = false;
            }
    }

        
    public void SetTargetTransform(Transform target)
    {
        ResetInputHighlightMeshRenderer();

        tutorialLine.object1 = target;
    }

    void ResetInputHighlightMeshRenderer()
    {
        tutorialScriptableObject.ResetInputHighlight();
    }

}
}
