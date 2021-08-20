using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testingform : MonoBehaviour
{
   // private Text txt;
    public GameObject lasthit = null;
    //public Vector3 collision = Vector3.zero;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gvrStatus;
    public float gvrTimer;
    public UnityEvent GVRClick;
    public UnityEvent GVRClick2;
    public Canvas myCanvas;
    private BoxCollider ButtonTest1 = null;
    private BoxCollider ButtonTest2 = null;
    private BoxCollider NoButton1 = null;
    private BoxCollider ConfirmButton1 = null;

    //public GameObject testObject;
    
    void Start()
    {
        //txt = GameObject.Find("Text").GetComponent<Text>();
        ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
        ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        NoButton1 = GameObject.Find("NoButton1").GetComponent<BoxCollider>();
        ConfirmButton1 = GameObject.Find("ConfirmButton1").GetComponent<BoxCollider>();
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders
        NoButton1.enabled = false;
        ConfirmButton1.enabled = false;
        /*if (testObject != null)
        {
            myCanvas = testObject.GetComponent<Canvas>();
            
            Debug.Log("testcanvas");
        }*/
        myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        myCanvas.enabled = false;
    }
    
    
    // Update is called once per frame
    void Update()
    { 
      //  txt = GameObject.Find("Text").GetComponent<Text>();
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            lasthit = hit.transform.gameObject;
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer > totalTime)
                {
                    Debug.Log("hello");
                    if (_gazedAtObject.name == "ButtonTest1")
                    {
                        enableCanvas1();
                        /*if (testObject != null)
                        {
                            myCanvas = testObject.GetComponent<Canvas>();
                            myCanvas.enabled = true;
                            Debug.Log("testcanvas");
                        }*/
                    }
                    if (_gazedAtObject.name == "NoButton1")
                    {
                        disableCanvas1();
                        /*myCanvas.enabled = false;
                        ButtonTest1.enabled = true;
                        ButtonTest2.enabled = true;
                        NoButton1.enabled = false;
                        ConfirmButton1.enabled = false;*/
                    }                
                    if (_gazedAtObject.name == "ButtonTest2")
                    {
                        GVRClick2.Invoke();
                    }
                }
            }
            else
            {
                imgCircle.fillAmount = 0;
            }

            /*//_gazedAtObject?.SendMessage("OnPointerExit");
                //lasthit = hit.transform.gameObject;
                //_gazedAtObject = lasthit;
                //_gazedAtObject.SendMessage("OnPointerEnter")
                //collision = hit.point;
            //Debug.Log("test");*/
        }
        else
        {
          // _gazedAtObject.SendMessage("OnPointerExit");
           _gazedAtObject = null;
           gvrTimer = 0;
           imgCircle.fillAmount = 0;
          // txt.text = "no";
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

    public void enableCanvas1()
    {
        myCanvas.enabled = true;
        ButtonTest1.enabled = false;
        ButtonTest2.enabled = false;
        NoButton1.enabled = true;
        ConfirmButton1.enabled = true;
    }
    
    public void disableCanvas1()
    {
        myCanvas.enabled = false;
        ButtonTest1.enabled = true;
        ButtonTest2.enabled = true;
        NoButton1.enabled = false;
        ConfirmButton1.enabled = false;
    }
}
