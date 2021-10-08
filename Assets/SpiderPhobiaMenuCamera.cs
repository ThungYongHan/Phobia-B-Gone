using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Management;
using Random = System.Random;

public class SpiderPhobiaMenuCamera : MonoBehaviour
{
    /*/*Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;*/
    
    private string[] spiderFacts =
    {
        "Spiders eat more insects than birds and bats combined.",
        "For its weight, spider web silk is stronger and tougher than steel.",
        "Some species of jumping spiders can see light spectrums that humans cannot.",
        "Spiders are nearsighted.",
        "Spiders have pale blue blood.",
        "Spiders do not have muscles, but instead move their legs by shifting internal body fluid.",
        "Spiders help control insect populations such as flies and caterpillars.",
        "By eating pests like fleas and mosquitoes, spiders can prevent the spread of disease.",
        "Most tarantula species pose no threat to humans.",
        "Less than 0.5% of spider species are considered as potentially dangerous to humans."
    };
    
    public GameObject lasthit = null;
    private GameObject DoctorCanvas;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gazeStatus;
    private float gazeTimer;
    
    private SpiderSelection spiderSelectionScript;
    private GameObject GazeOptionPanel;
    private GameObject ArachnophobiaSelectionMenuPanel;
    private GameObject GazePrePanel;
    private GameObject GamePrePanel;
    
    private GameObject test;
    private GameObject test2;
    
    public int selectedGazeSize = 1;
    public int selectedGazeNum = 1;

    /*private BoxCollider NextSpider = null;
    private BoxCollider PreviousSpider = null;*/
    // private BoxCollider TreatmentProgress = null;
    /*private BoxCollider GazeSmall = null;
    private BoxCollider GazeMedium = null;
    private BoxCollider GazeLarge = null;*/
    
    private Button GazeSmall;
    private ColorBlock GazeColorS;
    private Button GazeMedium;
    private ColorBlock GazeColorM;
    private Button GazeLarge;
    private ColorBlock GazeColorL;

    private GameObject demo;
    private Button GazeNum1;
    private ColorBlock GazeColor1;
    private Button GazeNum3;
    private ColorBlock GazeColor3;
    private Button GazeNum5;
    private ColorBlock GazeColor5;
    private GameObject Doctor;
    public TextMeshPro BubbleText;

    private GameObject GazeDoctor;
    private GameObject GameDoctor;
    private TextMeshPro SpiderGazeFactsText;
    private TextMeshPro SpiderGameFactsText;
    private TextMeshPro GazeInstructionText;
    private TextMeshPro GazeRelaxtionText;
    
    private TextMeshPro GameInstructionText;
    private TextMeshPro GameRelaxtionText;

    public Camera mainCamera;
    void Start()
    {
        
        //InitializeFirebase();
        // cannot disable screen space camera canvas for some reason, so switching the code to call it as a gameobject and disabling it entirely
        DoctorCanvas = GameObject.Find("DoctorCanvas");
        
        GazeInstructionText = GameObject.Find("GazeInstructionText").GetComponent<TextMeshPro>();
        Debug.Log(GazeInstructionText);
        GazeRelaxtionText = GameObject.Find("GazeRelaxationText").GetComponent<TextMeshPro>();
        GazeRelaxtionText.enabled = false;
        
        GameInstructionText = GameObject.Find("GameInstructionText").GetComponent<TextMeshPro>();
        Debug.Log(GameInstructionText);
        GameRelaxtionText = GameObject.Find("GameRelaxationText").GetComponent<TextMeshPro>();
        GameRelaxtionText.enabled = false;
        
        Doctor = GameObject.Find("SpeechBubble");
        //SpeechBubbleText = Doctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        GazeDoctor = GameObject.Find("SpiderGazeFacts");
        SpiderGazeFactsText = GazeDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        GameDoctor = GameObject.Find("SpiderGameFacts");
        SpiderGameFactsText = GameDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        test2 = GameObject.Find("Main Camera");
        test = GameObject.Find("Spiders");
        spiderSelectionScript = test.GetComponent<SpiderSelection>();
        
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
        ArachnophobiaSelectionMenuPanel = GameObject.Find("ArachnophobiaSelectionMenuPanel");
        GazePrePanel = GameObject.Find("GazePrePanel");
        GazePrePanel.SetActive(false);
        
        GamePrePanel = GameObject.Find("GamePrePanel");
        GamePrePanel.SetActive(false);
        GazeOptionPanel.SetActive(false);
    }
    
