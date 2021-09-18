using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    Camera FirstPersonCamera;

    void Start()
    {
        FirstPersonCamera = Camera.main;
        Debug.Log(FirstPersonCamera);
    }
    void Update()
    {
        /*if (target != null)
        {
            transform.LookAt(target);
        }*/
        // Camera.main.transform.forward gets the direction that the camera is facing
        // transform.position gets the position of the camera (like actual world position)  
        Vector3 dir = (-FirstPersonCamera.transform.forward * 10) - transform.position;
        dir.y = 0; // keep the direction strictly horizontal
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 2f * Time.deltaTime);
        
        // transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
        /*transform.Translate(Vector3.forward.normalized);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);*/
        // Vector3 dir = FirstPersonCamera.transform.position - transform.position;
        // Quaternion rot = Quaternion.LookRotation(dir);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rot, 100f * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(Camera.main.transform.forward - transform.position);
        
    }
    // transform.rotation + Camera.main.transform.forward * 5 * Time.deltaTime;
    }

