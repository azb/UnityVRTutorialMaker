using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplayKey : MonoBehaviour
{
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(key))
        {
            Quit();
        }
    }

    public static void Quit()
     {
         #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
         #else
         Application.Quit();
         #endif
     }
}
