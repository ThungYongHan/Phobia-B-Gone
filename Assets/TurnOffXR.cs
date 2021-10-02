using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
public class TurnOffXR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("VR mode has been turned off");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
