using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowSliderValue : MonoBehaviour
{
    private TextMeshProUGUI sliderNum;
    // Start is called before the first frame update
    void Start()
    {
        sliderNum = GetComponent<TextMeshProUGUI>();
        sliderNum.text = "testing";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
