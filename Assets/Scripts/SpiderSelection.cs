using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderSelection : MonoBehaviour
{
    public GameObject[] spiders;
    public int selectedSpider = 0;
    
    public void NextSpider()
    {
        spiders[selectedSpider].SetActive(false);
        selectedSpider = (selectedSpider + 1) % spiders.Length;
        spiders[selectedSpider].SetActive(true);
    }

    public void PreviousSpider()
    {
        spiders[selectedSpider].SetActive(false);
        selectedSpider--;
        if (selectedSpider < 0)
        {
            selectedSpider += spiders.Length;
        }
        spiders[selectedSpider].SetActive(true);
    }
    
    public void SetSpider()
    {
        PlayerPrefs.SetInt("selectedSpider", selectedSpider);
    }
}
