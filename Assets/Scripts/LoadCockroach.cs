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
    public int selectedGazeSize;
    public int selectedGazeNum;
    public int selectedCockroach;
    void Start()
    {
        selectedCockroach = PlayerPrefs.GetInt("selectedCockroach");
        selectedGazeNum = PlayerPrefs.GetInt("cockroachselectedGazeNum");
        selectedGazeSize = PlayerPrefs.GetInt("cockroachselectedGazeSize");
        
        /*Debug.Log(selectedGazeSize);
        Debug.Log(selectedGazeNum);*/

        GameObject prefab = cockroachPrefabs[selectedCockroach];
        // 1 cockroach
        if (selectedGazeNum == 1)
        {   
            // size s
            if (selectedGazeSize == 1)
            {
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                selectCockroachSizeFunction(clone);
            }
            
            // size m
            if (selectedGazeSize == 2)
            {
                // cartoon cockroach (1 - m)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.98f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.98f, 0f);
                }
                // realistic cockroach (1 - m)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.033f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.033f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {
                // cartoon cockroach (1 - l)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.09f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.09f, 0f);
                }

                // realistic cockroach (1 - l)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0365f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0365f, 0f);
                }
            }
            
        }
        // 3 cockroaches
        if (selectedGazeNum == 3)
        {
            // size s
            if (selectedGazeSize == 1)
            {
                // cartoon cockroach (3 - s)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.739f, 0f);

                }
                // realistic cockroach (3 - s)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0256f, 0f);
                }
            }
            
            // size m
            if (selectedGazeSize == 2)
            {
                // cartoon cockroach (3 - m)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.86f, 0f);
                }
                // realistic cockroach (3 - m)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.03f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {
                // cartoon cockroach (3 - l)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.02f, 0f);
                }
                // realistic cockroach (3 - l)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.035f, 0f);
                }
            }
            
        }
        // 5 cockroaches 
        if (selectedGazeNum == 5)
        {
            // size s 
            if (selectedGazeSize == 1)
            {
                // cartoon cockroach (5 - s)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone4.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                    clone5.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
                }
                
                // realistic cockroach (5 - s)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone4.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                    clone5.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.02f, 0f);
                }
            }
            
            // size m
            if (selectedGazeSize == 2)
            {   
                
                // cartoon cockroach (5 - m)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone4.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                    clone5.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.725f, 0f);
                }
                
                // realistic cockroach (5 - m)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone4.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                    clone5.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.026f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {   
                // cartoon cockroach (5 - l)
                if (selectedCockroach == 0)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone2.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone3.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone4.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                    clone5.transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.95f, 0f);
                }
                
                // realistic cockroach (5 - l)
                if (selectedCockroach == 1)
                {
                    clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                    selectCockroachSizeFunction(clone);
                    selectCockroachSizeFunction(clone2);
                    selectCockroachSizeFunction(clone3);
                    selectCockroachSizeFunction(clone4);
                    selectCockroachSizeFunction(clone5);
                    clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone2.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone3.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone4.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                    clone5.transform.GetChild(4).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.0325f, 0f);
                }
            }
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
