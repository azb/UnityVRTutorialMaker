using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static Transform FindTransform(string name)
    {
        GameObject foundGameObject = GameObject.Find(name);
        Transform foundTransform = null;
        
        if (foundGameObject == null)
        {
            Debug.LogError("Couldn't find a Transform named "+name);
        }
        else
        {
            foundTransform = foundGameObject.transform;
        }

        return foundTransform;
    }
}
