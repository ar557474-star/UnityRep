using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    private Transform playerCarTransform;
    private Transform cameraPointTransform;

    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraPointTransform = playerCarTransform.Find("CameraPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(playerCarTransform);
       // transform.position = cameraPointTransform.position;
        transform.position = Vector3.SmoothDamp(transform.position,cameraPointTransform.position,ref velocity,0.1f);
    }
}
