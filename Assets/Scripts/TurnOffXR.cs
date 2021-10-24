using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;
public class TurnOffXR : MonoBehaviour
{
    public VrModeController turnOffVR;
    public GameObject vrModeController;
    
    void Awake()
    {
        turnOffVR = vrModeController.GetComponent<VrModeController>();
        turnOffVR.ExitVR();
    }
}
