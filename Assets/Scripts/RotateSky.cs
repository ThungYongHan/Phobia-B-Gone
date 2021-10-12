using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float SkyRotateSpeed = 1.23f;
    private Material sky;
    void Start()
    { 
        sky = gameObject.GetComponent<Skybox>().material;
    }
    
    void Update()
    {
        //RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * RotateSpeed);
        sky.SetFloat("_Rotation", Time.time * SkyRotateSpeed);
    }
}
