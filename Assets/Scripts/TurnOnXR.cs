using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;

public class TurnOnXR : MonoBehaviour
{
    public VrModeController turnOnVR;
    public GameObject vrModeController;
    
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        turnOnVR = vrModeController.GetComponent<VrModeController>();
        turnOnVR.EnterVR();
        Api.Recenter();
    }
}
