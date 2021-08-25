/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testingform : MonoBehaviour
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
    
    private BoxCollider OptionTest1 = null;
    private BoxCollider OptionTest2 = null;

    private TMPro.TextMeshProUGUI numTest;
    private TextMeshPro testText;
    
    
    private BoxCollider NoButton1 = null;
    private BoxCollider ConfirmButton1 = null;
    public GameObject test;
    public GameObject test2;
    public JSONReadandWrite scriptB;
    public GameObject testObject;

    private int chosenOption = 0;
    
    private Button theButton1;
    private ColorBlock theColor1;
    
    private Button theButton2;
    private ColorBlock theColor2;

    public TextAsset JsonFile;
    
    /*void Awake()
    {
        /*SaveObject saveObject = new SaveObject
        {
            evalChoice = 1,
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);

        SaveObject loadedSaveObject JsonUtility.FromJson<SaveObject>(json);#2#
    }#1#
    void Awake()
    {
        
        
       // PatientConnection pat = JsonUtility.FromJson<PatientConnection>(JsonFile.text);
       
        
        theButton1 = GameObject.Find("OptionTest1").GetComponent<Button>();
        theColor1 = theButton1.colors;
        
        theButton2 = GameObject.Find("OptionTest2").GetComponent<Button>();
        theColor2 = theButton2.colors;
        /*theColor.normalColor = Color.green;
        theButton.colors = theColor;#1#
        
        test2 = GameObject.Find("HiddenCanvas");
        
        
        // numTest = GameObject.Find("HiddenCanvas").GetComponent<Text>();
        // numTest.text = "no";
        // reference gameobject first
        test = GameObject.Find("HiddenCanvas");
        if (test != null)
        {
            // then reference the gameobject's script
            scriptB = test.GetComponent<JSONReadandWrite>();
        }
        scriptB.SaveToJson();
         // scriptB.LoadFromJson();

        if (GameObject.Find("ButtonTest1") != null)
        {
            ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
        }
        
        if (GameObject.Find("ButtonTest2") != null)
        {
            ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        }
        
        if (GameObject.Find("OptionTest1") != null)
        {
            OptionTest1 = GameObject.Find("OptionTest1").GetComponent<BoxCollider>();
        }
        
        if (GameObject.Find("OptionTest2") != null)
        {
            OptionTest2 = GameObject.Find("OptionTest2").GetComponent<BoxCollider>();
        }
        
//        ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
//        ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        NoButton1 = GameObject.Find("NoButton1").GetComponent<BoxCollider>();
        ConfirmButton1 = GameObject.Find("ConfirmButton1").GetComponent<BoxCollider>();
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders
        NoButton1.enabled = false;
        ConfirmButton1.enabled = false;
        if (testObject != null)
        {
            myCanvas = testObject.GetComponent<Canvas>();
            
            Debug.Log("testcanvas");
        }
        myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        myCanvas.enabled = false;
    }

    void Update()
    {
        /*if (test2 != null)
        {   
            Debug.Log(test2);
            // parent.transform.GetChild(0).GetComponent<Slider>();
            numTest = test2.transform.GetChild(2).GetComponent<Text>();
            Debug.Log(numTest);
//            numTest.text = "TestingCheck";
        }#1#
        
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
                        enableCanvas1();
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
                            // passing data to JSONReadandWrite
                            scriptB.Saving("testscenario", 2, 1);
                            disableCanvas1();
                            // scriptB.SaveToJson();
                        }
                        else
                        {
                            Debug.Log("alert");
                        }
                    }
                    if (_gazedAtObject.name == "NoButton1")
                    {
                        // scriptB.Saving("testscenario", 2, 1);
                        disableCanvas1();
                    }       
                    
                    if (_gazedAtObject.name == "ConfirmationButton")
                    {
                        
                        // get 4th child of hidden canvas (which is a TextMeshProUGUI component)
                        if (test2 != null)
                        {   
                            Debug.Log(test2);
                            numTest = test2.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                            Debug.Log(numTest);
                            // change the text of the TMP text ui object
                            // numTest.text = "Your Chosen Option is " + chosenOption.ToString();

                             scriptB.LoadFromJson();
                             numTest.text = scriptB.LoadFromJson();

                             
                            // numTest.("testing");
                            //testText = numTest.GetComponent<TextMeshPro>();
                            //Debug.Log(testText);
                            // testText.text = "Testing";
                            //Debug.Log(testText);
                            // numTest.text = "TestingCheck";
                        }
                        enableCanvas1();
                    }       
                    
                    if (_gazedAtObject.name == "OptionTest1")
                    {
                        theColor1.normalColor = Color.green;
                        theButton1.colors = theColor1;
                        
                        theColor2.normalColor = Color.white;
                        theButton2.colors = theColor2;

                        chosenOption = 1;
                        Debug.Log(chosenOption);

                        // disableCanvas1();
                    }     
                    
                    if (_gazedAtObject.name == "OptionTest2")
                    {
                        theColor2.normalColor = Color.green;
                        theButton2.colors = theColor2;
                        
                        theColor1.normalColor = Color.white;
                        theButton1.colors = theColor1;
                        
                        chosenOption = 2;
                        Debug.Log(chosenOption);
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
    
    public void enableCanvas1()
    {
        myCanvas.enabled = true;
//        ButtonTest1.enabled = false;
//        ButtonTest2.enabled = false;
        OptionTest1.enabled = false;
        OptionTest2.enabled = false;
        NoButton1.enabled = true;
        ConfirmButton1.enabled = true;
    }
    
    public void disableCanvas1()
    {
        myCanvas.enabled = false;
 //       ButtonTest1.enabled = true;
 //       ButtonTest2.enabled = true;
        NoButton1.enabled = false;
        ConfirmButton1.enabled = false;
        OptionTest1.enabled = true;
        OptionTest2.enabled = true;
    }

    /*private void SaveButton1()
    {
        
    }

    private class SaveObject
    {
        public int evalChoice;
    }#1#
}
*/
