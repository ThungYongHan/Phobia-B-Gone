using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InsectGaze : MonoBehaviour
{
   // private Text txt;
    public GameObject lasthit = null;
    //private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gvrStatus;
    public float gvrTimer;
    public UnityEvent GVRClick;
    public UnityEvent GVRClick2;

    void Start()
    {
        //txt = GameObject.Find("Text").GetComponent<Text>();
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
            // imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            imgCircle.fillAmount = gvrTimer / totalTime;
            /*if (_gazedAtObject == lasthit)
            {*/
                /*if (gvrTimer > totalTime)
                {*/
                    if (_gazedAtObject.name != "Plane" && _gazedAtObject.name != "table_2" && _gazedAtObject.name != "TableColliders")
                    {
                        if (gvrTimer > totalTime)
                        {
                        Debug.Log("You are looking at the spider!");
                        }
                    }
                    else
                    {
                        _gazedAtObject = null;
                        gvrTimer = 0;
                        imgCircle.fillAmount = 0;
                    }
              
            //}
            /*else
            {
                imgCircle.fillAmount = 0;
            }*/
        }
        else
        {
           _gazedAtObject = null;
           gvrTimer = 0;
           imgCircle.fillAmount = 0;
        }
    }
}
