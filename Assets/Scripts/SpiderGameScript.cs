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
    private GameObject _hitGameObject;
    public Image progressCircle;
    private const float MAXGazeDistance = 10;
    // shortened required gaze time for spider catching 
    private const float TotalTime = 0.05f;
    // normal required gaze time for non-spider collider interactions
    private const float UITime = 2.5f;
    private float _gazeTimer;
    
    // did the user fail to catch 30 spiders before the time limit
    // and the timer ran out? (default false)
    public bool gameOverPlayOnce;
    // did the user catch 30 spiders before the time limit? (default false)
    public bool gameComplete;
    
    // to be used to store obtained PlayerPrefs value for toggleable virtual therapist
    // companion and background music in gamified exposure options menu
    private int gameVirtualTherapist, gameBGM;
    
    // gamified exposure time limit used for countdown
    public float timeRemaining = 90;
    // spiders to be caught number 
    public int scoreNum = 30;
    
    // audio source for gamified exposure background music, spider catching sound,
    // gaze interaction sound, 30 spiders caught (game complete) sound, game over music
    public AudioSource GameBGM, spiderCatchFX, audioSource, spiders0Audio, gameOverAudio;
    
    // initial pitch for spider catch sound clip
    private float _startingPitch = 1.0f;
    public TextMeshPro TimerText, BubbleText, ScoreText;
    
    // spider spawning locations
    public GameObject gameSpawn;
    // virtual therapist companion canvas
    public GameObject DoctorCanvas;
    
    // spider spawning script 
    private LoadSpiderGame loadSpiderGameSpawn;
    
    public Collider LampCollider, BackToMainMenuButton, RetryGameSessionButton, 
        ExitSpiderGameButton, ExitSpiderGameCancelButton, GameOverBackToMainMenuButton, 
        GameOverRetryGameSessionButton;
    
    public Canvas ExitSpiderGameCanvas, BackToMainMenuCanvas, GameOverCanvas;
    
    private void Awake()
    {
        // deactivate Exit Arachnophobia Gamified Exposure canvas
        ExitSpiderGameCanvas.enabled = false;
        ExitSpiderGameButton.enabled = false;
        ExitSpiderGameCancelButton.enabled = false;
        
        // deactivate Return To Arachnophobia Menu canvas
        BackToMainMenuCanvas.enabled = false;
        BackToMainMenuButton.enabled = false;
        RetryGameSessionButton.enabled = false;
        
        // deactivate Game Over canvas
        GameOverCanvas.enabled = false;
        GameOverBackToMainMenuButton.enabled = false;
        GameOverRetryGameSessionButton.enabled = false;
        
        // spawn spiders 
        loadSpiderGameSpawn = gameSpawn.GetComponent<LoadSpiderGame>();
        
        // get PlayerPrefs value for virtual therapist companion
        // for gamified exposure toggle
        gameVirtualTherapist = PlayerPrefs.GetInt("spidergameVirtualTherapist");
        // get PlayerPrefs value for gamified exposure background music toggle
        gameBGM = PlayerPrefs.GetInt("spidergameBGM");
        
        // if the user checked the gamified exposure bgm toggle to on
        // in the gamified exposure options menu
        if (gameBGM == 1)
        {
            // play background music at reduced volume
            GameBGM.volume = 0.3f;
            GameBGM.Play();
        }
        // if the user checked the virtual therapist companion toggle to on
        // in the gamified exposure options menu
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
        // time remaining countdown has less than 10 seconds left
        if (timeRemaining < 10)
        {
            BubbleText.text = "The time is almost up!";
        }
        // time remaining countdown ran out (0 seconds left)
        if (timeRemaining <= 0)
        {
            // stop playing gamified exposure task background music
            GameBGM.Stop();
            if (gameOverPlayOnce == false)
            {
                // play the game over sound clip
                gameOverAudio.Play();
                // set game over bool to true so the game over 
                // sound clip will not play over and over
                gameOverPlayOnce = true;
            }
            
            // activate Game Over canvas
            GameOverCanvas.enabled = true;
            GameOverBackToMainMenuButton.enabled = true;
            GameOverRetryGameSessionButton.enabled = true;
            
            // activate Exit Arachnophobia Gamified Exposure canvas
            ExitSpiderGameCanvas.enabled = false;
            ExitSpiderGameButton.enabled = false;
            ExitSpiderGameCancelButton.enabled = false;
            // disabled collider attached to lamp 
            LampCollider.enabled = false;
            BubbleText.text = "Don't sweat it! You can always try again!";
        }
        
        // if the game is not completed yet
        if (gameComplete == false)
        {
            // if there is still time remaining
            if (timeRemaining > 0)
            {
                // minus the time remaining every second
                timeRemaining -= Time.deltaTime;
            }
            // display time remaining
            DisplayTimeLeft(timeRemaining);
        }
        
        // if user catches 30 spiders
        if (scoreNum == 0)
        {
            // deactivate Exit Arachnophobia Gamified Exposure canvas
            ExitSpiderGameCanvas.enabled = false;
            ExitSpiderGameButton.enabled = false;
            ExitSpiderGameCancelButton.enabled = false;
            LampCollider.enabled = false;
            
            // activate Return To Arachnophobia Menu canvas
            BackToMainMenuCanvas.enabled = true;
            BackToMainMenuButton.enabled = true;
            RetryGameSessionButton.enabled = true;
        }
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, MAXGazeDistance))
        {
            _hitGameObject = hit.transform.gameObject;
            _gazeTimer += Time.deltaTime;
            
            switch (_hitGameObject.name)
            {
                // if the casted ray collides with spiders with colliders
                case "CuteSpiderGame(Clone)":
                case "NormalSpiderGame(Clone)":
                case "RealisticSpiderGame(Clone)":
                {
                    // if there is still time remaining
                    if (timeRemaining > 0)
                    {
                        // if user has not caught 30 spiders
                        if (scoreNum > 0)
                        {
                            progressCircle.fillAmount = _gazeTimer / TotalTime;
                            if (_gazeTimer > TotalTime)
                            {
                                // decrease spiders to be caught number 
                                scoreNum -= 1;
                                ScoreText.text = (scoreNum).ToString();
                                // destroy currently gazed at spider gameobject
                                Destroy(_hitGameObject);
                                // set the spider catch sound clip pitch
                                spiderCatchFX.pitch = _startingPitch;
                                // increase the spider catch sound clip pitch each time the user catches a spider
                                _startingPitch += 0.015f;
                                // play spider catch sound clip 
                                spiderCatchFX.Play();
                                // if spiders to be caught number matches the specified number
                                if (scoreNum == 26 || scoreNum == 22 || scoreNum == 17 || scoreNum == 12 || scoreNum == 8 ||
                                    scoreNum == 4)
                                {
                                    // instantiate new spiders 
                                    loadSpiderGameSpawn.spawnRandomLocation();
                                }
                                if (scoreNum <= 26)
                                {
                                    BubbleText.text = "You can exit the gamified exposure task by gazing at the pink lamp on the wall!";
                                }
                                if (scoreNum <= 22)
                                {
                                    BubbleText.text = "Just so you know, we will not harm these spiders but will release them outside after this session!";
                                }
                                if (scoreNum <= 16)
                                {
                                    BubbleText.text = "By eating pests like fleas and mosquitoes, spiders can prevent the spread of disease!";
                                }
                                if (scoreNum <= 12)
                                {
                                    BubbleText.text = "Keep it up!";
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
                                // if user catches 30 spiders
                                if (scoreNum == 0)
                                {
                                    // stop gamified exposure background music
                                    GameBGM.Stop();
                                    // play 30 spiders caught (game complete) sound clip
                                    spiders0Audio.Play();
                                    // set game complete bool to true
                                    gameComplete = true;
                                    BubbleText.text = "Well done! Thanks for catching the spiders!";
                                }
                            }
                        }
                    }
                    break;
                }
                case "LampCollider":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        LampCollider.enabled = false;
                        ExitSpiderGameCanvas.enabled = true;
                        ExitSpiderGameButton.enabled = true;
                        ExitSpiderGameCancelButton.enabled = true;
                        _gazeTimer = 0;
                    }
                    break;
                }
                case "ExitSpiderGameCancelButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        ExitSpiderGameCanvas.enabled = false;
                        ExitSpiderGameButton.enabled = false;
                        ExitSpiderGameCancelButton.enabled = false;
                        LampCollider.enabled = true; 
                        _gazeTimer = 0;
                    }

                    break;
                }
                case "ExitSpiderGameButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("SpiderPhobiaMenu");
                        _gazeTimer = 0;
                    }

                    break;
                }
                case "BackToMainMenuButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("SpiderPhobiaMenu");
                        _gazeTimer = 0;
                    }

                    break;
                }
                case "RetryGameSessionButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("SpiderBathroom");
                        _gazeTimer = 0;
                    }

                    break;
                }
                case "GameOverRetryGameSessionButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("SpiderBathroom");
                        _gazeTimer = 0;
                    }

                    break;
                }
                case "GameOverBackToMainMenuButton":
                {
                    progressCircle.fillAmount = _gazeTimer / UITime;
                    if (_gazeTimer > UITime)
                    {
                        audioSource.Play();
                        SceneManager.LoadScene("SpiderPhobiaMenu");
                        _gazeTimer = 0;
                    }

                    break;
                }
                default:
                    _hitGameObject = null;
                    _gazeTimer = 0;
                    progressCircle.fillAmount = 0;
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
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
