using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testcanvas : MonoBehaviour
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
            imgCircle.fillAmount = gvrTimer / totalTime;
            _gazedAtObject = lasthit;
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer > totalTime)
                {
                    Debug.Log("hello");
                    if (_gazedAtObject.name == "TestSpiderButton")
                    { 
                        GVRClick.Invoke();
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
}
