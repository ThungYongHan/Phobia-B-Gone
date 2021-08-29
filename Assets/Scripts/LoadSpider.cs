using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpider : MonoBehaviour
{
    public GameObject[] spiderPrefabs;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        int selectedSpider = PlayerPrefs.GetInt("selectedSpider");
        GameObject prefab = spiderPrefabs[selectedSpider];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
    
}
