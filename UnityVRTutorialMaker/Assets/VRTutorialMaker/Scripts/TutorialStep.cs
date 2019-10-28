using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRTutorializer
{
public class TutorialStep : MonoBehaviour
{
    public enum ActivationType { Enabled, CloseToPlayer };

    public ActivationType activateWhen;

    public Tutorial tutorial;

    //The tutorial text for this tutorial step
    public string text;
    
    //The controller on which the user must press a button to complete this tutorial step
    //public OVRInput.Controller controller;

    //The button the user must press to complete this tutorial step
    //public OVRInput.Button button;
    
    public VRController.VRDevice vrDeviceToHighlight;
    public VRController.VRInput vrInputToHighlight;

    public UnityEvent executeOnEnable, executeOnDisable, executeOnInputComplete;

    public GameObject[] objectsToActivateOnEnable;

    //The executeOnCompletion event will be executed when the user presses the 
    //correct button to complete this tutorial step
    //public UnityEvent executeOnCompletion;

    Hashtable vrInputToOVRInput;

    bool stepCompleted;

    public int requiredCompletions = 1;
    int completions;

    public TutorialScriptableObject tutorialScriptableObject;

    void Start()
    {

    }

    public void ActivateStep()
    {   
        tutorial.SetTargetTransform(vrDeviceToHighlight, vrInputToHighlight);
    }
    
    void OnEnable()
    {
        int count = objectsToActivateOnEnable.Length;

        for(int i=0;i<count;i++)
        {
            objectsToActivateOnEnable[i].SetActive(true);
        }

        tutorialScriptableObject.ShowJoystickArrows(vrDeviceToHighlight, vrInputToHighlight);
    }

    void OnDisable()
    {
        int count = objectsToActivateOnEnable.Length;

        for(int i=0;i<count;i++)
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

        if (tutorialScriptableObject.vrController.InputActive(vrDeviceToHighlight, vrInputToHighlight))
        {
            userCompletedTutorialAction = true;
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
}
}