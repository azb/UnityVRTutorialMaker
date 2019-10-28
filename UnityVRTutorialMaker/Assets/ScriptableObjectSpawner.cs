using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
public class ScriptableObjectSpawner : MonoBehaviour
{
    public TutorialScriptableObject tutorialSO;

    // Start is called before the first frame update
    void Awake()
    {
        tutorialSO.Initialize();
    }
}
}