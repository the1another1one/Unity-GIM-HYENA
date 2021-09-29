using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smooth_speed;

    // Update is called once per frame, when everything already happen, last one to execute

    private void FixedUpdate()
    {
        Vector3 desired_position = target.position + offset;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        transform.position = smoothed_position;

        /*
        if (Mathf.Abs(camera.position.x - target.position.x) >= x_offset)
        {
            camera.position = new Vector3(target.position.x, camera.position.y, camera.position.z);
        } */
    }
}
