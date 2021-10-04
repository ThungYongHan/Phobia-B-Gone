using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowSliderValue : MonoBehaviour
{
    public TextMeshProUGUI sliderNum;
    public Slider SliderUI;
    // Start is called before the first frame update
    void Start()
    {
        sliderNum = GetComponent<TextMeshProUGUI>();
        sliderNum.text = "0";
    }

    public void SliderValueUpdate()
    {
        // Debug.Log(SliderUI.value);
        string sliderMessage = SliderUI.value.ToString();
        sliderNum.text = sliderMessage;
    }
}
