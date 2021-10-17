using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpiderGameScript : MonoBehaviour
{
    public float timeRemaining = 90;
    public TextMeshPro timerText;

    public int gameComplete = 0;
    private int gameVirtualTherapist;
    private int gameBGM;
    
    public AudioSource GameBGM, spiderCatchFX, audioSource, spiders0Audio, gameOverAudio;
    
    private float startingPitch = 1.0f;
    
    public Collider LampCollider;
    public Collider ExitSpiderGameButton;
    public Collider ExitSpiderGameCancelButton;
    
    public Collider BackToMainMenuButton;
    public Collider RetryGameSessionButton;
    
    public Canvas ExitSpiderGameCanvas;
    public Canvas BackToMainMenuCanvas;
    
    public Canvas GameOverCanvas;
    public Collider GameOverBackToMainMenuButton;
    public Collider GameOverRetryGameSessionButton;
    
    public GameObject DoctorCanvas;
    
    public TextMeshPro BubbleText;
    
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    // changed gaze time for game script 
    public float totalTime = 0.3f;
    
    public float UITime = 2f;
    public float gazeTimer;

    public GameObject test;
    public LoadSpiderGame loadSpiderGame;
    
    public int scoreNum = 30;
    public TextMeshPro ScoreText;
    
    private void Awake()
    {
        BackToMainMenuButton.enabled = false;
        RetryGameSessionButton.enabled = false;
        BackToMainMenuCanvas.enabled = false;

        ExitSpiderGameCanvas.enabled = false;
        ExitSpiderGameButton.enabled = false;
        ExitSpiderGameCancelButton.enabled = false;

        GameOverCanvas.enabled = false;
        GameOverBackToMainMenuButton.enabled = false;
        GameOverRetryGameSessionButton.enabled = false;
        
        test = GameObject.Find("GameSpawn");
        loadSpiderGame = test.GetComponent<LoadSpiderGame>();
        gameVirtualTherapist = PlayerPrefs.GetInt("spidergameVirtualTherapist");
        gameBGM = PlayerPrefs.GetInt("spidergameBGM");
        if (gameBGM == 1)
        {
            GameBGM.volume = 0.3f;
            GameBGM.Play();
        }
        if (gameVirtualTherapist == 1)
        {
            DoctorCanvas.SetActive(true);
        }
        else
        {
            DoctorCanvas.SetActive(false);
        }
    }
    
    void Update()
    {
        // Debug.Log(_gazedAtObject);
        if (timeRemaining < 10)
        {
            BubbleText.text = "The time is almost up!";
        }
        
        if (timeRemaining <= 0)
        {
            GameBGM.Stop();
            gameOverAudio.Play();
            GameOverCanvas.enabled = true;
            GameOverBackToMainMenuButton.enabled = true;
            GameOverRetryGameSessionButton.enabled = true;
        }
        
        if (gameComplete == 0)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            DisplayTimeLeft(timeRemaining);
        }
        
        if (scoreNum == 0)
        {
            ExitSpiderGameCanvas.enabled = false;
            ExitSpiderGameButton.enabled = false;
            ExitSpiderGameCancelButton.enabled = false;
            LampCollider.enabled = false;
            
            BackToMainMenuCanvas.enabled = true;
            BackToMainMenuButton.enabled = true;
            RetryGameSessionButton.enabled = true;
            
        }
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            gazeTimer += Time.deltaTime;
            _gazedAtObject = hit.transform.gameObject;
            
            if (_gazedAtObject.name == "CuteSpiderGame(Clone)" || _gazedAtObject.name == "NormalSpiderGame(Clone)" || _gazedAtObject.name == "RealisticSpiderGame(Clone)")
            {
                if (scoreNum > 0){
                    
                    imgCircle.fillAmount = gazeTimer / totalTime;
                    if (gazeTimer > totalTime)
                    {
                        Debug.Log("You are looking at the spider!");
                        scoreNum -= 1;
                        ScoreText.text = (scoreNum).ToString();
                        // this only destroys the gameobject that I am looking at, not the other gameobjects with the same name
                        Destroy(_gazedAtObject);
                        spiderCatchFX.pitch = startingPitch;
                        startingPitch += 0.015f;
                        spiderCatchFX.Play();
                        if (scoreNum == 26 || scoreNum == 22 || scoreNum == 17 || scoreNum == 12 || scoreNum == 8  || scoreNum == 4) 
                        {
                            loadSpiderGame.spawnRandomLocation();
                        }
                        
                        if (scoreNum <= 26)
                        {
                            BubbleText.text =
                                "You can exit the gamified exposure task by gazing at the pink lamp on the wall!";
                        }
                        
                        if (scoreNum <= 22)
                        {
                            BubbleText.text =
                                "Just so you know, we will not harm these spiders but will release them outside after this session!";
                        }
                        
                        if (scoreNum <= 16)
                        {
                            BubbleText.text =
                                "By eating pests like fleas and mosquitoes, spiders can prevent the spread of disease!";
                        }
                        
                        if (scoreNum <= 12)
                        {
                            BubbleText.text =
                                "Keep it up!";
                        }

                        if (scoreNum <= 10)
                        {
                            BubbleText.text =
                                "Spiders actually eat more insects than birds and bats combined!";
                        }
                        
                        if (scoreNum <= 8)
                        {
                            BubbleText.text =
                                "You are doing great!";
                        }
                        
                        if (scoreNum <= 4)
                        {
                            BubbleText.text =
                                "You are almost there!";
                        }
                        
                        if (scoreNum == 0)
                        {
                            GameBGM.Stop();
                            spiders0Audio.Play();
                            gameComplete = 1;
                            BubbleText.text = "Well done! Thanks for catching the spiders!";
                        }
                    }
                }
            }
            
            else if (_gazedAtObject.name == "LampCollider")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    LampCollider.enabled = false;
                    ExitSpiderGameCanvas.enabled = true;
                    ExitSpiderGameButton.enabled = true;
                    ExitSpiderGameCancelButton.enabled = true;
                    /*ExitSpiderGazeCanvas.enabled = true;
                    ExitSpiderGazeButton.enabled = true;
                    ExitSpiderGazeCancelButton.enabled = true;
                    PillowCollider.enabled = false; */
                    gazeTimer = 0;
                }
                
            }
            else if (_gazedAtObject.name == "ExitSpiderGameCancelButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    ExitSpiderGameCanvas.enabled = false;
                    ExitSpiderGameButton.enabled = false;
                    ExitSpiderGameCancelButton.enabled = false;
                    LampCollider.enabled = true; 
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "ExitSpiderGameButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "BackToMainMenuButton" )
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "RetryGameSessionButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderBathroom");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "GameOverRetryGameSessionButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderBathroom");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "GameOverBackToMainMenuButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                    gazeTimer = 0;
                }
            }
            else
            {
                _gazedAtObject = null;
                gazeTimer = 0;
                imgCircle.fillAmount = 0;
            }
        }
    }
    
    void DisplayTimeLeft(float timeLeft)
    {
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
