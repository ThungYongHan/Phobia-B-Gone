using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class testcanvas : MonoBehaviour
{
    public GameObject lasthit = null;

    public GameObject DoctorCanvas;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gvrStatus;
    public float gvrTimer;
    
    public SpiderSelection scriptC;
    public VrModeController scriptD;
    public GameObject GazeOptionPanel;
    public GameObject PhobiaSelectionMenuPanel;
    public GameObject GazePrePanel;

    public GameObject test;
    public GameObject test2;
    
    public int selectedGazeSize = 1;
    public int selectedGazeNum = 1;

    /*private BoxCollider NextSpider = null;
    private BoxCollider PreviousSpider = null;*/
    private BoxCollider TreatmentProgress = null;
    /*private BoxCollider GazeSmall = null;
    private BoxCollider GazeMedium = null;
    private BoxCollider GazeLarge = null;*/
    
    private Button GazeSmall;
    private ColorBlock GazeColorS;
    private Button GazeMedium;
    private ColorBlock GazeColorM;
    private Button GazeLarge;
    private ColorBlock GazeColorL;

    public GameObject demo;
    private Button GazeNum1;
    private ColorBlock GazeColor1;
    private Button GazeNum3;
    private ColorBlock GazeColor3;
    private Button GazeNum5;
    private ColorBlock GazeColor5;
    public GameObject Doctor;
    public TextMeshPro SpeechBubbleText;
    
    public TextMeshPro GazeInstructionText;
    public TextMeshPro RelaxationText;

    void Start()
    {
        // cannot disable screen space camera canvas for some reason, so switching the code to call it as a gameobject and disabling it entirely
        DoctorCanvas = GameObject.Find("DoctorCanvas");
        
        GazeInstructionText = GameObject.Find("GazeInstructionText").GetComponent<TextMeshPro>();
        Debug.Log(GazeInstructionText);
        RelaxationText = GameObject.Find("RelaxationText").GetComponent<TextMeshPro>();
        RelaxationText.enabled = false;
        Doctor = GameObject.Find("SpeechBubble");
        SpeechBubbleText = Doctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        // SpeechBubbleText.text = ("I hate myself");
        
        test2 = GameObject.Find("Main Camera");
        scriptD = test2.GetComponent<VrModeController>();

        test = GameObject.Find("Spiders");
        scriptC = test.GetComponent<SpiderSelection>();
        
        /*NextSpider = GameObject.Find("NextSpider").GetComponent<BoxCollider>();
        PreviousSpider = GameObject.Find("PreviousSpider").GetComponent<BoxCollider>();
        TreatmentProgress = GameObject.Find("TreatmentProgress").GetComponent<BoxCollider>();*/
        /*GazeSmall = GameObject.Find("GazeSmall").GetComponent<BoxCollider>();
        GazeMedium = GameObject.Find("GazeMedium").GetComponent<BoxCollider>();
        GazeLarge = GameObject.Find("GazeLarge").GetComponent<BoxCollider>();*/
        
        GazeSmall = GameObject.Find("GazeSmall").GetComponent<Button>();
        GazeColorS = GazeSmall.colors;
        GazeMedium = GameObject.Find("GazeMedium").GetComponent<Button>();
        GazeColorM = GazeMedium.colors;
        GazeLarge = GameObject.Find("GazeLarge").GetComponent<Button>();
        GazeColorL = GazeLarge.colors;
        
        GazeNum1 = GameObject.Find("GazeNum1").GetComponent<Button>();
        GazeColor1 = GazeNum1.colors;
        GazeNum3 = GameObject.Find("GazeNum3").GetComponent<Button>();
        GazeColor3 = GazeNum3.colors;
        GazeNum5 = GameObject.Find("GazeNum5").GetComponent<Button>();
        GazeColor5 = GazeNum5.colors;
        
        // then reference the gameobject's script
        GazeOptionPanel = GameObject.Find("GazeOptionPanel");
        PhobiaSelectionMenuPanel = GameObject.Find("PhobiaSelectionMenuPanel");
        GazePrePanel = GameObject.Find("GazePrePanel");
        GazePrePanel.SetActive(false);
        //  Debug.Log(GazeOptionPanel);
        GazeOptionPanel.SetActive(false);
    }
    
    void FixedUpdate()
    { 
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            lasthit = hit.transform.gameObject;
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer < totalTime)
                {
                    if (_gazedAtObject.name == "GazeExposureButton")
                    { 
                        SpeechBubbleText.text = ("Exposure Session Where You Stare At Spiders");
                    }
                    if (_gazedAtObject.name == "GamifiedExposureButton")
                    { 
                        SpeechBubbleText.text = ("Exposure Session Where You Try To Find Spiders");
                    }
                }

                if (gvrTimer > totalTime)
                {
                    if (_gazedAtObject.name == "NextSlide")
                    {
                        GazeInstructionText.enabled = false;
                        RelaxationText.enabled = true;
                    }
                    
                    if (_gazedAtObject.name == "PreviousSlide")
                    {
                        GazeInstructionText.enabled = true;
                        RelaxationText.enabled = false;
                    }
                    
                    if (_gazedAtObject.name == "GazeStart")
                    {
                        scriptC.StartSession();
                    }
                                        
                    if (_gazedAtObject.name == "GazeExposureButton")
                    {
                        //GazePrePanel.SetActive(true);
                        DoctorCanvas.SetActive(false);
                        GazeOptionPanel.SetActive(true);
                        PhobiaSelectionMenuPanel.SetActive(false);
                    }
                                        
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                       SceneManager.LoadScene("selectphobia");
                    }
                    
                    if (_gazedAtObject.name == "NextSpider")
                    {
                        //NextSpider.enabled = false;
                        scriptC.NextSpider();
                        // this code is genius and totally unexpected
                        // it works because when you reset the timer, the timer starts again from 0 and it will have to wait until it is >2 seconds,
                        // making it look seamless when gazing at the same thing for a long time where it will enable and disable automatically
                        // also this will prevent the circle from being full in between scenes, which can cause unwanted interactions
                        gvrTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "PreviousSpider")
                    { 
                        scriptC.PreviousSpider();
                        //PreviousSpider.enabled = false;
                        gvrTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "TestCarrySpider")
                    { 
                        scriptC.StartSession();
                        //NextSpider.enabled = false;
                    }
                    
                    if (_gazedAtObject.name == "TreatmentProgress")
                    {
                        if (scriptD._isVrModeEnabled)
                        {
                            scriptD.ExitVR();
                            TreatmentProgress.enabled = false;
                        }
                        else
                        {
                            scriptD.EnterVR();
                            TreatmentProgress.enabled = false;
                        }
                    }
                    
                    if (_gazedAtObject.name == "GazeSmall")
                    {
                        GazeColorS.normalColor = Color.green;
                        GazeSmall.colors = GazeColorS;
                        GazeColorM.normalColor = Color.white;
                        GazeMedium.colors = GazeColorM;
                        GazeColorL.normalColor = Color.white;
                        GazeLarge.colors = GazeColorL;
                        selectedGazeSize = 1;
                    }
                    
                    if (_gazedAtObject.name == "GazeMedium")
                    {
                        GazeColorS.normalColor = Color.white;
                        GazeSmall.colors = GazeColorS;
                        GazeColorM.normalColor = Color.green;
                        GazeMedium.colors = GazeColorM;
                        GazeColorL.normalColor = Color.white;
                        GazeLarge.colors = GazeColorL;
                        selectedGazeSize = 2;
                    }
                    
                    if (_gazedAtObject.name == "GazeLarge")
                    {
                        GazeColorS.normalColor = Color.white;
                        GazeSmall.colors = GazeColorS;
                        GazeColorM.normalColor = Color.white;
                        GazeMedium.colors = GazeColorM;
                        GazeColorL.normalColor = Color.green;
                        GazeLarge.colors = GazeColorL;
                        selectedGazeSize = 3;
                    }
                    
                    if (_gazedAtObject.name == "GazeNum1")
                    {
                        GazeColor1.normalColor = Color.green;
                        GazeNum1.colors = GazeColor1;
                        GazeColor3.normalColor = Color.white;
                        GazeNum3.colors = GazeColor3;
                        GazeColor5.normalColor = Color.white;
                        GazeNum5.colors = GazeColor5;
                        selectedGazeNum = 1;
                    }
                    
                    if (_gazedAtObject.name == "GazeNum3")
                    {
                        GazeColor1.normalColor = Color.white;
                        GazeNum1.colors = GazeColor1;
                        GazeColor3.normalColor = Color.green;
                        GazeNum3.colors = GazeColor3;
                        GazeColor5.normalColor = Color.white;
                        GazeNum5.colors = GazeColor5;
                        selectedGazeNum = 3;
                    }
                    
                    if (_gazedAtObject.name == "GazeNum5")
                    {
                        GazeColor1.normalColor = Color.white;
                        GazeNum1.colors = GazeColor1;
                        GazeColor3.normalColor = Color.white;
                        GazeNum3.colors = GazeColor3;
                        GazeColor5.normalColor = Color.green;
                        GazeNum5.colors = GazeColor5;
                        selectedGazeNum = 5;
                    }
                    
                    if (_gazedAtObject.name == "GazeSelect")
                    {
                        PlayerPrefs.SetInt("selectedGazeSize", selectedGazeSize);
                        PlayerPrefs.SetInt("selectedGazeNum", selectedGazeNum);
                        
                        
                        GazePrePanel.SetActive(true);
                        GazeOptionPanel.SetActive(false);
                        //scriptC.StartSession();
                        gvrTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeCancel")
                    {
                        GazeOptionPanel.SetActive(false);
                        PhobiaSelectionMenuPanel.SetActive(true);
                        gvrTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeBack")
                    {
                        GazePrePanel.SetActive(false);
                        GazeOptionPanel.SetActive(true);
                        //PhobiaSelectionMenuPanel.SetActive(true);
                        gvrTimer = 0;
                    }
                }
            }
            else
            {
                imgCircle.fillAmount = 0;
            }
        }
        else
        {
           _gazedAtObject = null;
           gvrTimer = 0;
           imgCircle.fillAmount = 0;
           //NextSpider.enabled = true;
           //PreviousSpider.enabled = true;
           // TreatmentProgress.enabled = true;
        }
    }
}