    void FixedUpdate()
    { 
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            // lasthit = hit.transform.gameObject;
            gazeTimer += Time.deltaTime;
            imgCircle.fillAmount = gazeTimer / totalTime;
            // _gazedAtObject = lasthit;
            _gazedAtObject = hit.transform.gameObject;
            if (_gazedAtObject == hit.transform.gameObject)
            {
                // talk about how you let them view instructions?
                if (gazeTimer < totalTime)
                {
                    if (_gazedAtObject.name == "GazeExposureButton")
                    { 
                        BubbleText.text = ("Exposure Session Where You Stare At Spiders");
                    }
                    if (_gazedAtObject.name == "GamifiedExposureButton")
                    { 
                        BubbleText.text = ("Exposure Session Where You Try To Find Spiders");
                    }
                    if (_gazedAtObject.name == "TreatmentProgress")
                    { 
                        BubbleText.text = ("View Your Overall Treatment Progress");
                    }
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                        BubbleText.text = ("Go Back To Phobia Selection Menu");
                    }
                }

                if (gazeTimer > totalTime)
                {
                    if (_gazedAtObject.name == "RandomSpiderGazeFactsDoctor ")
                    {
                        /*String fact1 = "Spiders eat more insects than birds and bats combined";
                        String fact2 = "For its weight, spider web silk is stronger and tougher than steel.";
                        String fact3 = "Some species of jumping spiders can see light spectrums that humans cannot.";
                        String fact4 = "Spiders are nearsighted.";
                        String fact5 = "Spiders have pale blue blood.";
                        String fact6 = "Spiders do not have muscles, but instead move their legs by shifting internal body fluid";
                        String fact7 = "Spiders help control insect populations such as flies and caterpillars";
                        String fact8 = "By eating pests like fleas and mosquitoes, spiders can prevent the spread of disease";
                        String fact9 = "Most tarantula species pose no threat to humans";
                        String fact10 = "Less than 0.5% of spider species are considered as potentially dangerous to humans";*/
                        Random rand = new Random();
                        int factsNum = rand.Next(0, spiderFacts.Length);
                        Debug.Log(spiderFacts.Length);
                        SpiderGazeFactsText.text = spiderFacts[factsNum];
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "RandomSpiderGameFactsDoctor")
                    {
                        Random rand = new Random();
                        int factsNum = rand.Next(0, spiderFacts.Length);
                        Debug.Log(spiderFacts.Length);
                        SpiderGameFactsText.text = spiderFacts[factsNum];
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "NextGazeSlide")
                    {
                        GazeInstructionText.enabled = false;
                        GazeRelaxtionText.enabled = true;
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "PreviousGazeSlide")
                    {
                        GazeInstructionText.enabled = true;
                        GazeRelaxtionText.enabled = false;
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "NextGameSlide")
                    {
                        GameInstructionText.enabled = false;
                        GameRelaxtionText.enabled = true;
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "PreviousGameSlide")
                    {
                        GameInstructionText.enabled = true;
                        GameRelaxtionText.enabled = false;
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeStart")
                    {
                        //spiderSelectionScript.StartSession();
                        //SceneManager.LoadScene("SpiderExposureTaskScene");
                        //SceneManager.LoadScene("GazeOptionPanel");
                        GazePrePanel.SetActive(false);
                        GazeOptionPanel.SetActive(true);
                        gazeTimer = 0;
                    }

                    if (_gazedAtObject.name == "GameStart")
                    {
                        spiderSelectionScript.SetSpider();
                        SceneManager.LoadScene("SpiderBathroom");
                        gazeTimer = 0;
                    }
                                        
                    if (_gazedAtObject.name == "GameBack")
                    {
                        GamePrePanel.SetActive(false);
                        ArachnophobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeExposureButton")
                    {
                        //GazePrePanel.SetActive(true);
                        DoctorCanvas.SetActive(false);
                        GazePrePanel.SetActive(true);
                        ArachnophobiaSelectionMenuPanel.SetActive(false);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GamifiedExposureButton")
                    {
                        //GazePrePanel.SetActive(true);
                        DoctorCanvas.SetActive(false);
                        GamePrePanel.SetActive(true);
                        ArachnophobiaSelectionMenuPanel.SetActive(false);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                       SceneManager.LoadScene("2dphobiaselectmenu");
                    }
                    
                    if (_gazedAtObject.name == "TreatmentProgress")
                    {
                        SceneManager.LoadScene("TreatmentProgress2D");
                    }
                    
                    if (_gazedAtObject.name == "NextSpider")
                    {
                        //NextSpider.enabled = false;
                        spiderSelectionScript.NextSpider();
                        // this code is genius and totally unexpected
                        // it works because when you reset the timer, the timer starts again from 0 and it will have to wait until it is >2 seconds,
                        // making it look seamless when gazing at the same thing for a long time where it will enable and disable automatically
                        // also this will prevent the circle from being full in between scenes, which can cause unwanted interactions
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "PreviousSpider")
                    { 
                        spiderSelectionScript.PreviousSpider();
                        //PreviousSpider.enabled = false;
                        gazeTimer = 0;
                    }
                    
                    /*if (_gazedAtObject.name == "TreatmentProgress")
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
                    }*/
                    
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
                        spiderSelectionScript.SetSpider();
                        SceneManager.LoadScene("SpiderExposureTaskScene");
                        //GazePrePanel.SetActive(true);
                        // GazeOptionPanel.SetActive(false);
                        //scriptC.StartSession();
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeCancel")
                    {
                        GazeOptionPanel.SetActive(false);
                        ArachnophobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeBack")
                    {
                        GazePrePanel.SetActive(false);
                        GazeOptionPanel.SetActive(true);
                        //PhobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
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
           gazeTimer = 0;
           imgCircle.fillAmount = 0;
           //NextSpider.enabled = true;
           //PreviousSpider.enabled = true;
           // TreatmentProgress.enabled = true;
        }
    }
    /*// Handle initialization of the necessary firebase modules:
    void InitializeFirebase() {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

// Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }*/
}
