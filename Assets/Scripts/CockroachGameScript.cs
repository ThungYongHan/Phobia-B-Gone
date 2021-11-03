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
    private GameObject _hitGameObject;
    public Image imgCircle;
    private const float MAXGazeDistance = 10;
    
    
    public float timeRemaining = 90;
    public TextMeshPro timerText;

    public bool gameComplete;
    public bool gameOverPlayOnce;
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
    
    
    // changed gaze time for game script 
    private float totalTime = 0.05f;
    
    private float UITime = 2.5f;
    public float gazeTimer;
    
    public GameObject gameSpawn;
    private LoadCockroachGame loadCockroachGame;
    
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
        
        gameSpawn = GameObject.Find("GameSpawn");
        loadCockroachGame = gameSpawn.GetComponent<LoadCockroachGame>();
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
    
    void FixedUpdate()
    {
        if (timeRemaining < 10)
        {
            BubbleText.text = "The time is almost up!";
        }
        
        if (timeRemaining <= 0)
        {
            GameBGM.Stop();
            if (gameOverPlayOnce == false)
            {
                gameOverAudio.Play();
                gameOverPlayOnce = true;
            }
            
            GameOverCanvas.enabled = true;
            GameOverBackToMainMenuButton.enabled = true;
            GameOverRetryGameSessionButton.enabled = true;
            BubbleText.text = "Don't sweat it! You can always try again!";
        }
        
        if (gameComplete == false)
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, MAXGazeDistance))
        {
            _hitGameObject = hit.transform.gameObject;
            gazeTimer += Time.deltaTime;
            
            switch (_hitGameObject.name)
            {
                case "CartoonCockroachGame(Clone)":
                case "RealisticCockroachGame(Clone)":
                {
                    if (timeRemaining > 0)
                    {
                        if (scoreNum > 0)
                        {
                            imgCircle.fillAmount = gazeTimer / totalTime;
                            if (gazeTimer > totalTime)
                            {
                                scoreNum -= 1;
                                ScoreText.text = (scoreNum).ToString();
                                // this only destroys the gameobject that I am looking at, not the other gameobjects with the same name
                                Destroy(_hitGameObject);
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
                                
                                if (scoreNum <= 12)
                                {
                                    BubbleText.text =
                                        "Eliminating cockroaches can prevent the triggering of allergies that can cause asthma";
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
                                    gameComplete = true;
                                    BubbleText.text = "Well done! Thanks for eliminating the cockroaches!";
                                }
                            }
                        }
                    }

                    break;
                }
                case "LampCollider":
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

                    break;
                }
                case "ExitCockroachGameCancelButton":
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

                    break;
                }
                case "ExitCockroachGameButton":
                {
                    imgCircle.fillAmount = gazeTimer / UITime;
                    if (gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("CockroachPhobiaMenu");
                        gazeTimer = 0;
                    }

                    break;
                }
                case "BackToMainMenuButton":
                {
                    imgCircle.fillAmount = gazeTimer / UITime;
                    if (gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("CockroachPhobiaMenu");
                        gazeTimer = 0;
                    }

                    break;
                }
                case "RetryGameSessionButton":
                {
                    imgCircle.fillAmount = gazeTimer / UITime;
                    if (gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("CockroachBathroom");
                        gazeTimer = 0;
                    }

                    break;
                }
                case "GameOverRetryGameSessionButton":
                {
                    imgCircle.fillAmount = gazeTimer / UITime;
                    if (gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("CockroachBathroom");
                        gazeTimer = 0;
                    }

                    break;
                }
                case "GameOverBackToMainMenuButton":
                {
                    imgCircle.fillAmount = gazeTimer / UITime;
                    if (gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("CockroachPhobiaMenu");
                        gazeTimer = 0;
                    }

                    break;
                }
                default:
                    _hitGameObject = null;
                    gazeTimer = 0;
                    imgCircle.fillAmount = 0;
                    break;
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
