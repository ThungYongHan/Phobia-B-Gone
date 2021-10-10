using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    //Camera FirstPersonCamera;
    /*void Start()
    {
        //FirstPersonCamera = Camera.main;
    }*/
    void Update()
    {
        // Camera.main.transform.forward gets the direction that the camera is facing
        // transform.position gets the position of the camera (like actual world position)  
        Vector3 dir = (-Camera.main.transform.forward * 10) - transform.position;
        dir.y = 0; // keep the direction strictly horizontal
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 2f * Time.deltaTime);
    }
}

