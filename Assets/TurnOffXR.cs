using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;
public class TurnOffXR : MonoBehaviour
{
    public VrModeController turnOffVR;

    void Start()
    {
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        //Screen.orientation = ScreenOrientation.Portrait;
    }
    /*// Start is called before the first frame update
    void Start()
    {
        /*Debug.Log("VR mode has been turned off");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Screen.orientation = ScreenOrientation.Portrait;#1#
        StopXR();
    }
    
    
    public void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }*/
}
