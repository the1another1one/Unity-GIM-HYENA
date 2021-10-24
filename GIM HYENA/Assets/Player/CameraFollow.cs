using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping = 0.5f;

    private Vector3 smooth_speed = Vector3.zero;

    // Update is called once per frame, when everything already happen, last one to execute

    private void FixedUpdate()
    {
        // following player when its not destroyed
     if (target!= null)
        {
        Vector3 desired_position = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired_position, ref smooth_speed, damping);
        }
    }
}
