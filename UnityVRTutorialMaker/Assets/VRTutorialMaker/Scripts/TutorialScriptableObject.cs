using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TutorialScriptableObject", order = 1)]
public class TutorialScriptableObject : ScriptableObject
{
    
    Transform
        rightJoystickClickArrow,
        rightJoystickRightArrow,
        rightJoystickLeftArrow,
        rightJoystickUpArrow,
        rightJoystickDownArrow,
        leftJoystickClickArrow,
        leftJoystickRightArrow,
        leftJoystickLeftArrow,
        leftJoystickUpArrow,
        leftJoystickDownArrow
        ;

    public TutorialActivator openTutorial;
    
    public VRController vrController;
    
    public Color highlightColor;
    Color originalColor;
    
    MeshRenderer inputMeshRenderer;
    
    float alpha, time;
        
    void GetJoystickClickArrowTransforms()
    {
        
        rightJoystickClickArrow = TransformUtils.FindTransform("RightJoystickClickArrow");
        rightJoystickRightArrow = TransformUtils.FindTransform("RightJoystickRightArrow");
        rightJoystickLeftArrow = TransformUtils.FindTransform("RightJoystickLeftArrow");
        rightJoystickUpArrow = TransformUtils.FindTransform("RightJoystickUpArrow");
        rightJoystickDownArrow = TransformUtils.FindTransform("RightJoystickDownArrow");

        leftJoystickClickArrow = TransformUtils.FindTransform("LeftJoystickClickArrow");
        leftJoystickRightArrow = TransformUtils.FindTransform("LeftJoystickRightArrow");
        leftJoystickLeftArrow = TransformUtils.FindTransform("LeftJoystickLeftArrow");
        leftJoystickUpArrow = TransformUtils.FindTransform("LeftJoystickUpArrow");
        leftJoystickDownArrow = TransformUtils.FindTransform("LeftJoystickDownArrow");

        

        rightJoystickClickArrow.gameObject.SetActive(false);
        rightJoystickRightArrow.gameObject.SetActive(false);
        rightJoystickLeftArrow.gameObject.SetActive(false);
        rightJoystickUpArrow.gameObject.SetActive(false);
        rightJoystickDownArrow.gameObject.SetActive(false);

        leftJoystickClickArrow.gameObject.SetActive(false);
        leftJoystickRightArrow.gameObject.SetActive(false);
        leftJoystickLeftArrow.gameObject.SetActive(false);
        leftJoystickUpArrow.gameObject.SetActive(false);
        leftJoystickDownArrow.gameObject.SetActive(false);

    }
    
    MeshRenderer GetInputMeshRenderer(Transform input)
    {
        MeshRenderer result = null;

        if (input != null)
        {
        result = input.GetComponent<MeshRenderer>();
        if (result == null)
            result = input.parent.GetComponent<MeshRenderer>();

        if (result == null)
            Debug.LogError("Input transform has no mesh renderer attached!");
        }

        return result;
    }

    public void SetTutorialOpen(TutorialActivator tutorial)
    {
        openTutorial = tutorial;
        if (openTutorial == null)
        {
            ResetInputHighlight();
        }
    }


    public TutorialActivator GetTutorialOpen()
    {
        return openTutorial;
    }
    
    // Initialize is called from ScriptableObjectSpawner.cs
    public void Initialize()
    {
        vrController = FindObjectOfType<VRController>();

        GetJoystickClickArrowTransforms();        
    }
    
    // Update is called once per frame
    public void UpdateFlash()
    {
        if (inputMeshRenderer != null)
        {
            alpha = (Mathf.Sin(Time.frameCount / 10f) + 1f) / 2f;
            Color newColor = Color.Lerp(originalColor, highlightColor, alpha);
            inputMeshRenderer.material.color = newColor; //new Color(alpha,alpha,alpha,1); //SetColor("Albedo", new Color(alpha,alpha,alpha,1));
        }
    }

    public void HideAllJoystickArrows()
    {
        SetHorizontalJoystickArrowVisibility(VRController.VRDevice.LeftController, false);
        SetVerticalJoystickArrowVisibility(VRController.VRDevice.LeftController, false);
        
        SetJoystickClickArrowVisibility(VRController.VRDevice.LeftController, false);

        SetHorizontalJoystickArrowVisibility(VRController.VRDevice.RightController, false);
        SetVerticalJoystickArrowVisibility(VRController.VRDevice.RightController, false);
        
        SetJoystickClickArrowVisibility(VRController.VRDevice.RightController, false);
    }

    public void ShowJoystickArrows(VRController.VRDevice vrDevice, VRController.VRInput vrInputToHighlight)
    {
        HideAllJoystickArrows();

        switch (vrInputToHighlight)
        {
            case VRController.VRInput.JoystickHorizontal:
                SetHorizontalJoystickArrowVisibility(vrDevice, true);
                break;
                
            case VRController.VRInput.JoystickVertical:
                SetVerticalJoystickArrowVisibility(vrDevice, true);
                break;
                
            case VRController.VRInput.JoystickClick:
                SetJoystickClickArrowVisibility(vrDevice, true);
                break;
        }
    }

    void SetJoystickClickArrowVisibility(VRController.VRDevice vrDevice, bool visible)
    {
        if (vrDevice == VRController.VRDevice.LeftController)
        {
        if (leftJoystickClickArrow != null)
        leftJoystickClickArrow.gameObject.SetActive(visible);
        }
        else
        { 
        if (rightJoystickClickArrow != null)
        rightJoystickClickArrow.gameObject.SetActive(visible);
        }
    }
    
    void SetHorizontalJoystickArrowVisibility(VRController.VRDevice vrDevice, bool visible)
    {
        if (vrDevice == VRController.VRDevice.LeftController)
        {
        if (leftJoystickRightArrow != null)
        leftJoystickRightArrow.gameObject.SetActive(visible);
        if (leftJoystickLeftArrow != null)
        leftJoystickLeftArrow.gameObject.SetActive(visible);
        }
        else
        { 
        if (rightJoystickRightArrow != null)
        rightJoystickRightArrow.gameObject.SetActive(visible);
        if (rightJoystickLeftArrow != null)
        rightJoystickLeftArrow.gameObject.SetActive(visible);
        }
    }

    void SetVerticalJoystickArrowVisibility(VRController.VRDevice vrDevice, bool visible)
    {
        if (vrDevice == VRController.VRDevice.LeftController)
        {
        if (leftJoystickUpArrow != null)
        leftJoystickUpArrow.gameObject.SetActive(visible);
        if (leftJoystickDownArrow != null)
        leftJoystickDownArrow.gameObject.SetActive(visible);
        }
        else
        { 
        if (rightJoystickUpArrow != null)
        rightJoystickUpArrow.gameObject.SetActive(visible);
        if (rightJoystickDownArrow != null)
        rightJoystickDownArrow.gameObject.SetActive(visible);
        }
    }
    
    public void ResetInputHighlight()
    {
        if (inputMeshRenderer != null)
            {
            inputMeshRenderer.material.color = originalColor;
            inputMeshRenderer = null;
            }
    }

    public void FlashObject(Transform objectToFlash)
    {
            Debug.Log("FlashObject calleds");


        inputMeshRenderer = GetInputMeshRenderer(objectToFlash);
        originalColor = inputMeshRenderer.material.color;
    }
}
}