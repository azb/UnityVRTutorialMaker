using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Tutorial : MonoBehaviour
{
    public TextMeshPro tutorialText;

    public Material highlight, normal;

    public DrawLineBetweenObjects tutorialLine;

    float alpha, time;

    //Tutorial steps parent transform should contain game objects with TutorialStep component 
    //attached to children, where each game object represents another step in the tutorial
    public Transform tutorialStepsParent;

    public TutorialStep[] tutorialSteps;

    int currentStep = 0;

    public VRController vrController;

    public FloatTowards floatTowards;

    MeshRenderer inputMeshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        tutorialSteps = tutorialStepsParent.GetComponentsInChildren<TutorialStep>();

        floatTowards = FindObjectOfType<FloatTowards>();
        vrController = FindObjectOfType<VRController>();

        int count = tutorialSteps.Length;

        for(int i=0;i<count;i++)
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
    
    MeshRenderer GetInputMeshRenderer(Transform input)
    {
        MeshRenderer result;

        result = input.GetComponent<MeshRenderer>();
        if (result == null)
            result = input.parent.GetComponent<MeshRenderer>();
        
        if (result == null)
            Debug.LogError("Input transform has no mesh renderer attached!");
        
        return result;
    }

    public Color highlightColor;
    Color originalColor;

    // Update is called once per frame
    void Update()
    {
        if (inputMeshRenderer != null)
        { 
        alpha = (Mathf.Sin(Time.frameCount / 10f) + 1f) / 2f;
        Color newColor = Color.Lerp(originalColor, highlightColor, alpha);
        inputMeshRenderer.material.color = newColor; //new Color(alpha,alpha,alpha,1); //SetColor("Albedo", new Color(alpha,alpha,alpha,1));
        }
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
            vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);
            StartTutorialVibrationTimer();
        }
    }

    public void Next()
    {
        vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);
        currentStep++;
        
        EnableCurrentTutorialStep();

        if (currentStep < tutorialSteps.Length)
        {
        vrController.Vibrate(tutorialSteps[currentStep].vrDeviceToHighlight, .03f, 1f, .5f);
        VRController.VRDevice vrDevice = tutorialSteps[currentStep].vrDeviceToHighlight;
        VRController.VRInput vrInput = tutorialSteps[currentStep].vrInputToHighlight;
        SetTargetTransform(vrDevice, vrInput);
        }
        else
        {
            ResetInputHighlightMeshRenderer();
            gameObject.SetActive(false);
        }
    }

    void EnableCurrentTutorialStep()
    {
        int count = tutorialSteps.Length;

        for(int i=0;i<count;i++)
        {
            tutorialSteps[i].gameObject.SetActive(i == currentStep);
        }

        if (currentStep >= 0 && currentStep < count)
        {
            tutorialText.text = tutorialSteps[currentStep].text;
            tutorialSteps[currentStep].ActivateStep();
            Debug.Log("Activating tutorial step "+currentStep);
        }
    }
    
    public void SetTargetTransform(VRController.VRDevice vrDevice, VRController.VRInput vrInput)
    {
        if (vrDevice == VRController.VRDevice.LeftController)
        {
            floatTowards.target = vrController.leftController;
        }
        else
        {
            floatTowards.target = vrController.rightController;
        }

        Debug.Log("vrInput = "+vrInput);

        //Debug.Log(vrInputToTransform[vrInput]);

        ResetInputHighlightMeshRenderer();

        Transform newTarget = vrController.VRInputToTransform(vrDevice, vrInput);

        inputMeshRenderer = GetInputMeshRenderer(newTarget);
        originalColor = inputMeshRenderer.material.color;

        Debug.Log("newTarget.name = "+newTarget.name);
        
        if (newTarget != null)
            tutorialLine.object1 = newTarget;
        else
            Debug.LogError("newTarget is null");
    }

    void ResetInputHighlightMeshRenderer()
    {
        if (inputMeshRenderer != null)
            inputMeshRenderer.material.color = originalColor;
        
    }





}
