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
    /*public UnityEvent GVRClick;
    public UnityEvent GVRClick2;#1#
    public Canvas myCanvas;
    
    /*private BoxCollider ButtonTest1 = null;
    private BoxCollider ButtonTest2 = null;#1#
    
    private BoxCollider OptionTest0 = null;
    private BoxCollider OptionTest1 = null;
    private BoxCollider OptionTest2 = null;
    private BoxCollider OptionTest3 = null;
    private BoxCollider OptionTest4 = null;

    private TMPro.TextMeshProUGUI numTest;
    private TMPro.TextMeshProUGUI numTest2;
    private TextMeshPro testText;
    
    
    private BoxCollider CancelButton = null;
    private BoxCollider ConfirmButton = null;
    public GameObject test;
    public GameObject test2;
    //public JSONReadandWrite scriptB;
    public GameObject testObject;

    private int chosenOption = 0;
    
    private Button theButton0;
    private ColorBlock theColor0;
    private Button theButton1;
    private ColorBlock theColor1;
    private Button theButton2;
    private ColorBlock theColor2;
    private Button theButton3;
    private ColorBlock theColor3;
    private Button theButton4;
    private ColorBlock theColor4;

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
        theButton0 = GameObject.Find("OptionTest0").GetComponent<Button>();
        theColor0 = theButton0.colors;
        
        theButton1 = GameObject.Find("OptionTest1").GetComponent<Button>();
        theColor1 = theButton1.colors;
        
        theButton2 = GameObject.Find("OptionTest2").GetComponent<Button>();
        theColor2 = theButton2.colors;
        
        theButton3 = GameObject.Find("OptionTest3").GetComponent<Button>();
        theColor3 = theButton3.colors;
        
        theButton4 = GameObject.Find("OptionTest4").GetComponent<Button>();
        theColor4 = theButton4.colors;

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

        /*if (GameObject.Find("ButtonTest1") != null)
        {
            ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
        }
        
        if (GameObject.Find("ButtonTest2") != null)
        {
            ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        }#1#
        
        if (GameObject.Find("OptionTest0") != null)
        {
            OptionTest0 = GameObject.Find("OptionTest0").GetComponent<BoxCollider>();
        }
        if (GameObject.Find("OptionTest1") != null)
        {
            OptionTest1 = GameObject.Find("OptionTest1").GetComponent<BoxCollider>();
        }
        if (GameObject.Find("OptionTest2") != null)
        {
            OptionTest2 = GameObject.Find("OptionTest2").GetComponent<BoxCollider>();
        }
        if (GameObject.Find("OptionTest3") != null)
        {
            OptionTest3 = GameObject.Find("OptionTest3").GetComponent<BoxCollider>();
        }
        if (GameObject.Find("OptionTest4") != null)
        {
            OptionTest4 = GameObject.Find("OptionTest4").GetComponent<BoxCollider>();
        }
        
//        ButtonTest1 = GameObject.Find("ButtonTest1").GetComponent<BoxCollider>();
//        ButtonTest2 = GameObject.Find("ButtonTest2").GetComponent<BoxCollider>();
        CancelButton = GameObject.Find("CancelButton").GetComponent<BoxCollider>();
        ConfirmButton = GameObject.Find("ConfirmButton").GetComponent<BoxCollider>();
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
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
                    /*if (_gazedAtObject.name == "ButtonTest1")
                    {
                        Debug.Log("hello");
                        enableCanvas1();
                    }#1#
                    if (_gazedAtObject.name == "ConfirmButton")
                    {
                        if (test != null)
                        {
                            //Debug.Log("please");
                            // passing data to JSONReadandWrite
                            scriptB.Saving("testscenario", 2, chosenOption);
                            disableCanvas1();
                            // scriptB.SaveToJson();
                        }
                        else
                        {
                            Debug.Log("alert");
                        }
                    }
                    if (_gazedAtObject.name == "CancelButton")
                    {
                        // scriptB.Saving("testscenario", 2, 1);
                        disableCanvas1();
                    }       
                    
                    if (_gazedAtObject.name == "SelectButton")
                    {
                        
                        // get 4th child of hidden canvas (which is a TextMeshProUGUI component)
                        if (test2 != null)
                        {   
                            Debug.Log(test2);
                            numTest = test2.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                            numTest2 = test2.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

                            Debug.Log(numTest);
                            // change the text of the TMP text ui object
                            // numTest.text = "Your Chosen Option is " + chosenOption.ToString();

                             scriptB.LoadFromJson();
                             numTest.text = scriptB.LoadFromJson();
                             numTest2.text = chosenOption.ToString();

                             
                            // numTest.("testing");
                            //testText = numTest.GetComponent<TextMeshPro>();
                            //Debug.Log(testText);
                            // testText.text = "Testing";
                            //Debug.Log(testText);
                            // numTest.text = "TestingCheck";
                        }
                        enableCanvas1();
                    }       
                    
                    if (_gazedAtObject.name == "OptionTest0")
                    {
                        theColor0.normalColor = Color.green;
                        theButton0.colors = theColor0;
                        theColor1.normalColor = Color.white;
                        theButton1.colors = theColor1;
                        theColor2.normalColor = Color.white;
                        theButton2.colors = theColor2;
                        theColor3.normalColor = Color.white;
                        theButton3.colors = theColor3;
                        theColor4.normalColor = Color.white;
                        theButton4.colors = theColor4;
                        chosenOption = 0;
                    } 
                    
                    if (_gazedAtObject.name == "OptionTest1")
                    {
                        theColor0.normalColor = Color.white;
                        theButton0.colors = theColor0;
                        theColor1.normalColor = Color.green;
                        theButton1.colors = theColor1;
                        theColor2.normalColor = Color.white;
                        theButton2.colors = theColor2;
                        theColor3.normalColor = Color.white;
                        theButton3.colors = theColor3;
                        theColor4.normalColor = Color.white;
                        theButton4.colors = theColor4;
                        chosenOption = 1;
                    }     
                    
                    if (_gazedAtObject.name == "OptionTest2")
                    {
                        theColor0.normalColor = Color.white;
                        theButton0.colors = theColor0;
                        theColor1.normalColor = Color.white;
                        theButton1.colors = theColor1;
                        theColor2.normalColor = Color.green;
                        theButton2.colors = theColor2;
                        theColor3.normalColor = Color.white;
                        theButton3.colors = theColor3;
                        theColor4.normalColor = Color.white;
                        theButton4.colors = theColor4;
                        chosenOption = 2;
                    }  
                    
                    if (_gazedAtObject.name == "OptionTest3")
                    {
                        theColor0.normalColor = Color.white;
                        theButton0.colors = theColor0;
                        theColor1.normalColor = Color.white;
                        theButton1.colors = theColor1;
                        theColor2.normalColor = Color.white;
                        theButton2.colors = theColor2;
                        theColor3.normalColor = Color.green;
                        theButton3.colors = theColor3;
                        theColor4.normalColor = Color.white;
                        theButton4.colors = theColor4;
                        chosenOption = 3;
                    } 
                    
                    if (_gazedAtObject.name == "OptionTest4")
                    {
                        theColor0.normalColor = Color.white;
                        theButton0.colors = theColor0;
                        theColor1.normalColor = Color.white;
                        theButton1.colors = theColor1;
                        theColor2.normalColor = Color.white;
                        theButton2.colors = theColor2;
                        theColor3.normalColor = Color.white;
                        theButton3.colors = theColor3;
                        theColor4.normalColor = Color.green;
                        theButton4.colors = theColor4;
                        chosenOption = 4;
                        //Debug.Log(chosenOption);
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
        OptionTest0.enabled = false;
        OptionTest1.enabled = false;
        OptionTest2.enabled = false;
        OptionTest3.enabled = false;
        OptionTest4.enabled = false;
        CancelButton.enabled = true;
        ConfirmButton.enabled = true;
    }
    
    public void disableCanvas1()
    {
        myCanvas.enabled = false;
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
        OptionTest0.enabled = true;
        OptionTest1.enabled = true;
        OptionTest2.enabled = true;
        OptionTest3.enabled = true;
        OptionTest4.enabled = true;
    }
}
*/
