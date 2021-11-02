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
    // audio source for progress bar fully filled sound,
    // gaze interaction sound, and gaze exposure background music
    public AudioSource progressBarFilledAudio, audioSource, GazeBGM;
    
    // to be used to store obtained PlayerPrefs value for toggleable virtual therapist
    // companion and background music in gaze exposure options menu
    private int gazeVirtualTherapist;
    private int gazeBGM;

    public Collider ExitSpiderGazeButton, ExitSpiderGazeCancelButton, 
        BackToMainMenuButton, RetryGazeSessionButton, PillowCollider;
    
    // progress bar canvas
    public Canvas ProgressCanvas;
    public Canvas ExitSpiderGazeCanvas;
    public Canvas BackToMainMenuCanvas;
    
    // virtual therapist companion speech bubble text
    public TextMeshPro BubbleText;
    // virtual therapist companion canvas 
    public GameObject DoctorCanvas;
    
    private GameObject _hitGameObject;
    public Image progressCircle;
    private const float MAXGazeDistance = 10;
    private const float TotalTime = 2.5f;
    private float _gazeTimer;
    
    // slider used for progress bar
    public Slider slider;
    // progress bar filling speed
    public float FillSpeed = 2f;
    // is progress bar fully filled? (default false)
    public bool sliderMax;

    private void Awake()
    {
        // get PlayerPrefs value for toggleable gaze exposure background music
        gazeBGM = PlayerPrefs.GetInt("spidergazeBGM");
        // if user has the gaze bgm toggle marked as checked
        if (gazeBGM == 1)
        {
            // play the gaze background music at normal volume
            GazeBGM.volume = 1f;
            GazeBGM.Play();
        }
        
        // get PlayerPrefs value for toggleable gaze exposure virtual therapist companion
        gazeVirtualTherapist = PlayerPrefs.GetInt("spidergazeVirtualTherapist");
        // if user has the gaze exposure virtual therapist companion toggle marked as checked
        if (gazeVirtualTherapist == 1)
        {
            // activate the virtual therapist companion canvas
            DoctorCanvas.SetActive(true);
        }
        else
        {
            // deactivate the virtual therapist companion canvas
            DoctorCanvas.SetActive(false);
        }
        
        // deactivate Return To Arachnophobia Menu canvas
        BackToMainMenuCanvas.enabled = false;
        BackToMainMenuButton.enabled = false;
        RetryGazeSessionButton.enabled = false;
        // deactivate Exit Arachnophobia Gaze Exposure canvas
        ExitSpiderGazeCanvas.enabled = false;
        ExitSpiderGazeButton.enabled = false;
        ExitSpiderGazeCancelButton.enabled = false;
    }
    
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, MAXGazeDistance))
        {
            _hitGameObject = hit.transform.gameObject;
            _gazeTimer += Time.deltaTime;
            if (_hitGameObject.name == "table_2")
            {
                _gazeTimer = 0;
            }
            // if progress bar is not fully filled
            if (sliderMax == false)
            {
                InsectGazing();
            }
            else
            {
                CompletedMenuGazing();
            }
        }
        else
        {
            _hitGameObject = null;
            _gazeTimer = 0;
            progressCircle.fillAmount = 0;
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
            BubbleText.text = "I want you to feel safe, these spiders are virtual!";
        }
        if (slider.value > 15f)
        {
            BubbleText.text = "You can exit the gaze exposure task by gazing at the square pillow " +
                              "on the bed at the back!";       
        }
        if (slider.value > 20f)
        {
            BubbleText.text = "Take it slow if you need to!";
        }
        if (slider.value > 25f)
        {
            BubbleText.text = "Take off the headset if you have to! We can always try again!";
        }
        if (slider.value > 35f)
        {
            BubbleText.text = "Take slow and deep breaths to calm yourself down if you are feeling " +
                              "distressed";
        }
        if (slider.value > 40f)
        {
            BubbleText.text = "You are doing great!";
        }
        if (slider.value > 50f)
        {
            BubbleText.text = "Spiders actually eat more insects than birds and bats combined!";
        }
        if (slider.value > 60f)
        {
            BubbleText.text = "By eating pests like fleas and mosquitoes, spiders help prevent the " +
                              "spread of disease!";
        }
        if (slider.value > 70f)
        {
            BubbleText.text = "Keep it up!";
        }
        if (slider.value > 80f)
        {
            BubbleText.text = "You are progressing really well!";
        }
        if (slider.value > 85f)
        {
            BubbleText.text = "Almost there!";
        }
        // when the slider reaches max value (progress bar fully filled)
        if (slider.value == 100.0f)
        {
            // play progress bar filled audio clip with reduced volume
            progressBarFilledAudio.volume = 0.4f;
            progressBarFilledAudio.Play();
            // set sliderMax bool to true to indicate that progress bar is fully filled
            sliderMax = true;
            BubbleText.text = "The progress bar is fully filled! You have completed the gaze " +
                              "exposure session!";
        }
        
        switch (_hitGameObject.name)
        {
            // square pillow on the bed at the back
            case "PillowCollider":
            {
                BubbleText.text = "You can exit the gaze exposure task by gazing at the square pillow on the bed at the back!"; 
                progressCircle.fillAmount = _gazeTimer / TotalTime;
                if (_gazeTimer > TotalTime)
                {
                    // play gaze interaction beep sound
                    audioSource.Play();
                    // enable exit gaze exposure task window canvas
                    ExitSpiderGazeCanvas.enabled = true;
                    ExitSpiderGazeButton.enabled = true;
                    ExitSpiderGazeCancelButton.enabled = true;
                    PillowCollider.enabled = false; 
                    _gazeTimer = 0;
                }
                break;
            }
            // confirm button
            case "ExitSpiderGazeCancelButton":
            {
                progressCircle.fillAmount = _gazeTimer / TotalTime;
                if (_gazeTimer > TotalTime)
                {
                    audioSource.Play();
                    ExitSpiderGazeCanvas.enabled = false;
                    ExitSpiderGazeButton.enabled = false;
                    ExitSpiderGazeCancelButton.enabled = false;
                    PillowCollider.enabled = true; 
                    _gazeTimer = 0;
                }
                break;
            }
            // cancel button
            case "ExitSpiderGazeButton":
            {
                progressCircle.fillAmount = _gazeTimer / TotalTime;
                if (_gazeTimer > TotalTime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                    _gazeTimer = 0;
                }
                break;
            }
        }
        
        // if the ray cast collides with spider colliders
        if (_hitGameObject.name == "CuteSpiderGaze(Clone)" || 
            _hitGameObject.name == "NormalSpiderGaze(Clone)" || 
            _hitGameObject.name == "RealisticSpiderGaze(Clone)")
        {
            // increase the progress bar slider value 
            // according to specified filling speed 
            slider.value += FillSpeed * Time.deltaTime;
        }
        
        // else
        // {
        //     _hitGameObject = null;
        // }
    }
    
    private void CompletedMenuGazing()
    {
        // deactivate progress bar canvas
        ProgressCanvas.enabled = false;
        // deactivate Exit Arachnophobia Gaze Exposure canvas
        ExitSpiderGazeCanvas.enabled = false;
        ExitSpiderGazeButton.enabled = false;
        ExitSpiderGazeCancelButton.enabled = false;
        
        // activate Return To Arachnophobia Menu canvas
        BackToMainMenuCanvas.enabled = true;
        BackToMainMenuButton.enabled = true;
        RetryGazeSessionButton.enabled = true;
        
        // the name of the gameObject with collider that the ray collides with 
        switch (_hitGameObject.name)
        {
            // confirm button
            case "BackToMainMenuButton":
            {
                // fill the circularprogressbar (fully filled when the gazetimer
                // is equal to time needed to complete gaze interaction)
                progressCircle.fillAmount = _gazeTimer / TotalTime;
                // if the gazetimer is higher than the required gaze time
                if (_gazeTimer > TotalTime)
                {
                    // play gaze interact sound effect
                    audioSource.Play();
                    // load the arachnophobia exposure therapy menu scene
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                    // reset the gazetimer to restart gaze interaction instance
                    // (prevent unwanted interactions)
                    _gazeTimer = 0;
                }
                break;
            }
            // retry button
            case "RetryGazeSessionButton":
            {
                progressCircle.fillAmount = _gazeTimer / TotalTime;
                if (_gazeTimer > TotalTime)
                {
                    audioSource.Play();
                    // load the current scene again
                    SceneManager.LoadScene("SpiderGazeExposureTaskScene");
                    _gazeTimer = 0;
                }
                break; 
            // if physics.raycast is not colliding with any gameObjects attached
            // with colliders
            }
            default:
                // reset the circular progress bar fill amount
                progressCircle.fillAmount = 0;
                _hitGameObject = null;
                break;
        }
    }
}
