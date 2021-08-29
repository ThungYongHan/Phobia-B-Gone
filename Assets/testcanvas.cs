using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class testcanvas : MonoBehaviour
{
    public GameObject lasthit = null;
    //public Vector3 collision = Vector3.zero;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gvrStatus;
    public float gvrTimer;
    /*public UnityEvent GVRClick;
    public UnityEvent GVRClick2;*/
    public SpiderSelection scriptC;
    public GameObject test;
    private BoxCollider NextSpider = null;
    private BoxCollider PreviousSpider = null;

    void Start()
    {
        test = GameObject.Find("Spiders");
        scriptC = test.GetComponent<SpiderSelection>();
        NextSpider = GameObject.Find("NextSpider").GetComponent<BoxCollider>();
        PreviousSpider = GameObject.Find("PreviousSpider").GetComponent<BoxCollider>();
        //txt = GameObject.Find("Text").GetComponent<Text>();
        // then reference the gameobject's script
    }
    
    void FixedUpdate()
    { 
      //  txt = GameObject.Find("Text").GetComponent<Text>();
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            lasthit = hit.transform.gameObject;
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer > totalTime)
                {
                    if (_gazedAtObject.name == "TestSpiderButton")
                    { 
                        SceneManager.LoadScene("DemoScene");
                    }
                                        
                    if (_gazedAtObject.name == "BackSceneButton")
                    {
                       //  GVRClick2.Invoke();
                       SceneManager.LoadScene("selectphobia");
                    }
                    
                    if (_gazedAtObject.name == "NextSpider")
                    { 
                        scriptC.NextSpider();
                        NextSpider.enabled = false;
                    }
                    
                    if (_gazedAtObject.name == "PreviousSpider")
                    { 
                        scriptC.PreviousSpider();
                        PreviousSpider.enabled = false;
                    }
                    
                    if (_gazedAtObject.name == "TestCarrySpider")
                    { 
                        scriptC.StartSession();
                        NextSpider.enabled = false;
                    }
                }
            }
            else
            {
                imgCircle.fillAmount = 0;
            }
        }
        else
        {
           _gazedAtObject = null;
           gvrTimer = 0;
           imgCircle.fillAmount = 0;
           // this code is genius and totally unexpected
           // it works because when you disable the box collider of NextSpider, the raycast does not detect anything, which it then comes to this else clause, which
           // enables it again, making it look seamless
           NextSpider.enabled = true;
           PreviousSpider.enabled = true;
           //PreviousSpider.enabled = true;
        }
        /*if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
        }

        if (gvrTimer > totalTime)
        {
            GVRClick.Invoke();
        }*/
    }
    /*public void GvrOn()
    {
        gvrStatus = true; 
        
    }

    public void GvrOff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgCircle.fillAmount = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gvrStatus = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgCircle.fillAmount = 0;
    }*/
}
