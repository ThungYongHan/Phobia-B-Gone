using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderSelection : MonoBehaviour
{
    // create a gameObject array named spiders
    public GameObject[] spiders;
    // selected spider model index number (start from 0 as cartoon)
    public int selectedSpider;
    
    // user gazes at next spider arrow button
    public void NextSpider()
    {
        // set the spider model of selectedSpider index to inactive
        spiders[selectedSpider].SetActive(false);
        // set selectedSpider index number to the next index number in
        // the array and start again from 0 if trying to execute NextSpider()
        // when the current element is the last element (eg: from 3 to 0)
        selectedSpider = (selectedSpider + 1) % spiders.Length;
        spiders[selectedSpider].SetActive(true);
    }

    // user gazes at previous spider arrow button
    public void PreviousSpider()
    {
        // set the spider model of selectedSpider index to inactive
        spiders[selectedSpider].SetActive(false);
        // decrease the selectedSpider index number by 1
        selectedSpider--;
        if (selectedSpider < 0)
        {
            // set the selectedSpider index number to be equal to the length of the spiders array
            // (eg: from 0 to 3)
            selectedSpider += spiders.Length;
        }
        // set the spider model of the current index to active
        spiders[selectedSpider].SetActive(true);
    }
    
    // user starts the gaze or gamified exposure tasks
    public void SetSpider()
    {
        // set selectedSpider index number as the value for the "selectedSpider" key for PlayerPrefs
        PlayerPrefs.SetInt("selectedSpider", selectedSpider);
    }
}
