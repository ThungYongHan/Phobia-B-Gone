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
    public Collider LampCollider;
    
    public Collider ExitSpiderGameButton;
    public Collider ExitSpiderGameCancelButton;
    
    public Canvas ExitSpiderGameCanvas;
    public Canvas BackToMainMenuCanvas;
    
    public Collider BackToMainMenuButton;
    public Collider RetryGameSessionButton;
    
    
    public GameObject DoctorCanvas;
    private int gameVirtualTherapist;
    public TextMeshPro BubbleText;

    //public GameObject lasthit = null;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    // changed gaze time for game script 
    public float totalTime = 0.3f;
    
    public float UITime = 2f;
    public float gazeTimer;
    /*
    private GameObject parent;
    */
    public GameObject test;

    public LoadSpiderGame loadSpiderGame;
    
    /*private TextMeshProUGUI numTest;
    private TextMeshProUGUI numTest2;*/
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
        
        test = GameObject.Find("GameSpawn");
        loadSpiderGame = test.GetComponent<LoadSpiderGame>();
        gameVirtualTherapist = PlayerPrefs.GetInt("gameVirtualTherapist");
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
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            //lasthit = hit.transform.gameObject;
            gazeTimer += Time.deltaTime;
            _gazedAtObject = hit.transform.gameObject;
            if (_gazedAtObject.name == "CuteSpiderGame(Clone)" || _gazedAtObject.name == "NormalSpiderGame(Clone)" || _gazedAtObject.name == "RealisticSpiderGame(Clone)")
            {
                //IncrementProgress(1.0f);
                imgCircle.fillAmount = gazeTimer / totalTime;
                if (gazeTimer > totalTime)
                {
                    Debug.Log("You are looking at the spider!");
                    scoreNum = scoreNum - 1;
                    ScoreText.text = (scoreNum).ToString();
                    // this only destroys the gameobject that I am looking at, not the other gameobjects with the same name
                    Destroy(_gazedAtObject);
                    if (scoreNum == 26 || scoreNum == 21 || scoreNum == 16 || scoreNum == 11 || scoreNum == 6 || scoreNum == 1) 
                    //if (scoreNum == 26 || scoreNum == 21 || scoreNum == 1 || scoreNum == 11 || scoreNum == 5)
                    {
                        BubbleText.text = "test";
                        loadSpiderGame.spawnRandomLocation();
                    }
                }
            }
            else if (_gazedAtObject.name == "LampCollider")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
                    if (gazeTimer > UITime)
                    {
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
            }
            else if (_gazedAtObject.name == "ExitSpiderGameCancelButton")
            {
                imgCircle.fillAmount = gazeTimer / UITime;
                if (gazeTimer > UITime)
                {
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
}
