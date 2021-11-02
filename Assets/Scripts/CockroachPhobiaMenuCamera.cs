using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
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

public class CockroachPhobiaMenuCamera : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    
    public AudioSource GameBGM;
    public AudioSource GazeBGM;
    public AudioSource audioSource;
    
    
    private string[] cockroachFacts =
    {
        "Fossil evidence shows that cockroaches originated more than 300 million years ago.",
        "Cockroaches can hold their breath for 40 minutes, and can survive underwater for 30 minutes.",
        "Cockroaches can live for a week without its head.",
        "There are more than 4000 cockroach species around the world.",
        "Cockroaches feed on decaying organic matter, and releases nitrogen that is crucial for forest health.",
    };
    
    private GameObject DoctorCanvas;
    private const float maxDistance = 10;
    private GameObject _hitGameObject = null;
    public Image imgCircle;
    public float totalTime = 2.5f;
    private bool gazeStatus;
    private float gazeTimer;
    private string username;
    private CockroachSelection cockroachSelectionScript;
    public GameObject KatsaridaphobiaSelectionMenuPanel;
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

    public int gameBGM = 0;
    public int gazeBGM = 0;
    
    public Toggle GazeVirtualTherapistToggle;
    public Toggle GameVirtualTherapistToggle;
    public Toggle GameBGMToggle;
    public Toggle GazeBGMToggle;
    
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
    private TextMeshPro CockroachGazeFactsText;
    private TextMeshPro CockroachGameFactsText;
    private TextMeshPro GazeInstructionText;
    private TextMeshPro GazeRelaxtionText;
    
    private TextMeshPro GameInstructionText;
    private TextMeshPro GameRelaxtionText;

    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _auth = FirebaseAuth.DefaultInstance;
        // get currently logged-in user
        _user = _auth.CurrentUser;
        LoadPatientUsername();
        DoctorCanvas = GameObject.Find("DoctorCanvas");
        
        GazeInstructionText = GameObject.Find("GazeInstructionText").GetComponent<TextMeshPro>();
        //GazeRelaxtionText = GameObject.Find("GazeRelaxationText").GetComponent<TextMeshPro>();
        //GazeRelaxtionText.enabled = false;
        GameInstructionText = GameObject.Find("GameInstructionText").GetComponent<TextMeshPro>();
        //GameRelaxtionText = GameObject.Find("GameRelaxationText").GetComponent<TextMeshPro>();
        //GameRelaxtionText.enabled = false;
        
        Doctor = GameObject.Find("SpeechBubble");
        GazeDoctor = GameObject.Find("CockroachGazeFacts");
        CockroachGazeFactsText = GazeDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        GameDoctor = GameObject.Find("CockroachGameFacts");
        CockroachGameFactsText = GameDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        test2 = GameObject.Find("Main Camera");
        test = GameObject.Find("Cockroaches");
        cockroachSelectionScript = test.GetComponent<CockroachSelection>();
        
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
        
        if (gameBGM == 0)
        {
            GameBGMToggle.isOn = false;
        }
        
        if (gazeBGM == 0)
        {
            GazeBGMToggle.isOn = false;
        }
    }
    
    void FixedUpdate()
    { 
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            
            gazeTimer += Time.deltaTime;
            imgCircle.fillAmount = gazeTimer / totalTime;
            _hitGameObject = hit.transform.gameObject;
            //Debug.Log(_hitGameObject);
            // if (_hitGameObject == hit.transform.gameObject)
            // {
                if (gazeTimer < totalTime)
                {
                    switch (_hitGameObject.name)
                    {
                        case "GazeExposureButton":
                            BubbleText.text = ("Exposure Session Where You Stare At Cockroaches");
                            break;
                        case "GamifiedExposureButton":
                            BubbleText.text = ("Exposure Session Where You Catch Cockroaches");
                            break;
                        case "TreatmentProgress":
                            BubbleText.text = ("View Your Katsaridaphobia Treatment Progress (Exit VR mode)");
                            break;
                        case "BackSceneButton":
                            BubbleText.text = ("Go Back To Phobia Selection Menu (Exit VR mode)");
                            break;
                        case "NextCockroach":
                        case "PreviousCockroach":
                            BubbleText.text = ("Select Desired Cockroach Model For Exposure Tasks");
                            break;
                        case "SignOutAndQuitButton":
                            BubbleText.text = ("Sign Out And Quit Phobia-B-Gone After Answering the FCQ (Exit VR mode)");
                            break;
                    }
                }

                if (gazeTimer > totalTime)
                {
                    gazeTimer = 0;
                    audioSource.Play();
                    switch (_hitGameObject.name)
                    {
                        case "RandomCockroachGazeFactsDoctor ":
                        {
                            Random rand = new Random();
                            int factsNum = rand.Next(0, cockroachFacts.Length);
                            CockroachGazeFactsText.text = cockroachFacts[factsNum];
                            break;
                        }
                        case "RandomCockroachGameFactsDoctor":
                        {
                            Random rand = new Random();
                            int factsNum = rand.Next(0, cockroachFacts.Length);
                            CockroachGameFactsText.text = cockroachFacts[factsNum];
                            break;
                        }
                        // case "NextGazeSlide":
                        //     GazeInstructionText.enabled = false;
                        //     GazeRelaxtionText.enabled = true;
                        //     break;
                        // case "PreviousGazeSlide":
                        //     GazeInstructionText.enabled = true;
                        //     GazeRelaxtionText.enabled = false;
                        //     break;
                        // case "NextGameSlide":
                        //     GameInstructionText.enabled = false;
                        //     GameRelaxtionText.enabled = true;
                        //     break;
                        // case "PreviousGameSlide":
                        //     GameInstructionText.enabled = true;
                        //     GameRelaxtionText.enabled = false;
                        //     break;
                        case "GazeStart":
                            GazePrePanel.SetActive(false);
                            GazeOptionPanel.SetActive(true);
                            break;
                        case "GameStart":
                            GameOptionPanel.SetActive(true);
                            GamePrePanel.SetActive(false);
                            break;
                        case "GameSelect":
                            PlayerPrefs.SetInt("cockroachgameVirtualTherapist", gameVirtualTherapist);
                            PlayerPrefs.SetInt("cockroachgameBGM", gameBGM);
                            cockroachSelectionScript.SetCockroach();
                            SceneManager.LoadScene("CockroachBathroom");
                            break;
                        case "GameCancel":
                            GameBGM.Stop();
                            gameBGM = 0;
                            GameBGMToggle.isOn = false;
                            GameOptionPanel.SetActive(false);
                            DoctorCanvas.SetActive(true);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GameBack":
                            DoctorCanvas.SetActive(true);
                            GamePrePanel.SetActive(false);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GazeExposureButton":
                            DoctorCanvas.SetActive(false);
                            GazePrePanel.SetActive(true);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(false);
                            break;
                        case "GamifiedExposureButton":
                            DoctorCanvas.SetActive(false);
                            GamePrePanel.SetActive(true);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(false);
                            break;
                        case "BackSceneButton":
                            SceneManager.LoadScene("PhobiaSelectMenu");
                            break;
                        case "TreatmentProgress":
                            SceneManager.LoadScene("TreatmentProgressCockroach");
                            break;
                        case "NextCockroach":
                            cockroachSelectionScript.NextCockroach();
                            break;
                        case "PreviousCockroach":
                            cockroachSelectionScript.PreviousCockroach();
                            break;
                        case "GazeSmall":
                            GazeColorS.normalColor = Color.green;
                            GazeSmall.colors = GazeColorS;
                            GazeColorM.normalColor = Color.white;
                            GazeMedium.colors = GazeColorM;
                            GazeColorL.normalColor = Color.white;
                            GazeLarge.colors = GazeColorL;
                            selectedGazeSize = 1;
                            break;
                        case "GazeMedium":
                            GazeColorS.normalColor = Color.white;
                            GazeSmall.colors = GazeColorS;
                            GazeColorM.normalColor = Color.green;
                            GazeMedium.colors = GazeColorM;
                            GazeColorL.normalColor = Color.white;
                            GazeLarge.colors = GazeColorL;
                            selectedGazeSize = 2;
                            break;
                        case "GazeLarge":
                            GazeColorS.normalColor = Color.white;
                            GazeSmall.colors = GazeColorS;
                            GazeColorM.normalColor = Color.white;
                            GazeMedium.colors = GazeColorM;
                            GazeColorL.normalColor = Color.green;
                            GazeLarge.colors = GazeColorL;
                            selectedGazeSize = 3;
                            break;
                        case "GazeNum1":
                            GazeColor1.normalColor = Color.green;
                            GazeNum1.colors = GazeColor1;
                            GazeColor3.normalColor = Color.white;
                            GazeNum3.colors = GazeColor3;
                            GazeColor5.normalColor = Color.white;
                            GazeNum5.colors = GazeColor5;
                            selectedGazeNum = 1;
                            break;
                        case "GazeNum3":
                            GazeColor1.normalColor = Color.white;
                            GazeNum1.colors = GazeColor1;
                            GazeColor3.normalColor = Color.green;
                            GazeNum3.colors = GazeColor3;
                            GazeColor5.normalColor = Color.white;
                            GazeNum5.colors = GazeColor5;
                            selectedGazeNum = 3;
                            break;
                        case "GazeNum5":
                            GazeColor1.normalColor = Color.white;
                            GazeNum1.colors = GazeColor1;
                            GazeColor3.normalColor = Color.white;
                            GazeNum3.colors = GazeColor3;
                            GazeColor5.normalColor = Color.green;
                            GazeNum5.colors = GazeColor5;
                            selectedGazeNum = 5;
                            break;
                        case "GazeSelect":
                            PlayerPrefs.SetInt("cockroachgazeBGM", gazeBGM);
                            PlayerPrefs.SetInt("cockroachselectedGazeSize", selectedGazeSize);
                            PlayerPrefs.SetInt("cockroachselectedGazeNum", selectedGazeNum);
                            PlayerPrefs.SetInt("cockroachgazeVirtualTherapist", gazeVirtualTherapist);
                            cockroachSelectionScript.SetCockroach();
                            SceneManager.LoadScene("CockroachGazeExposureTaskScene");
                            break;
                        case "GazeCancel":
                            GazeBGM.Stop();
                            gazeBGM = 0;
                            GazeBGMToggle.isOn = false;
                            DoctorCanvas.SetActive(true);
                            GazeOptionPanel.SetActive(false);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GazeBack":
                            DoctorCanvas.SetActive(true);
                            GazePrePanel.SetActive(false);
                            KatsaridaphobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GazeVirtualTherapistToggle" when GazeVirtualTherapistToggle.isOn:
                            GazeVirtualTherapistToggle.isOn = false;
                            gazeVirtualTherapist = 0;
                            break;
                        case "GazeVirtualTherapistToggle":
                        {
                            if (GazeVirtualTherapistToggle.isOn == false)
                            {
                                GazeVirtualTherapistToggle.isOn = true;
                                gazeVirtualTherapist = 1;
                            }

                            break;
                        }
                        case "GameVirtualTherapistToggle" when GameVirtualTherapistToggle.isOn:
                            GameVirtualTherapistToggle.isOn = false;
                            gameVirtualTherapist = 0;
                            break;
                        case "GameVirtualTherapistToggle":
                        {
                            if (GameVirtualTherapistToggle.isOn == false)
                            {
                                GameVirtualTherapistToggle.isOn = true;
                                gameVirtualTherapist = 1;
                            }

                            break;
                        }
                        case "SignOutAndQuitButton":
                            SceneManager.LoadScene("SignOutCockroachEval");
                            break;
                        case "GameBGMToggle" when GameBGMToggle.isOn:
                            GameBGM.Stop();
                            GameBGMToggle.isOn = false;
                            gameBGM = 0;
                            break;
                        case "GameBGMToggle":
                            GameBGMToggle.isOn = true;
                            GameBGM.volume = 0.3f;
                            GameBGM.Play();
                            gameBGM = 1;
                            break;
                        case "GazeBGMToggle" when GazeBGMToggle.isOn:
                            GazeBGM.Stop();
                            GazeBGMToggle.isOn = false;
                            gazeBGM = 0;
                            break;
                        case "GazeBGMToggle":
                            GazeBGMToggle.isOn = true;
                            GazeBGM.volume = 0.3f;
                            GazeBGM.Play();
                            gazeBGM = 1;
                            break;
                    }
                }
            // }
            // // else
            // // {
            // //     imgCircle.fillAmount = 0;
            // //     gazeTimer = 0;
            // // }
        }
        else
        {
            BubbleText.text = ("Hello there, " + username + "! Welcome to Phobia-B-Gone for katsaridaphobia!");
            _hitGameObject = null;
           gazeTimer = 0;
           imgCircle.fillAmount = 0;
        }
    }
    public void LoadPatientUsername()
    {
        _reference.Child("User").Child(_user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
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
