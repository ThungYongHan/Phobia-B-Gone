using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InsectGaze : MonoBehaviour
{
    public Canvas myCanvas1;
    public Canvas myCanvas2;
    public Canvas myCanvas3;

    public GameObject lasthit = null;
    //private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    public float gvrTimer;
    private GameObject parent;
    private Slider slider;
    // slider filling speed
    public float FillSpeed = 5f;
    private float targetProgress = 0;
    
    private BoxCollider OptionTest0 = null;
    private BoxCollider OptionTest1 = null;
    private BoxCollider OptionTest2 = null;
    private BoxCollider OptionTest3 = null;
    private BoxCollider OptionTest4 = null;

    private BoxCollider SelectButton = null;
    private BoxCollider CancelButton = null;
    private BoxCollider ConfirmButton = null;
    
    private TMPro.TextMeshProUGUI numTest;
    private TMPro.TextMeshProUGUI numTest2;
    private TextMeshPro testText;
    
    public GameObject test;
    public GameObject test2;
    public JSONReadandWrite scriptB;
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

    public bool sliderMax = false;

    private void Awake()
    {
        test = GameObject.Find("HiddenCanvas");
        test2 = GameObject.Find("HiddenCanvas");
        if (test != null)
        {
            // then reference the gameobject's script
            scriptB = test.GetComponent<JSONReadandWrite>();
        }
        scriptB.SaveToJson();

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
        SelectButton = GameObject.Find("SelectButton").GetComponent<BoxCollider>();

        CancelButton = GameObject.Find("CancelButton").GetComponent<BoxCollider>();
        ConfirmButton = GameObject.Find("ConfirmButton").GetComponent<BoxCollider>();
        // set HiddenCanvas Button's Collider to false at the start to prevent "ghost" box colliders
        OptionTest0.enabled = false;
        OptionTest1.enabled = false;
        OptionTest2.enabled = false;
        OptionTest3.enabled = false;
        OptionTest4.enabled = false;
        SelectButton.enabled = false;
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
        
        myCanvas1 = GameObject.Find("FormCanvas").GetComponent<Canvas>();
        myCanvas2 = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        myCanvas3 = GameObject.Find("ProgressCanvas").GetComponent<Canvas>();
        
        myCanvas1.enabled = false;
        myCanvas2.enabled = false;
        
        parent = GameObject.Find("ProgressCanvas");
        slider = parent.transform.GetChild(0).GetComponent<Slider>();
        // enable canvas in editor then disable canvas in awake in order to reference it
        //myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        //myCanvas.enabled = false;
    }
    
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            lasthit = hit.transform.gameObject;
            gvrTimer += Time.deltaTime;
            _gazedAtObject = lasthit;
            if (sliderMax == false)
            {
                //Debug.Log("checking");
                InsectGazing();
                DisableCanvas1();
            }
            else
            {
                // enable the hidden canvas
                myCanvas3.enabled = false;
                FormGazing();
                // this is crucial, need to set if(myCanvas1.enabled == false) so that enableCanvas1() is not called each frame, which previously
                // would have disabled "Confirm" and "Select" buttons in
                // myCanvas2 and also set all buttons in myCanvas 1 to be always active (interactable)
                if (myCanvas1.enabled == false)
                {
                    EnableCanvas1(); 
                }
            }
        }
        else
        {
            _gazedAtObject = null;
            gvrTimer = 0;
            imgCircle.fillAmount = 0;
        }
    }

    private void InsectGazing()
    {
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        // when the slider is fully filled
        if (slider.value == 1.0f)
        {
            sliderMax = true;
        }
        if (_gazedAtObject.name != "Plane" && _gazedAtObject.name != "table_2" && _gazedAtObject.name != "TableColliders")
        {
            IncrementProgress(1.0f);
            imgCircle.fillAmount = gvrTimer / totalTime;
            if (gvrTimer > totalTime)
            {
                Debug.Log("You are looking at the spider!");
            }
        }
        else
        {
            IncrementProgress(0.0f);
            _gazedAtObject = null;
            gvrTimer = 0;
            imgCircle.fillAmount = 0;
        }
    }

    private void FormGazing()
    {
        if (_gazedAtObject.name != "Plane" && _gazedAtObject.name != "table_2" &&
            _gazedAtObject.name != "TableColliders" && _gazedAtObject.name != "capsulespiderworking1" && _gazedAtObject.name != "capsulespiderworking2" && _gazedAtObject.name != "capsulespiderworking3" )
        {
            imgCircle.fillAmount = gvrTimer / totalTime;
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer > totalTime)
                {
                    if (_gazedAtObject.name == "ConfirmButton")
                    {
                        Debug.Log("confirm");
                        if (test != null)
                        {
                            // passing data to JSONReadandWrite
                            //PlayerPrefs.GetInt loads the PlayerPrefs data stored into the key
                            // if == 0 then it is the first item in the array (cartoon spider)
                            // if == 1 then it is the second item in the array (normal spider)
                            if (PlayerPrefs.GetInt("selectedSpider") == 0)
                            {
                                Debug.Log("testing0");
                                // realism input 1 for cartoon spider(array 0) because realism should start from 1
                                scriptB.Saving("testscenario", 1, chosenOption);
                            }
                            if (PlayerPrefs.GetInt("selectedSpider") == 1)
                            {
                                Debug.Log("testing1");
                                scriptB.Saving("testscenario", 2, chosenOption);
                            }
                            //Debug.Log("disable");
                            DisableCanvas2();
                        }
                        else
                        {
                            Debug.Log("alert");
                        }
                    }

                    if (_gazedAtObject.name == "CancelButton")
                    {
                        DisableCanvas2();
                    }

                    if (_gazedAtObject.name == "SelectButton")
                    {
                        Debug.Log("select");
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
                        }
                        EnableCanvas2();
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
                    }
                }
            }
        }
        else
        {
            _gazedAtObject = null;
            gvrTimer = 0;
            imgCircle.fillAmount = 0;
        }
    }

    private void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    private void EnableCanvas1()
    {
        myCanvas1.enabled = true;
        OptionTest0.enabled = true;
        OptionTest1.enabled = true;
        OptionTest2.enabled = true;
        OptionTest3.enabled = true;
        OptionTest4.enabled = true;
        SelectButton.enabled = true;
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
    }

    private void DisableCanvas1()
    {
        //bulkBoxColliderGet();
        myCanvas1.enabled = false;
        OptionTest0.enabled = false;
        OptionTest1.enabled = false;
        OptionTest2.enabled = false;
        OptionTest3.enabled = false;
        OptionTest4.enabled = false;
        SelectButton.enabled = false;
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
    }

    private void EnableCanvas2()
    {
        //bulkBoxColliderGet();
        myCanvas2.enabled = true;
        CancelButton.enabled = true;
        ConfirmButton.enabled = true;
        OptionTest0.enabled = false;
        OptionTest1.enabled = false;
        OptionTest2.enabled = false;
        OptionTest3.enabled = false;
        OptionTest4.enabled = false;
        SelectButton.enabled = false;
    }

    private void DisableCanvas2()
    {
        myCanvas2.enabled = false;
        CancelButton.enabled = false;
        ConfirmButton.enabled = false;
        OptionTest0.enabled = true;
        OptionTest1.enabled = true;
        OptionTest2.enabled = true;
        OptionTest3.enabled = true;
        OptionTest4.enabled = true;
        SelectButton.enabled = true;
    }
}
