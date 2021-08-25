/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InsectGaze : MonoBehaviour
{
    public Canvas myCanvas = null;

    public GameObject lasthit = null;
    //private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    // bool gvrStatus;
    public float gvrTimer;
    /*
    public UnityEvent GVRClick;
    public UnityEvent GVRClick2;
    #1#
    private GameObject parent;
    private Slider slider;
    public float FillSpeed = 0.05f;
    private float targetProgress = 0;

    void Awake()
    {
        parent = GameObject.Find("ProgressCanvas");
        slider = parent.transform.GetChild(0).GetComponent<Slider>();
        // enable canvas in editor then disable canvas in awake in order to reference it
        //myCanvas = GameObject.Find("HiddenCanvas").GetComponent<Canvas>();
        //myCanvas.enabled = false;
    }
    void Start()
    {
        //IncrementProgress(1.0f);
    }
    
    void Update()
    { 
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            lasthit = hit.transform.gameObject;
            // imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            gvrTimer += Time.deltaTime;

            /*if (_gazedAtObject == lasthit)
            {#1#
                /*if (gvrTimer > totalTime)
                {#1#
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
                    /*else
                    {
                        imgCircle.fillAmount = 0;
                    }#1#
        }
        else
        {
           _gazedAtObject = null;
           gvrTimer = 0;
           imgCircle.fillAmount = 0;
        }
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        // when the slider is fully filled
        if (slider.value == 1.0f)
        {
            // enable the hidden canvas 
            enableHiddenCanvas();
        }
    }
    
    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
    
    public void enableHiddenCanvas()
    {
        myCanvas.enabled = true;
    }
}
*/
