using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToObject : MonoBehaviour
{
    public Transform newParent;
    public string name;
    public Vector3 offset;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        if (waitTime > 0)
        {
            StartTimer(waitTime);
        }
        else
        {
            ZeroPosition();
        }
    }

    void StartTimer(float waitTime)
    {
        if (gameObject.activeSelf)
        {
            IEnumerator timer = Timer(waitTime);
            StartCoroutine(timer);
        }
    }

    IEnumerator Timer(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        //Do something
        if (gameObject.activeSelf)
        {
            ZeroPosition();
        }
    }


    void ZeroPosition()
    {

        if (newParent != null)
        {
            transform.parent = newParent;
            transform.localPosition = Vector3.zero + offset;
            transform.localRotation = Quaternion.identity;
        }
        else
        if (name != "")
        {
            GameObject parentGO = GameObject.Find(name);
            if (parentGO != null && parentGO.activeSelf && gameObject.activeSelf)
            {
                transform.parent = parentGO.transform;
                transform.localPosition = Vector3.zero + offset;
                transform.localRotation = Quaternion.identity;
            }
        }
    }



}
