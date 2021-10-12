using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCockroach : MonoBehaviour
{
    public GameObject[] cockroachPrefabs;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public GameObject clone, clone2, clone3, clone4, clone5;
    public CapsuleCollider exampleCollider;
    private int selectedGazeSize;
    private int selectedGazeNum;
    private int selectedCockroach;
    void Start()
    {
        selectedCockroach = PlayerPrefs.GetInt("selectedCockroach");
        selectedGazeNum = PlayerPrefs.GetInt("selectedGazeNum");
        selectedGazeSize = PlayerPrefs.GetInt("selectedGazeSize");
        
        /*Debug.Log(selectedGazeSize);
        Debug.Log(selectedGazeNum);*/

        GameObject prefab = cockroachPrefabs[selectedCockroach];
        if (selectedGazeNum == 1)
        {
            clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
            selectCockroachSizeFunction(clone);
        }
        if (selectedGazeNum == 3)
        {

            if (selectedCockroach == 0)
            {
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                selectCockroachSizeFunction(clone);
                selectCockroachSizeFunction(clone2);
                selectCockroachSizeFunction(clone3);
                clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.21f, 0f);
                clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.21f, 0f);
                clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.21f, 0f);
            }
            
            if (selectedCockroach == 1)
            {
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                selectCockroachSizeFunction(clone);
                selectCockroachSizeFunction(clone2);
                selectCockroachSizeFunction(clone3);
                clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
            }
            
            if (selectedCockroach == 2)
            {
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                selectCockroachSizeFunction(clone);
                selectCockroachSizeFunction(clone2);
                selectCockroachSizeFunction(clone3);
                clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
            }
        }
        if (selectedGazeNum == 5)
        {
            clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
             clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
             selectCockroachSizeFunction(clone);
             selectCockroachSizeFunction(clone2);
             selectCockroachSizeFunction(clone3);
             selectCockroachSizeFunction(clone4);
             selectCockroachSizeFunction(clone5);
        }
    }
    
    public void selectCockroachSizeFunction(GameObject cockroachExample)
    {
        if (selectedGazeSize == 1)
        {
            Vector3 newScale = cockroachExample.transform.localScale;
            newScale *= 1f;
            cockroachExample.transform.localScale = newScale;
        }
        
        if (selectedGazeSize == 2)
        {
            Vector3 newScale = cockroachExample.transform.localScale;
            newScale *= 1.5f;
            cockroachExample.transform.localScale = newScale;
        }
        
        if (selectedGazeSize == 3)
        {
            Vector3 newScale = cockroachExample.transform.localScale;
            newScale *= 3f;
            cockroachExample.transform.localScale = newScale;
            
        }
    }
}
