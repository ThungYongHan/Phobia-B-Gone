using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;
public class TurnOffXR : MonoBehaviour
{
    private VrModeController _turnOffVR;
    public GameObject vrModeController;
    
    void Awake()
    {
        _turnOffVR = vrModeController.GetComponent<VrModeController>();
        _turnOffVR.ExitVR();
    }
}
