using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
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
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;
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
    
    //public GameObject lasthit = null;
    private GameObject DoctorCanvas;
    private const float maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2.5f;
    private bool gazeStatus;
    private float gazeTimer;
    private string username;
    private SpiderSelection spiderSelectionScript;
    public GameObject ArachnophobiaSelectionMenuPanel;
    public GameObject GazeOptionPanel;
    public GameObject GameOptionPanel;
    public GameObject GazePrePanel;
    public GameObject GamePrePanel;
    
    private GameObject test;
    private GameObject test2;
    
    public int selectedGazeSize = 1;
    public int selectedGazeNum = 1;
    
    public int gazeVirtualTherapist = 1;
    public int gameVirtualTherapist = 1;

    public Toggle GazeVirtualTherapistToggle;
    public Toggle GameVirtualTherapistToggle;
    
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
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitializeFirebase();
        LoadPatientSpiderTreatmentData();
        // cannot disable screen space camera canvas for some reason, so switching the code to call it as a gameobject and disabling it entirely
        DoctorCanvas = GameObject.Find("DoctorCanvas");
        
        GazeInstructionText = GameObject.Find("GazeInstructionText").GetComponent<TextMeshPro>();
        GazeRelaxtionText = GameObject.Find("GazeRelaxationText").GetComponent<TextMeshPro>();
        GazeRelaxtionText.enabled = false;
        GameInstructionText = GameObject.Find("GameInstructionText").GetComponent<TextMeshPro>();
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
        //GazeOptionPanel = GameObject.Find("GazeOptionPanel");
        //ArachnophobiaSelectionMenuPanel = GameObject.Find("ArachnophobiaSelectionMenuPanel");
        //GazePrePanel = GameObject.Find("GazePrePanel");
        //GamePrePanel = GameObject.Find("GamePrePanel");

        GazePrePanel.SetActive(false);
        GazeOptionPanel.SetActive(false);
        GamePrePanel.SetActive(false);
        GameOptionPanel.SetActive(false);
        
        if (selectedGazeSize == 1)
        {
            GazeColorS.normalColor = Color.green;
            GazeSmall.colors = GazeColorS;
        }

        if (selectedGazeNum == 1)
        {
            GazeColor1.normalColor = Color.green;
            GazeNum1.colors = GazeColor1;
        }
    }
    
    void FixedUpdate()
    { 
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            
            // lasthit = hit.transform.gameObject;
            gazeTimer += Time.deltaTime;
            imgCircle.fillAmount = gazeTimer / totalTime;
            // _gazedAtObject = lasthit;
            _gazedAtObject = hit.transform.gameObject;
            Debug.Log(_gazedAtObject);
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
                        BubbleText.text = ("Exposure Session Where You Catch Spiders");
                    }
                    if (_gazedAtObject.name == "TreatmentProgress")
                    { 
                        BubbleText.text = ("View Your Arachnophobia Treatment Progress");
                    }
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                        BubbleText.text = ("Go Back To Phobia Selection Menu");
                    }
                    if (_gazedAtObject.name == "NextSpider" || _gazedAtObject.name == "PreviousSpider")
                    {
                        BubbleText.text = ("Select Desired Spider Model For Exposure Tasks");
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
                        GazePrePanel.SetActive(false);
                        GazeOptionPanel.SetActive(true);
                        gazeTimer = 0;
                    }

                    if (_gazedAtObject.name == "GameStart")
                    {
                        GameOptionPanel.SetActive(true);
                        GamePrePanel.SetActive(false);

                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GameSelect")
                    {
                        PlayerPrefs.SetInt("gameVirtualTherapist", gameVirtualTherapist);
                        spiderSelectionScript.SetSpider();
                        SceneManager.LoadScene("SpiderBathroom");
                        gazeTimer = 0;
                    }
                                 
                    if (_gazedAtObject.name == "GameCancel")
                    {
                        GameOptionPanel.SetActive(false);
                        DoctorCanvas.SetActive(true);
                        ArachnophobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GameBack")
                    {
                        DoctorCanvas.SetActive(true);
                        GamePrePanel.SetActive(false);
                        
                        ArachnophobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeExposureButton")
                    {
                        DoctorCanvas.SetActive(false);
                        GazePrePanel.SetActive(true);
                        ArachnophobiaSelectionMenuPanel.SetActive(false);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GamifiedExposureButton")
                    {
                        DoctorCanvas.SetActive(false);
                        GamePrePanel.SetActive(true);
                        ArachnophobiaSelectionMenuPanel.SetActive(false);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                       SceneManager.LoadScene("PhobiaSelectMenu");
                    }
                    
                    if (_gazedAtObject.name == "TreatmentProgress")
                    {
                        SceneManager.LoadScene("TreatmentProgressSpider");
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
                        PlayerPrefs.SetInt("gazeVirtualTherapist", gazeVirtualTherapist);
                        spiderSelectionScript.SetSpider();
                        SceneManager.LoadScene("SpiderGazeExposureTaskScene");
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeCancel")
                    {   
                        DoctorCanvas.SetActive(true);
                        GazeOptionPanel.SetActive(false);
                        ArachnophobiaSelectionMenuPanel.SetActive(true);
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeBack")
                    {
                        DoctorCanvas.SetActive(true);
                        GazePrePanel.SetActive(false);
                        ArachnophobiaSelectionMenuPanel.SetActive(true);

                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GazeVirtualTherapistToggle")
                    {
                        if (GazeVirtualTherapistToggle.isOn == true)
                        {
                            GazeVirtualTherapistToggle.isOn = false;
                            gazeVirtualTherapist = 0;
                        }
                        else if (GazeVirtualTherapistToggle.isOn == false)
                        {
                            GazeVirtualTherapistToggle.isOn = true;
                            gazeVirtualTherapist = 1;
                        }
                        gazeTimer = 0;
                    }
                    
                    if (_gazedAtObject.name == "GameVirtualTherapistToggle")
                    {
                        if (GameVirtualTherapistToggle.isOn == true)
                        {
                            GameVirtualTherapistToggle.isOn = false;
                            gameVirtualTherapist = 0;
                        }
                        else if (GameVirtualTherapistToggle.isOn == false)
                        {
                            GameVirtualTherapistToggle.isOn = true;
                            gameVirtualTherapist = 1;
                        }
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
            BubbleText.text = ("Hello there, " + username + "! Welcome to Phobia-B-Gone for arachnophobia!");
            _gazedAtObject = null;
           gazeTimer = 0;
           imgCircle.fillAmount = 0;
        }
    }
    
    void InitializeFirebase() {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

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
    }
    
    
    public void LoadPatientSpiderTreatmentData()
    {
        reference.Child("User").Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                username= snapshot.Child("Username").Value.ToString();
                Debug.Log(username);
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
}