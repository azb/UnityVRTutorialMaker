using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTutorializer
{
    public class FloatTowards : MonoBehaviour
    {
        public Transform target1, target2;

        public Vector3 offset;

        public float speed;

        public float distance;

        // Update is called once per frame
        void Update()
        {
            if (target1 != null && target2 != null)
            {
                transform.position = (target1.position + target1.forward * distance + target2.position + target2.forward * distance) / 2f + offset;
            }
            else
            {
                if (target1 != null)
                {
                    transform.position = target1.position;
                }
            }
        }
    }
}