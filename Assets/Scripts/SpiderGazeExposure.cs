using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpiderGazeExposure : MonoBehaviour
{
    public AudioSource progressBarFilledAudio, audioSource, GazeBGM;
    private int gazeVirtualTherapist;

    public Collider ExitSpiderGazeButton;
    public Collider ExitSpiderGazeCancelButton;
    
    public Collider BackToMainMenuButton;
    public Collider RetryGazeSessionButton;
    public Collider PillowCollider;
    public Canvas ExitSpiderGazeCanvas;
    public Canvas BackToMainMenuCanvas;
    
    public TextMeshPro BubbleText;
    
    public Canvas ProgressCanvas;
    public GameObject DoctorCanvas;
    
    private const float maxDistance = 10;
    private GameObject gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2.5f;
    public float gazeTimer;
    public Slider slider;
    // slider filling speed
    public float FillSpeed = 2f;

    private int gazeBGM;
    /*private BoxCollider OptionTest0 = null;
    private BoxCollider OptionTest1 = null;
    private BoxCollider OptionTest2 = null;
    private BoxCollider OptionTest3 = null;
    private BoxCollider OptionTest4 = null;

    private BoxCollider SelectButton = null;
    private BoxCollider CancelButton = null;
    private BoxCollider ConfirmButton = null;*/
    
    private TextMeshProUGUI numTest;
    private TextMeshProUGUI numTest2;
    
    //public JSONReadandWrite scriptB;
    
    public bool sliderMax = false;

    private void Awake()
    {
        gazeBGM = PlayerPrefs.GetInt("spidergazeBGM");
        if (gazeBGM == 1)
        {
            GazeBGM.volume = 1f;
            GazeBGM.Play();
        }
        
        BackToMainMenuButton.enabled = false;
        RetryGazeSessionButton.enabled = false;
        
        ExitSpiderGazeCanvas.enabled = false;
        ExitSpiderGazeButton.enabled = false;
        ExitSpiderGazeCancelButton.enabled = false;
        BackToMainMenuCanvas.enabled = false;
        
        gazeVirtualTherapist = PlayerPrefs.GetInt("spidergazeVirtualTherapist");
        if (gazeVirtualTherapist == 1)
        {
            DoctorCanvas.SetActive(true);
        }
        else
        {
            DoctorCanvas.SetActive(false);
        }
        
        ProgressCanvas = GameObject.Find("ProgressCanvas").GetComponent<Canvas>();
        
        /*if (test != null)
        {
            // then reference the gameobject's script
            //scriptB = test.GetComponent<JSONReadandWrite>();
        }*/
        //scriptB.SaveToJson();
       
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders

        // slider = parent.transform.GetChild(0).GetComponent<Slider>();
        // enable canvas in editor then disable canvas in awake in order to reference it
        //myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        //myCanvas.enabled = false;
    }
    
    void FixedUpdate()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            /*lasthit = hit.transform.gameObject;
            gazeTimer += Time.deltaTime; 
            _gazedAtObject = lasthit;*/ 
            // this is important to not trigger raycast with non spider objects
            gazedAtObject = hit.transform.gameObject;
            gazeTimer += Time.deltaTime;
            // this is to bypass the triggering gazetimer adding when looking at the tablecolliders (messing with the retry menu)
            if (gazedAtObject.name == "table_2")
            {
                gazeTimer = 0;
            }
            if (sliderMax == false)
            {
                InsectGazing();
            }
            else
            {
                ProgressCanvas.enabled = false;
                CompletedMenuGazing();
                /*
                imgCircle.fillAmount = gazeTimer / totalTime;
                _gazedAtObject = lasthit;
                if (_gazedAtObject.name == "BackToMainMenuButton")
                {
                    if (gazeTimer > totalTime)
                    {
                        if (_gazedAtObject.name == "BackToMainMenuButton")
                        {
                            SceneManager.LoadScene("SpiderPhobiaMenu");
                        }
                    }
                }
                else
                {
                    imgCircle.fillAmount = 0;
                }*/
                /*imgCircle.fillAmount = gazeTimer / totalTime;
                _gazedAtObject = lasthit;
                if (gazeTimer > totalTime)
                {
                    if (_gazedAtObject.name == "BackToMainMenuButton")
                    {
                        SceneManager.LoadScene("SpiderPhobiaMenu");
                    }
                }
                else
                {
                    imgCircle.fillAmount = 0;
                }*/
                //FormGazing();
                // this is crucial, need to set if(myCanvas1.enabled == false) so that enableCanvas1() is not called each frame, which previously
                // would have disabled "Confirm" and "Select" buttons in
                // myCanvas2 and also set all buttons in myCanvas 1 to be always active (interactable)
                /*if (myCanvas1.enabled == false)
                {
                    EnableCanvas1(); 
                }*/
            }
        }
        else
        {
            gazedAtObject = null;
            gazeTimer = 0;
            imgCircle.fillAmount = 0;
        }
    }

    private void InsectGazing()
    {
        if (slider.value < 5f)
        {
            BubbleText.text = "Gaze at the spiders to fill the progress bar!";
        }
        if (slider.value > 10f)
        {
            BubbleText.text = "You can exit the gaze exposure task by gazing at the square pillow on the bed at the back!";
        }
        if (slider.value > 20f)
        {
            BubbleText.text = "Take it slow if you need to!";
        }
        if (slider.value > 25f)
        {
            BubbleText.text = "Take deep breaths to calm yourself down if you are feeling distressed";
        }
        if (slider.value > 30f)
        {
            BubbleText.text = "Spiders actually eat more insects than birds and bats combined!";
        }
        if (slider.value > 45f)
        {
            BubbleText.text = "You are doing great!";
        }
        if (slider.value > 55f)
        {
            BubbleText.text = "By eating pests like fleas and mosquitoes, spiders can prevent the spread of disease!";
        }
        if (slider.value > 65f)
        {
            BubbleText.text = "Keep it up!";
        }
        if (slider.value > 75f)
        {
            BubbleText.text = "You are progressing really well!";
        }
        if (slider.value > 85f)
        {
            BubbleText.text = "Almost there!";
        }

        // when the slider is fully filled
        if (slider.value == 100.0f)
        {
            progressBarFilledAudio.volume = 0.4f;
            progressBarFilledAudio.Play();
            BackToMainMenuCanvas.enabled = true;
            sliderMax = true;
            BubbleText.text = "The progress bar is fully filled! You have completed the gaze exposure session!";
        }
        if (gazedAtObject.name == "PillowCollider")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                ExitSpiderGazeCanvas.enabled = true;
                ExitSpiderGazeButton.enabled = true;
                ExitSpiderGazeCancelButton.enabled = true;
                PillowCollider.enabled = false; 
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name == "ExitSpiderGazeCancelButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                ExitSpiderGazeCanvas.enabled = false;
                ExitSpiderGazeButton.enabled = false;
                ExitSpiderGazeCancelButton.enabled = false;
                PillowCollider.enabled = true; 
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name == "ExitSpiderGazeButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                SceneManager.LoadScene("SpiderPhobiaMenu");
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name != "Plane" && gazedAtObject.name != "table_2" && 
            gazedAtObject.name != "PillowCollider" && gazedAtObject.name != "ExitSpiderGazeButton" && gazedAtObject.name != "ExitSpiderGazeCancelButton" )
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        else
        {
            gazedAtObject = null;
        }
    }
    
    private void CompletedMenuGazing()
    {
        ExitSpiderGazeCanvas.enabled = false;
        ExitSpiderGazeButton.enabled = false;
        ExitSpiderGazeCancelButton.enabled = false;
        
        BackToMainMenuButton.enabled = true;
        RetryGazeSessionButton.enabled = true;
        if (gazedAtObject.name == "BackToMainMenuButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                SceneManager.LoadScene("SpiderPhobiaMenu");
                gazeTimer = 0;
            }
        }
        else if (gazedAtObject.name == "RetryGazeSessionButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                SceneManager.LoadScene("SpiderGazeExposureTaskScene");
                gazeTimer = 0;
            }
        }
        else
        {
            imgCircle.fillAmount = 0;
            gazedAtObject = null;
        }
    }
}
