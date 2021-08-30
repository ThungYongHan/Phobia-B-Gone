using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarningSplashScript : MonoBehaviour
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
    public GameObject test;
    private BoxCollider UnderstoodButton = null;
    void Start()
    {
        UnderstoodButton = GameObject.Find("UnderstoodButton").GetComponent<BoxCollider>();
        // then reference the gameobject's script
    }

    void FixedUpdate()
    {
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
                    if (_gazedAtObject.name == "UnderstoodButton")
                    {
                        SceneManager.LoadScene("selectphobia");
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
            //PreviousSpider.enabled = true;
        }
    }
}
