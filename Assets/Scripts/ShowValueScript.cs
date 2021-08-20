using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueScript : MonoBehaviour
{
    private Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }

    public void textUpdate (float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
