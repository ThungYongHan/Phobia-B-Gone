using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpiderGame : MonoBehaviour
{
    public GameObject[] spiderModels;
    public Transform spawnPoint, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5;
    private GameObject clone, clone2, clone3, clone4, clone5;

    private int selectedSpider;
    // to spawn starting batch of spiders
    void Start()
    {
        selectedSpider = PlayerPrefs.GetInt("selectedSpider");
        GameObject spiderModel = spiderModels[selectedSpider];
        clone = Instantiate(spiderModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone2 = Instantiate(spiderModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 90f, 0f));
        clone3 = Instantiate(spiderModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 270f, 0f));
        clone4 = Instantiate(spiderModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 270f, 0f));
        clone5 = Instantiate(spiderModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 45f, 0f));
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
        GameObject spiderModel = spiderModels[selectedSpider];
        clone = Instantiate(spiderModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, randSpawn1, 0f));
        clone2 = Instantiate(spiderModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, randSpawn2, 0f));
        clone3 = Instantiate(spiderModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, randSpawn3, 0f));
        clone4 = Instantiate(spiderModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, randSpawn4, 0f));
        clone5 = Instantiate(spiderModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, randSpawn5, 0f));
    }
}



//int randSpawn = Random.Range(1, 3);
//  switch(randSpawn)
// {
        //     case 1: 
        //         clone = Instantiate(spiderModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 310f, 0f));
        //         clone2 = Instantiate(spiderModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 110f, 0f));                
        //         clone3 = Instantiate(spiderModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 230f, 0f));
        //         clone4 = Instantiate(spiderModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 240f, 0f));                
        //         clone5 = Instantiate(spiderModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 0f, 0f));
        //         break;
        //     case 2:
        //         clone = Instantiate(spiderModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 270f, 0f));
        //         clone2 = Instantiate(spiderModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 90f, 0f));
        //         clone3 = Instantiate(spiderModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 270f, 0f));
        //         clone4 = Instantiate(spiderModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 270f, 0f));
        //         clone5 = Instantiate(spiderModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 45f, 0f));
        //     break;
        //     case 3:
        //         clone = Instantiate(spiderModel, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 290f, 0f));
        //         clone2 = Instantiate(spiderModel, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 110f, 0f));                
        //         clone3 = Instantiate(spiderModel, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 285f, 0f));
        //         clone4 = Instantiate(spiderModel, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler(0f, 240f, 0f));                
        //         clone5 = Instantiate(spiderModel, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler(0f, 15f, 0f));
        //     break;
        // }
