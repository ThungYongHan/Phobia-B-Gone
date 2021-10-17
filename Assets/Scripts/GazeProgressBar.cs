// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class GazeProgressBar : MonoBehaviour
// {
//     private GameObject parent;
//     private Slider slider;
//     public float FillSpeed = 0.05f;
//     private float targetProgress = 0;
//     // Start is called before the first frame update
//     void Awake()
//     {
//         parent = GameObject.Find("ProgressCanvas");
//         slider = parent.transform.GetChild(0).GetComponent<Slider>();
//     }   
//     void Start()
//     {
//         IncrementProgress(1.0f);
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (slider.value < targetProgress)
//         {
//             slider.value += FillSpeed * Time.deltaTime;
//         }
//     }
//
//     public void IncrementProgress(float newProgress)
//     {
//         targetProgress = slider.value + newProgress;
//     }
// }
