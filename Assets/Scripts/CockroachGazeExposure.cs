using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CockroachGazeExposure : MonoBehaviour
{
    public AudioSource progressBarFilledAudio, audioSource, GazeBGM;
    
    
    private int gazeVirtualTherapist;

    public Collider ExitCockroachGazeButton;
    public Collider ExitCockroachGazeCancelButton;
    
    public Collider BackToMainMenuButton;
    public Collider RetryGazeSessionButton;
    public Collider PillowCollider;
    public Canvas ExitCockroachGazeCanvas;
    public Canvas BackToMainMenuCanvas;

    public TextMeshPro BubbleText;
    
    public Canvas ProgressCanvas;
    public GameObject DoctorCanvas;
    
    private const float maxDistance = 10;
    private GameObject gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2.5f;
    public float gazeTimer;
    private GameObject parent;
    public Slider slider;
    // slider filling speed
    public float FillSpeed = 2f;
    
    private TextMeshProUGUI numTest;
    private TextMeshProUGUI numTest2;
    
    private int gazeBGM;

    public bool sliderMax = false;

    private void Awake()
    {
        gazeBGM = PlayerPrefs.GetInt("cockroachgazeBGM");
        if (gazeBGM == 1)
        {
            GazeBGM.volume = 1f;
            GazeBGM.Play();
        }
        BackToMainMenuButton.enabled = false;
        RetryGazeSessionButton.enabled = false;
        
        ExitCockroachGazeCanvas.enabled = false;
        ExitCockroachGazeButton.enabled = false;
        ExitCockroachGazeCancelButton.enabled = false;
        gazeVirtualTherapist = PlayerPrefs.GetInt("cockroachgazeVirtualTherapist");
        if (gazeVirtualTherapist == 1)
        {
            DoctorCanvas.SetActive(true);
        }
        else
        {
            DoctorCanvas.SetActive(false);
        }

        BackToMainMenuCanvas.enabled = false;
        ProgressCanvas = GameObject.Find("ProgressCanvas").GetComponent<Canvas>();
        parent = GameObject.Find("ProgressCanvas");
    }
    
    void FixedUpdate()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            gazedAtObject = hit.transform.gameObject;
            Debug.Log(gazedAtObject);
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
                ExitCockroachGazeCanvas.enabled = true;
                ExitCockroachGazeButton.enabled = true;
                ExitCockroachGazeCancelButton.enabled = true;
                PillowCollider.enabled = false; 
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name == "ExitCockroachGazeCancelButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                audioSource.Play();
                ExitCockroachGazeCanvas.enabled = false;
                ExitCockroachGazeButton.enabled = false;
                ExitCockroachGazeCancelButton.enabled = false;
                PillowCollider.enabled = true; 
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name == "ExitCockroachGazeButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                SceneManager.LoadScene("CockroachPhobiaMenu");
                gazeTimer = 0;
            }
        }
        if (gazedAtObject.name != "Plane" && gazedAtObject.name != "table_2" && 
            gazedAtObject.name != "PillowCollider" && gazedAtObject.name != "ExitCockroachGazeButton" && gazedAtObject.name != "ExitCockroachGazeCancelButton" )
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
        ExitCockroachGazeCanvas.enabled = false;
        ExitCockroachGazeButton.enabled = false;
        ExitCockroachGazeCancelButton.enabled = false;
        
        BackToMainMenuButton.enabled = true;
        RetryGazeSessionButton.enabled = true;
        if (gazedAtObject.name == "BackToMainMenuButton" )
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                SceneManager.LoadScene("CockroachPhobiaMenu");
                gazeTimer = 0;
            }
        }
        else if (gazedAtObject.name == "RetryGazeSessionButton")
        {
            imgCircle.fillAmount = gazeTimer / totalTime;
            if (gazeTimer > totalTime)
            {
                SceneManager.LoadScene("CockroachGazeExposureTaskScene");
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
