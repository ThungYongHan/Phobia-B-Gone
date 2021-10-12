using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CockroachSelection : MonoBehaviour
{
    public GameObject[] cockroaches;
    public int selectedCockroach = 0;
    
    // Start is called before the first frame update
    public void NextCockroach()
    {
        cockroaches[selectedCockroach].SetActive(false);
        selectedCockroach = (selectedCockroach + 1) % cockroaches.Length;
        cockroaches[selectedCockroach].SetActive(true);
    }

    public void PreviousCockroach()
    {
        cockroaches[selectedCockroach].SetActive(false);
        selectedCockroach--;
        if (selectedCockroach < 0)
        {
            selectedCockroach += cockroaches.Length;
        }
        cockroaches[selectedCockroach].SetActive(true);
    }
    
    public void SetCockroach()
    {
        PlayerPrefs.SetInt("selectedCockroach", selectedCockroach);
    }
}