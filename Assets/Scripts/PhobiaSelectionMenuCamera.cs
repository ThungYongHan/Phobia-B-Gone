using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhobiaSelectionMenuCamera : MonoBehaviour
{
    public GameObject lasthit = null;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public Image imgCircle;
    public float totalTime = 2;
    private bool gvrStatus;
    public float gvrTimer;
   // public UnityEvent GVRSpider;
    
    void Start()
    {
    }

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
            // ensure that whenever the reticle is moved away from the collider, the imgCircle reflects it
            if (_gazedAtObject == lasthit)
            {
                if (gvrTimer > totalTime)
                {
                    Debug.Log("hello");
                    if (_gazedAtObject.name == "SpiderButton")
                    {
                        SceneManager.LoadScene("sparecardboar");
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
        }
    }

}
