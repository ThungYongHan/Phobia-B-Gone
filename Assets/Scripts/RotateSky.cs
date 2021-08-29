using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float RotateSpeed = 1f;
    private Material sky1;
    void Start()
    { 
        sky1 = gameObject.GetComponent<Skybox>().material;
    }
    
    void Update()
    {
        //RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * RotateSpeed);
        sky1.SetFloat("_Rotation", Time.time * 1.23f);
    }
}
