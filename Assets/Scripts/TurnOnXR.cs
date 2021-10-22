using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;

public class TurnOnXR : MonoBehaviour
{
    public VrModeController turnOnVR;
    //public DeviceOrientation orientation; 
    public GameObject VrModeController;
    
    // Start is called before the first frame update
    //turnOnVR = test.GetComponent<VrModeController>();
    void Awake()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        /*Screen.orientation = ScreenOrientation.LandscapeLeft;
        orientation = Input.deviceOrientation;
        Debug.Log(orientation.ToString());
        orientation = DeviceOrientation.LandscapeLeft;*/
        turnOnVR = VrModeController.GetComponent<VrModeController>();
        turnOnVR.EnterVR();
        Api.Recenter();
        /*StartCoroutine(StartXR());
        Screen.sleepTimeout = SleepTimeout.NeverSleep;*/
        //InitXR();
        // turnOnVR.EnterVR();

        // XRGeneralSettings.Instance.Manager.InitializeLoader();
        /*Debug.Log("VR mode has been turned on");
        StartCoroutine(StartXR());
        /*XRGeneralSettings.Instance.Manager.InitializeLoader();
        XRGeneralSettings.Instance.Manager.StartLoader();#1#
        //Screen.orientation = ScreenOrientation.Portrait;*/
    }
    /*public IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }*/
    /*
    public IEnumerator InitXR()
    {
        yield return  XRGeneralSettings.Instance.Manager.InitializeLoader();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }
    */
}
