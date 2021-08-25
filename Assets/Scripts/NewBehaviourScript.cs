/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehav : MonoBehaviour
{
   // private Text txt;
    public GameObject lasthit = null;
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
    public GameObject test;
    public JSONReadandWrite scriptB;
    void Awake()
    {
        /*SaveObject saveObject = new SaveObject
        {
            evalChoice = 1,
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);

        SaveObject loadedSaveObject JsonUtility.FromJson<SaveObject>(json);#1#
    }
    void Start()
    {
        // reference gameobject first
        test = GameObject.Find("HiddenCanvas");
        if (test != null)
        {
            // then reference the gameobject's script
            scriptB = test.GetComponent<JSONReadandWrite>();
        }
        //txt = GameObject.Find("Text").GetComponent<Text>();
        ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
        ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        /*NoButton1 = GameObject.Find("NoButton1").GetComponent<BoxCollider>();
        ConfirmButton1 = GameObject.Find("ConfirmButton1").GetComponent<BoxCollider>();
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders
        NoButton1.enabled = false;
        ConfirmButton1.enabled = false;#1#
        /*if (testObject != null)
        {
            myCanvas = testObject.GetComponent<Canvas>();
            
            Debug.Log("testcanvas");
        }#1#
        /*myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        myCanvas.enabled = false;#1#
        
    }
    
    
    // Update is called once per frame
    void Update()
    { 
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
                    if (_gazedAtObject.name == "ButtonTest1")
                    {
                        Debug.Log("hello");
                       // enableCanvas1();
                        /*if (_gazedAtObject.name == "ConfirmButton1")
                        {

                            Debug.Log("please");
                            if (test != null)
                            {
                                scriptB.SaveToJson();
                            }
                            else
                            {
                                Debug.Log("alert");
                            }
                        }#1#
                        /*if (testObject != null)
                        {
                            myCanvas = testObject.GetComponent<Canvas>();
                            myCanvas.enabled = true;
                            Debug.Log("testcanvas");
                        }#1#
                    }
                    if (_gazedAtObject.name == "ConfirmButton1")
                    {
                        if (test != null)
                        {
                            Debug.Log("please");
                            scriptB.SaveToJson();
                        }
                        else
                        {
                            Debug.Log("alert");
                        }
                    }
                    if (_gazedAtObject.name == "NoButton1")
                    {
                       // disableCanvas1();

                    }                
                    /*if (_gazedAtObject.name == "ButtonTest2")
                    {
                        GVRClick2.Invoke();
                    }#1#
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
        }
    }
    
    /*public void enableCanvas1()
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
    }#1#

    private void SaveButton1()
    {
        
    }

    private class SaveObject
    {
        public int evalChoice;
    }
}
*/
