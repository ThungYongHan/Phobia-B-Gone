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

public class SpiderPhobiaMenuCamera : MonoBehaviour
{
    // gameObject attached with collider hit by physics.raycast
    private GameObject _hitGameObject;
    // circular progress bar image 
    public Image progressCircle;
    // limit max distance for raycast range
    private const float MAXGazeDistance = 10;
    // time needed to complete one gaze interaction
    private const float TotalTime = 2.5f;
    // time spent gazing at gameObject
    private float _gazeTimer;
    
    // gaze interaction sound clip audio source
    public AudioSource audioSource;
    public AudioSource GameBGM;
    public AudioSource GazeBGM;
    
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    
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
    
    private GameObject DoctorCanvas;
    
    private string username;
    
    public GameObject ArachnophobiaSelectionMenuPanel;
    public GameObject GazeOptionPanel;
    public GameObject GameOptionPanel;
    public GameObject GazePrePanel;
    public GameObject GamePrePanel;
    
    public GameObject spiderSelectionMenu;
    private SpiderSelection _spiderSelectionScript;
    
    // default spider size for gaze (size s)
    public int selectedGazeSize = 1;
    // default number of spiders for gaze
    public int selectedGazeNum = 1;
    
    public int gazeVirtualTherapist = 1;
    public int gameVirtualTherapist = 1;
    
    public int gameBGM = 0;
    public int gazeBGM = 0;
    
    public Toggle GazeVirtualTherapistToggle;
    public Toggle GameVirtualTherapistToggle;
    public Toggle GameBGMToggle;
    public Toggle GazeBGMToggle;
    
    public Button gazeSmall;
    private ColorBlock _gazeColorS;
    public Button gazeMedium;
    private ColorBlock _gazeColorM;
    public Button gazeLarge;
    private ColorBlock _gazeColorL;

    private GameObject demo;
    private Button GazeNum1;
    private ColorBlock GazeColor1;
    private Button GazeNum3;
    private ColorBlock GazeColor3;
    private Button GazeNum5;
    private ColorBlock GazeColor5;
    private GameObject Doctor;
    public TextMeshPro instructionsBubbleText;

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
        _spiderSelectionScript = spiderSelectionMenu.GetComponent<SpiderSelection>();
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _auth = FirebaseAuth.DefaultInstance;
        // get currently logged-in user
        _user = _auth.CurrentUser;
        LoadPatientUsername();
        // cannot disable screen space camera canvas for some reason, so switching the code to call it as a gameobject and disabling it entirely
        DoctorCanvas = GameObject.Find("DoctorCanvas");
        
        GazeInstructionText = GameObject.Find("GazeInstructionText").GetComponent<TextMeshPro>();
        GameInstructionText = GameObject.Find("GameInstructionText").GetComponent<TextMeshPro>();
        
        Doctor = GameObject.Find("SpeechBubble");
        GazeDoctor = GameObject.Find("SpiderGazeFacts");
        SpiderGazeFactsText = GazeDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        GameDoctor = GameObject.Find("SpiderGameFacts");
        SpiderGameFactsText = GameDoctor.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        gazeSmall = GameObject.Find("GazeSmall").GetComponent<Button>();
        _gazeColorS = gazeSmall.colors;
        gazeMedium = GameObject.Find("GazeMedium").GetComponent<Button>();
        _gazeColorM = gazeMedium.colors;
        gazeLarge = GameObject.Find("GazeLarge").GetComponent<Button>();
        _gazeColorL = gazeLarge.colors;
        
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
        
        // default spider size for gaze
        if (selectedGazeSize == 1)
        {
            // set spider size 's' button colour to green
            _gazeColorS.normalColor = Color.green;
            gazeSmall.colors = _gazeColorS;
        }

