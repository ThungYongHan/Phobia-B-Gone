using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Load New Scene");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
