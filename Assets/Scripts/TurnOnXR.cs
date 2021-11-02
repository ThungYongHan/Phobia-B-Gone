using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;

public class TurnOnXR : MonoBehaviour
{
    private VrModeController _turnOnVR;
    public GameObject vrModeController;
    
    void Awake()
    {
        // set screen orientation to landscape left
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        _turnOnVR = vrModeController.GetComponent<VrModeController>();
        // enter vr mode
        _turnOnVR.EnterVR();
    }
}