        // default number of spiders for gaze
        if (selectedGazeNum == 1)
        {
            // set number of spiders '1' button colour to green
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
        // get information back from raycast (first gameObject attached with collider hit)
        RaycastHit hit;
        // cast a ray in the forward direction from the current position and store raycasthit data into hit
        if (Physics.Raycast(transform.position,transform.forward, out hit, MAXGazeDistance))
        {
            // add to the gazetimer for every moment the raycast is colliding with gameObject attached with collider
            _gazeTimer += Time.deltaTime;
            // fill the circularprogressbar (fully filled when the gazetimer is equal to time needed to complete gaze interaction)
            progressCircle.fillAmount = _gazeTimer / TotalTime;
            // get the gameObject's transform that the raycast collides with
            _hitGameObject = hit.transform.gameObject;
            
                // if the gazetimer is lower than the required gaze time
                if (_gazeTimer < TotalTime)
                {
                    // the name of the gameObject attached with collider that the ray collides with  
                    switch (_hitGameObject.name)
                    {
                        // if the gameObject name matches with the case, display corresponding text in doctor speech bubble
                        case "GazeExposureButton":
                            instructionsBubbleText.text = ("Exposure Session Where You Stare At Spiders");
                            break;
                        case "GamifiedExposureButton":
                            instructionsBubbleText.text = ("Exposure Session Where You Catch Spiders");
                            break;
                        case "TreatmentProgress":
                            instructionsBubbleText.text = ("View Your Arachnophobia Treatment Progress (Exit VR mode)");
                            break;
                        case "ReturnToSelectionMenuButton":
                            instructionsBubbleText.text = ("Go Back To Phobia Selection Menu (Exit VR mode)");
                            break;
                        case "NextSpider":
                        case "PreviousSpider":
                            instructionsBubbleText.text = ("Select Desired Spider Model For Exposure Tasks");
                            break;
                        case "SignOutAndQuitButton":
                            instructionsBubbleText.text = ("Sign Out And Quit Phobia-B-Gone After Answering the FSQ (Exit VR mode)");
                            break;
                    }
                }

                // if the gazetimer is higher than the required gaze time
                if (_gazeTimer > TotalTime)
                {
                    // reset the gazetimer to restart gaze interaction instance (prevent unwanted interactions)
                    _gazeTimer = 0;
                    // play gaze interact sound effect
                    audioSource.Play();
                    // the name of the gameObject attached with collider that the ray collides with  
                    switch (_hitGameObject.name)
                    {
                        // if the gameObject name matches with the case, execute the corresponding code
                        case "ReturnToSelectionMenuButton":
                            SceneManager.LoadScene("PhobiaSelectMenu");
                            break;
                        case "GazeSelect":
                            GazePrePanel.SetActive(false);
                            GazeOptionPanel.SetActive(true);
                            break;
                        // if user selects to start the gaze exposure session
                        case "GazeStart":
                            // set PlayerPrefs for gaze exposure bgm (on or off)
                            PlayerPrefs.SetInt("spidergazeBGM", gazeBGM);
                            // set PlayerPrefs for selected spider size
                            PlayerPrefs.SetInt("spiderselectedGazeSize", selectedGazeSize);
                            // set PlayerPrefs for selected spider number
                            PlayerPrefs.SetInt("spiderselectedGazeNum", selectedGazeNum);
                            // set PlayerPrefs for gaze exposure virtual therapist (enabled or disabled)
                            PlayerPrefs.SetInt("spidergazeVirtualTherapist", gazeVirtualTherapist);
                            // set PlayerPrefs for selected spider model
                            _spiderSelectionScript.SetSpider();
                            // load arachnophobia gaze exposure scene
                            SceneManager.LoadScene("SpiderGazeExposureTaskScene");
                            break;
                        case "GameStart":
                            GameOptionPanel.SetActive(true);
                            GamePrePanel.SetActive(false);
                            break;
                        case "RandomSpiderGazeFactsDoctor ":
                        {
                            Random rand = new Random();
                            int factsNum = rand.Next(0, spiderFacts.Length);
                            SpiderGazeFactsText.text = spiderFacts[factsNum];
                            break;
                        }
                        case "RandomSpiderGameFactsDoctor":
                        {
                            Random rand = new Random();
                            int factsNum = rand.Next(0, spiderFacts.Length);
                            SpiderGameFactsText.text = spiderFacts[factsNum];
                            break;
                        }
                        case "GameSelect":
                            PlayerPrefs.SetInt("spidergameVirtualTherapist", gameVirtualTherapist);
                            PlayerPrefs.SetInt("spidergameBGM", gameBGM);
                            _spiderSelectionScript.SetSpider();
                            SceneManager.LoadScene("SpiderBathroom");
                            break;
                        case "GameCancel":
                            GameBGM.Stop();
                            gameBGM = 0;
                            GameBGMToggle.isOn = false;
                            GameOptionPanel.SetActive(false);
                            DoctorCanvas.SetActive(true);
                            ArachnophobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GameBack":
                            DoctorCanvas.SetActive(true);
                            GamePrePanel.SetActive(false);
                            ArachnophobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GazeExposureButton":
                            DoctorCanvas.SetActive(false);
                            GazePrePanel.SetActive(true);
                            ArachnophobiaSelectionMenuPanel.SetActive(false);
                            break;
                        case "GamifiedExposureButton":
                            DoctorCanvas.SetActive(false);
                            GamePrePanel.SetActive(true);
                            ArachnophobiaSelectionMenuPanel.SetActive(false);
                            break;
                        case "TreatmentProgress":
                            SceneManager.LoadScene("TreatmentProgressSpider");
                            break;
                        case "NextSpider":
                            _spiderSelectionScript.NextSpider();
                            break;
                        case "PreviousSpider":
                            _spiderSelectionScript.PreviousSpider();
                            break;
                        case "GazeSmall":
                            _gazeColorS.normalColor = Color.green;
                            gazeSmall.colors = _gazeColorS;
                            _gazeColorM.normalColor = Color.white;
                            gazeMedium.colors = _gazeColorM;
                            _gazeColorL.normalColor = Color.white;
                            gazeLarge.colors = _gazeColorL;
                            selectedGazeSize = 1;
                            break;
                        case "GazeMedium":
                            // set spider size 's' button colour to white
                            _gazeColorS.normalColor = Color.white;
                            gazeSmall.colors = _gazeColorS;
                            // set size m button colour to green
                            _gazeColorM.normalColor = Color.green;
                            gazeMedium.colors = _gazeColorM;
                            // set size l button colour to white
                            _gazeColorL.normalColor = Color.white;
                            gazeLarge.colors = _gazeColorL;
                            // set selected gaze size integer to 2
                            selectedGazeSize = 2;
                            break;
                        case "GazeLarge":
                            _gazeColorS.normalColor = Color.white;
                            gazeSmall.colors = _gazeColorS;
                            _gazeColorM.normalColor = Color.white;
                            gazeMedium.colors = _gazeColorM;
                            _gazeColorL.normalColor = Color.green;
                            gazeLarge.colors = _gazeColorL;
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
                            // set number of spiders '1' button colour to white
                            GazeColor1.normalColor = Color.white;
                            GazeNum1.colors = GazeColor1;
                            // set number of spiders '3' button colour to green
                            GazeColor3.normalColor = Color.green;
                            GazeNum3.colors = GazeColor3;
                            // set number of spiders '5' button colour to white
                            GazeColor5.normalColor = Color.white;
                            GazeNum5.colors = GazeColor5;
                            // set selected gaze number integer to 3
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
                        
                        case "GazeCancel":
                            GazeBGM.Stop();
                            gazeBGM = 0;
                            GazeBGMToggle.isOn = false;
                            DoctorCanvas.SetActive(true);
                            GazeOptionPanel.SetActive(false);
                            ArachnophobiaSelectionMenuPanel.SetActive(true);
                            break;
                        case "GazeBack":
                            DoctorCanvas.SetActive(true);
                            GazePrePanel.SetActive(false);
                            ArachnophobiaSelectionMenuPanel.SetActive(true);
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
                        case "SignOutAndQuitButton":
                            SceneManager.LoadScene("SignOutSpiderEval");
                            break;
                    }
                // }
            }
            // else
            // {
            //     progressCircle.fillAmount = 0;
            //     _gazeTimer = 0;
            // }
        }
        // if physics.raycast is not colliding with any gameObjects attached with colliders
        else
        {
            instructionsBubbleText.text = ("Hello there, " + username + "! Welcome to Phobia-B-Gone for arachnophobia!");
            // reset the gazetimer to restart gaze interaction instance (prevent unwanted interactions)
            _gazeTimer = 0;
            // reset the circular progress bar fill amount
           progressCircle.fillAmount = 0;
        }
    }
    
    private void LoadPatientUsername()
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
