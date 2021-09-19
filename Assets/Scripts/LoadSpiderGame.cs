using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpiderGame : MonoBehaviour
{
    public GameObject[] spiderPrefabs;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public GameObject clone, clone2, clone3, clone4, clone5;
    public CapsuleCollider exampleCollider;
    private int selectedGazeSize;
    private int selectedGazeNum;
    private int selectedSpider;
    void Start()
    {
        selectedSpider = PlayerPrefs.GetInt("selectedSpider");
        selectedGazeNum = PlayerPrefs.GetInt("selectedGazeNum");
        selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        
        Debug.Log(selectedGazeSize);
        Debug.Log(selectedGazeNum);

        GameObject prefab = spiderPrefabs[selectedSpider];
        clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
        /*
        clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
        clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
        clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
        clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
        */
        
        /*if (selectedGazeNum == 1)
        {
            // clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            // to make the spider spawn and walk towards the player (post rotation)
            clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
            exampleFunction(clone);
        }
        if (selectedGazeNum == 3)
        {
             /*clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
             clone2 = Instantiate(prefab, spawnPoint2.position, Quaternion.identity);
             clone3 = Instantiate(prefab, spawnPoint3.position, Quaternion.identity);#1#
             clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
             exampleFunction(clone);
             exampleFunction(clone2);
             exampleFunction(clone3);
        }
        if (selectedGazeNum == 5)
        {
             /*clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
             clone2 = Instantiate(prefab, spawnPoint2.position, Quaternion.identity);
             clone3 = Instantiate(prefab, spawnPoint3.position, Quaternion.identity);
             clone4 = Instantiate(prefab, spawnPoint4.position, Quaternion.identity);
             clone5 = Instantiate(prefab, spawnPoint5.position, Quaternion.identity);#1#
             clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
             exampleFunction(clone);
             exampleFunction(clone2);
             exampleFunction(clone3);
             exampleFunction(clone4);
             exampleFunction(clone5);
        }*/
        
        //GameObject clone2 = Instantiate(prefab, spawnPoint2.position, Quaternion.identity);
        /*GameObject clone3 = Instantiate(prefab, spawnPoint3.position, Quaternion.identity);
        GameObject clone4 = Instantiate(prefab, spawnPoint4.position, Quaternion.identity);
        GameObject clone5 = Instantiate(prefab, spawnPoint5.position, Quaternion.identity);*/
        
        // using function and parameter because just calling clone with end up with only the last clone recieving the changes
        /*exampleFunction(clone);
        exampleFunction(clone2);
        exampleFunction(clone3);
        exampleFunction(clone4);
        exampleFunction(clone5);*/
        /*int selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        if (selectedGazeSize == 1)
        {
            // change clone gameobject scale using code (using multiply as different assets have different scales)
            Vector3 newScale = clone.transform.localScale;
            newScale *= 1f;
            clone.transform.localScale = newScale;
            //Debug.Log(clone.transform.localScale);
            // clone.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        if (selectedGazeSize == 2)
        {
            // clone.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            Vector3 newScale = clone.transform.localScale;
            newScale *= 2f;
            clone.transform.localScale = newScale;
        }
        if (selectedGazeSize == 3)
        {
            // clone.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            Vector3 newScale = clone.transform.localScale;
            newScale *= 3f;
            clone.transform.localScale = newScale;
        }*/
    }
    
    public void exampleFunction(GameObject spiderExample)
    {
        // int selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        //selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        if (selectedGazeSize == 1)
        {
            // change clone gameobject scale using code (using multiply as different assets have different scales)
            Vector3 newScale = spiderExample.transform.localScale;
            newScale *= 1f;
            spiderExample.transform.localScale = newScale;
            // need to call capsule collider and individualise the center as different scales will have different heights (prevent it from floating on the table)
            /*exampleCollider = spiderExample.GetComponent<CapsuleCollider>();
            exampleCollider.center = new Vector3(0f, 2f, 0f);
            Debug.Log(exampleCollider);*/
            //Debug.Log(clone.transform.localScale);
            // clone.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        if (selectedGazeSize == 2)
        {
            // clone.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            Vector3 newScale = spiderExample.transform.localScale;
            newScale *= 1.5f;
            spiderExample.transform.localScale = newScale;
        }
        if (selectedGazeSize == 3)
        {
            // clone.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            Vector3 newScale = spiderExample.transform.localScale;
            newScale *= 3f;
            spiderExample.transform.localScale = newScale;
            
        }
    }
    /*public void example(GameObject spider1)
    {
        int selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        if (selectedGazeSize == 1)
        {
            // change clone gameobject scale using code (using multiply as different assets have different scales)
            Vector3 newScale = clone.transform.localScale;
            newScale *= 1f;
            clone.transform.localScale = newScale;
            //Debug.Log(clone.transform.localScale);
            // clone.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        if (selectedGazeSize == 2)
        {
            // clone.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            Vector3 newScale = clone.transform.localScale;
            newScale *= 2f;
            clone.transform.localScale = newScale;
        }
        if (selectedGazeSize == 3)
        {
            // clone.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            Vector3 newScale = clone.transform.localScale;
            newScale *= 3f;
            clone.transform.localScale = newScale;
        }
    }*/
}
