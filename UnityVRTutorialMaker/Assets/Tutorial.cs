using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMesh tutorialText;

    public Material highlight, normal;

    public DrawLineBetweenObjects tutorialLine;

    float alpha, time;
    
    public TutorialStep[] tutorialSteps;
    int currentStep = 0;

    public VRController vrController;

    public FloatTowards floatTowards;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        //alpha = (Mathf.Sin(Time.frameCount / 10f) + 1f) / 2f;
        //rend.material.color = new Color(alpha,alpha,alpha,1); //SetColor("Albedo", new Color(alpha,alpha,alpha,1));
    }

    public void Next()
    {
        currentStep++;

        EnableCurrentTutorialStep();

        if (currentStep < tutorialSteps.Length)
        { 
        VRController.VRDevice vrDevice = tutorialSteps[currentStep].vrDeviceToHighlight;
        VRController.VRInput vrInput = tutorialSteps[currentStep].vrInputToHighlight;
        SetTargetTransform(vrDevice, vrInput);
        }
        else
        {
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
        
        Transform newTarget = vrController.VRInputToTransform(vrDevice, vrInput);

        Debug.Log("newTarget.name = "+newTarget.name);
        
        if (newTarget != null)
            tutorialLine.object1 = newTarget;
        else
            Debug.LogError("newTarget is null");
    }




}
