using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParentToObject : MonoBehaviour
{
    [SerializeField] private Transform newParent = null;
    [SerializeField][FormerlySerializedAs("name")] private string parentGameObjectName = "";
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float waitTime = 0;

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
        if (parentGameObjectName != "")
        {
            GameObject parentGO = GameObject.Find(parentGameObjectName);
            if (parentGO != null && parentGO.activeSelf && gameObject.activeSelf)
            {
                transform.parent = parentGO.transform;
                transform.localPosition = Vector3.zero + offset;
                transform.localRotation = Quaternion.identity;
            }
        }
    }



}
