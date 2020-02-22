using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : MonoBehaviour
{
    public enum Platform { None, OculusRift, OculusRiftS, OculusQuest, OculusGo, GearVR, HTCVive, ValveIndex, Other };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Platform GetPlatform()
    {
        string model = UnityEngine.XR.XRDevice.model.ToLower();
        
        Debug.Log("VR Device Model = "+model);

        Platform platform = Platform.None;
        
        if (model == "")
            return platform;
        
        if (model.Contains("vive"))
        {
            platform = Platform.HTCVive;
        }
        else if (model.Contains("rifts"))
        {
            platform = Platform.OculusRiftS;
        }
        else if (model.Contains("rift"))
        {
            platform = Platform.OculusRift;
        }
        else if (model.Contains("index"))
        {
            platform = Platform.ValveIndex;
        }
        else if (model.Contains("quest"))
        {
            platform = Platform.OculusQuest;
        }
        else if (model.Contains("go"))
        {
            platform = Platform.OculusGo;
        }
        else
        {
            platform = Platform.Other;
            Debug.Log("VR Device Model didn't match any supported devices!");
        }
        
        return platform;

    }
}
