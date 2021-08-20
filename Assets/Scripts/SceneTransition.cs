using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Load New Scene");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSpiderMenu()
    {
        SceneManager.LoadScene("sparecardboar");
    }
    
    public void LoadSelectionMenu()
    {
        SceneManager.LoadScene("selectphobia");
    }
}
