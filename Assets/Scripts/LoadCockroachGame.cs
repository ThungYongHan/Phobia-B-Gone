using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCockroachGame : MonoBehaviour
{
    public GameObject[] cockroachPrefabs;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public GameObject clone, clone2, clone3, clone4, clone5;

    private int selectedCockroach;
    void Start()
    {
        selectedCockroach = PlayerPrefs.GetInt("selectedCockroach");

        GameObject prefab = cockroachPrefabs[selectedCockroach];
        clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 90f, 0f));
        clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 270f, 0f));
        clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 45f, 0f));
    }
    
    public void spawnRandomLocation()
    {
        int randSpawn = Random.Range(1, 3);
        GameObject prefab = cockroachPrefabs[selectedCockroach];
        switch(randSpawn)
        {
            case 1: 
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 310f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 110f, 0f));                
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 230f, 0f));
                clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 240f, 0f));                
                clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 0f, 0f));
                break;
            case 2:
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 270f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 90f, 0f));
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 270f, 0f));
                clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 270f, 0f));
                clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 45f, 0f));
            break;
            case 3:
                clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 290f, 0f));
                clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 110f, 0f));                
                clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 285f, 0f));
                clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 240f, 0f));                
                clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 15f, 0f));
            break;
        }
    }
}
