using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
[DefaultExecutionOrder(-300)]
public class TutorialScriptableObjectSpawner : MonoBehaviour
{
    public TutorialScriptableObject tutorialSO;

    // Start is called before the first frame update
    void Start()
    {
        tutorialSO.Initialize();
    }
}
}