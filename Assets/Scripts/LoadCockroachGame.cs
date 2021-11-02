using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCockroachGame : MonoBehaviour
{
    public GameObject[] cockroachModels;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public GameObject clone, clone2, clone3, clone4, clone5;

    private int selectedCockroach;
    // to spawn starting batch of spiders
    void Start()
    {
        selectedCockroach = PlayerPrefs.GetInt("selectedCockroach");
        GameObject cockroachModel = cockroachModels[selectedCockroach];
        clone = Instantiate(cockroachModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone2 = Instantiate(cockroachModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 90f, 0f));
        clone3 = Instantiate(cockroachModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone4 = Instantiate(cockroachModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 270f, 0f));
        clone5 = Instantiate(cockroachModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 45f, 0f));
    }
    
    // called when user caught specific number of spiders to spawn more spiders with random y-axis values
    public void spawnRandomLocation()
    {
        // randomize the y-axis rotation values for spider instantiation
        float randSpawn1 = Random.Range(270f, 310f);
        float randSpawn2 = Random.Range(90f, 110f);
        float randSpawn3 = Random.Range(230f, 285f);
        float randSpawn4 = Random.Range(240f, 270f);
        float randSpawn5 = Random.Range(0f, 45f);
        GameObject cockroachModel = cockroachModels[selectedCockroach];
        clone = Instantiate(cockroachModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, randSpawn1, 0f));
        clone2 = Instantiate(cockroachModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, randSpawn2, 0f));
        clone3 = Instantiate(cockroachModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, randSpawn3, 0f));
        clone4 = Instantiate(cockroachModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, randSpawn4, 0f));
        clone5 = Instantiate(cockroachModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, randSpawn5, 0f));
    }
}
