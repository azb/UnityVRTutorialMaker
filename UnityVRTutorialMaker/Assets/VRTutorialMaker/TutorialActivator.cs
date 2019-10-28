using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
public class TutorialActivator : MonoBehaviour
{
    public Transform player;

    public float activationDistance;

    public enum ActivateBehavior { ThePlayerIsNearby , ThisGameObjectIsEnabled };

    public ActivateBehavior activateThisTutorialWhen;

    public enum DisactivateBehavior { ThePlayerWalksAway , ThisTutorialIsDone };

    public DisactivateBehavior deactivateThisTutorialWhen;

    public TutorialScriptableObject tutorialScriptableObject;

    GameObject child;

    float timeBetweenChecks = .1f;
    
    public bool reoccuring;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartProximityCheckTimer(timeBetweenChecks);
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);

    }

    void StartProximityCheckTimer(float waitTime)
    {
        IEnumerator timer = ProximityCheckTimer(waitTime);
        StartCoroutine(timer);
    }

    //This timer checks every second for if the player is close and if so activates the tutorial
    IEnumerator ProximityCheckTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Do something
        DoProximityActivation();
        StartProximityCheckTimer(timeBetweenChecks);
    }

    void DoProximityActivation()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position + player.forward);
        bool playerInRange = (distanceToPlayer < activationDistance);
        
        if (playerInRange)
            { 
            if (tutorialScriptableObject.GetTutorialOpen() == null 
                && activateThisTutorialWhen == ActivateBehavior.ThePlayerIsNearby)
                { 
                ActivateTutorial();
                }
            }
        else
            {
            if (tutorialScriptableObject.GetTutorialOpen() == this
                && deactivateThisTutorialWhen == DisactivateBehavior.ThePlayerWalksAway)
                {
                DeactivateTutorial();
                }
            }
    }

    void ActivateTutorial()
    {
        child.SetActive(true);
        tutorialScriptableObject.SetTutorialOpen(this);
    }

    void DeactivateTutorial()
    {
        child.SetActive(false);
        tutorialScriptableObject.SetTutorialOpen(null);
    }



    
    // Update is called once per frame
    void Update()
    {
        DoProximityActivation();
    }
}
}
