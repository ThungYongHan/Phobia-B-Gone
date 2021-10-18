using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CockroachGameScript : MonoBehaviour
{
    public float timeRemaining = 90;
    public TextMeshPro timerText;

    public int gameComplete = 0;
    private int gameVirtualTherapist;
    private int gameBGM;
    
    public AudioSource GameBGM, cockroachCatchFX, audioSource, cockroaches0Audio, gameOverAudio;
    
    private float startingPitch = 1.0f;
    
    public Collider LampCollider;
    public Collider ExitCockroachGameButton;
    public Collider ExitCockroachGameCancelButton;
    
    public Collider BackToMainMenuButton;
    public Collider RetryGameSessionButton;
    
    public Canvas ExitCockroachGameCanvas;
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
    public int gameOverPlayOnce = 0;
    public GameObject test;
    public LoadCockroachGame loadCockroachGame;
    
    public int scoreNum = 30;
    public TextMeshPro ScoreText;
    
    private void Awake()
    {
        BackToMainMenuButton.enabled = false;
        RetryGameSessionButton.enabled = false;
        BackToMainMenuCanvas.enabled = false;

        ExitCockroachGameCanvas.enabled = false;
        ExitCockroachGameButton.enabled = false;
        ExitCockroachGameCancelButton.enabled = false;

        GameOverCanvas.enabled = false;
        GameOverBackToMainMenuButton.enabled = false;
        GameOverRetryGameSessionButton.enabled = false;
        
        test = GameObject.Find("GameSpawn");
        loadCockroachGame = test.GetComponent<LoadCockroachGame>();
        gameVirtualTherapist = PlayerPrefs.GetInt("cockroachgameVirtualTherapist");
        gameBGM = PlayerPrefs.GetInt("cockroachgameBGM");
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
        if (timeRemaining < 10)
        {
            BubbleText.text = "The time is almost up!";
        }
        
        if (timeRemaining <= 0)
        {
            GameBGM.Stop();
            if (gameOverPlayOnce == 0)
            {
                gameOverAudio.Play();
                gameOverPlayOnce += 1;
            }
            
            GameOverCanvas.enabled = true;
            GameOverBackToMainMenuButton.enabled = true;
            GameOverRetryGameSessionButton.enabled = true;
            BubbleText.text = "Don't sweat it! You can always try again!";
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
            ExitCockroachGameCanvas.enabled = false;
            ExitCockroachGameButton.enabled = false;
            ExitCockroachGameCancelButton.enabled = false;
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
            
            if (_gazedAtObject.name == "CartoonCockroachGame(Clone)" || _gazedAtObject.name == "RealisticCockroachGame(Clone)")
            {
                if (timeRemaining > 0)
                {
                    if (scoreNum > 0)
                    {
                        imgCircle.fillAmount = gazeTimer / totalTime;
                        if (gazeTimer > totalTime)
                        {
                            
                                Debug.Log("You are looking at the cockroach!");
                                scoreNum -= 1;
                                ScoreText.text = (scoreNum).ToString();
                                // this only destroys the gameobject that I am looking at, not the other gameobjects with the same name
                                Destroy(_gazedAtObject);
                                cockroachCatchFX.Play();
                                if (scoreNum == 26 || scoreNum == 22 || scoreNum == 17 || scoreNum == 12 ||
                                    scoreNum == 8 || scoreNum == 4)
                                {
                                    loadCockroachGame.spawnRandomLocation();
                                }

                                if (scoreNum <= 26)
                                {
                                    BubbleText.text =
                                        "You can exit the gamified exposure task by gazing at the pink lamp on the wall!";
                                }

                                if (scoreNum <= 22)
                                {
                                    BubbleText.text =
                                        "Eliminating cockroaches can prevent the spread of disease like cholera";
                                }

                                if (scoreNum <= 16)
                                {
                                    BubbleText.text =
                                        "Keep it up!";
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
                                    cockroaches0Audio.Play();
                                    gameComplete = 1;
                                    BubbleText.text = "Well done! Thanks for eliminating the cockroaches!";
                                }
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
                    ExitCockroachGameCanvas.enabled = true;
                    ExitCockroachGameButton.enabled = true;
                    ExitCockroachGameCancelButton.enabled = true;
                    gazeTimer = 0;
                }
                
            }
            else if (_gazedAtObject.name == "ExitCockroachGameCancelButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    ExitCockroachGameCanvas.enabled = false;
                    ExitCockroachGameButton.enabled = false;
                    ExitCockroachGameCancelButton.enabled = false;
                    LampCollider.enabled = true; 
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "ExitCockroachGameButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("CockroachPhobiaMenu");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "BackToMainMenuButton" )
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("CockroachPhobiaMenu");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "RetryGameSessionButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("CockroachBathroom");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "GameOverRetryGameSessionButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("CockroachBathroom");
                    gazeTimer = 0;
                }
            }
            else if (_gazedAtObject.name == "GameOverBackToMainMenuButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    audioSource.Play();
                    SceneManager.LoadScene("CockroachPhobiaMenu");
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
