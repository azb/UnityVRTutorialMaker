using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialStep : MonoBehaviour
{
    public Tutorial tutorial;

    //The tutorial text for this tutorial step
    public string text;
    
    //The controller on which the user must press a button to complete this tutorial step
    //public OVRInput.Controller controller;

    //The button the user must press to complete this tutorial step
    //public OVRInput.Button button;
    
    public VRController.VRDevice vrDeviceToHighlight;
    public VRController.VRInput vrInputToHighlight;

    //The executeOnCompletion event will be executed when the user presses the 
    //correct button to complete this tutorial step
    public UnityEvent executeOnCompletion;

    Hashtable vrInputToOVRInput;

    bool stepCompleted;

    public void ActivateStep()
    {
        Debug.Log("ActivateStep() called for "+gameObject.name);
        
        tutorial.SetTargetTransform(vrDeviceToHighlight, vrInputToHighlight);
    }
    
    // Update is called once per frame
    void Update()
    {
        bool userCompletedTutorialAction = false;

        if (tutorial.vrController.InputActive(vrDeviceToHighlight, vrInputToHighlight))
        {
            userCompletedTutorialAction = true;
        }
        
        if (userCompletedTutorialAction && !stepCompleted)
        {
            stepCompleted = true;
            tutorial.Next();
        }
    }
}
