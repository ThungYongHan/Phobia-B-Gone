using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// public Transform spawnPoint2;
// public Transform spawnPoint3;
// public Transform spawnPoint4;
// public Transform spawnPoint5;
// public int selectedGazeNum;
// public int selectedSpider;

public class LoadSpider : MonoBehaviour
{
    // create a gameObject array named spiderPrefabs
    public GameObject[] spiderPrefabs;
    // spawn points used to determine where to instantiate the spider clones
    public Transform spawnPoint, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5;
    // instantiated spider clones
    private GameObject _clone, _clone2, _clone3, _clone4, _clone5;
    // selected spider model, spider size, and spider number in the menu scene
    public int selectedSpider, selectedGazeSize, selectedGazeNum;
    
    void Awake()
    {
        // get PlayerPrefs value for selected spider model key
        selectedSpider = PlayerPrefs.GetInt("selectedSpider");
        // get PlayerPrefs value for selected spider number key
        selectedGazeNum = PlayerPrefs.GetInt("spiderselectedGazeNum");
        // get PlayerPrefs value for selected spider size key
        selectedGazeSize = PlayerPrefs.GetInt("spiderselectedGazeSize");
        // store reference to selected spider model prefab
        GameObject prefab = spiderPrefabs[selectedSpider];
        
        // 1 spider
        if (selectedGazeNum == 1)
        {
            // size s
            if (selectedGazeSize == 1)  
            {   
                // instantiate selected spider model prefab at spawn point 1 with 180-degree rotation at the y axis
                _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                // set instantiated spider model prefab to selected size
                SelectSpiderSizeFunction(_clone);
            }
            
            // size m
            if (selectedGazeSize == 2)
            {
                // cartoon spider (1 - m)
                if (selectedSpider == 0)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.19f, 0f);
                    _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.19f, 0f);
                }
                // normal spider (1 - m)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.5f, 0f);
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.5f, 0f);
                }
                // realistic spider (1 - m)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.27f, 0f);
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.27f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {
                // cartoon spider (1 - l)
                if (selectedSpider == 0)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.21f, 0f);
                    _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.21f, 0f);
                }
                // normal spider (1 - l)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.7f, 0f);
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.7f, 0f);
                }
                // realistic spider (1 - l)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0f, 180f, 0f));
                    SelectSpiderSizeFunction(_clone);
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.29f, 0f);
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.29f, 0f);
                }
            }
        }
        
        //3 spiders
        if (selectedGazeNum == 3)
        {
            //size s
            if (selectedGazeSize == 1)
            {
                // cartoon spider (3 - s)
                if (selectedSpider == 0)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                
                    _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                    _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                    _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.145f, 0f);
                }
                // normal spider (3 - s)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.2f, 0f);
                }
                // realistic spider (3 - s)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
                }
            }
            
            // size m
            if (selectedGazeSize == 2)
            {
                // cartoon spider (3 - m)
                if (selectedSpider == 0)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                
                    _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                    _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                    _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1699f, 0f);
                }
                // normal spider (3 - m)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.3f, 0f);
                }
                // realistic spider (3 - m)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    // set center of capsule collider for instantiated spider prefabs 
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                    
                    // set center of collision blocker for instantiated spider prefabs
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.2f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {
                // cartoon spider (3 - l)
                if (selectedSpider == 0)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                
                    _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                    _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                    _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.2f, 0f);
                }
                // normal spider (3 - l)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.55f, 0f);
                }
                // realistic spider (3 - l)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.28f, 0f);
                }
            }
        }
        
        //5 spiders
        if (selectedGazeNum == 5)
        {
            // size s 
            if (selectedGazeSize == 1)
            {
                // cartoon spider (5 - s)
                if (selectedSpider == 0)
               {
                   _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                   _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                   SelectSpiderSizeFunction(_clone);
                   SelectSpiderSizeFunction(_clone2);
                   SelectSpiderSizeFunction(_clone3);
                   SelectSpiderSizeFunction(_clone4);
                   SelectSpiderSizeFunction(_clone5);
                
                   _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                
                   _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone4.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
                   _clone5.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.1099f, 0f);
               } 
                // normal spider (5 - s)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.8f, 0f);
                }
                // realistic spider (5 - s)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.9f, 0f);
                }
            }
            
            // size m
            if (selectedGazeSize == 2)
            {
                // cartoon spider (5 - m)
                if (selectedSpider == 0)
               {
                   _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                   _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                   SelectSpiderSizeFunction(_clone);
                   SelectSpiderSizeFunction(_clone2);
                   SelectSpiderSizeFunction(_clone3);
                   SelectSpiderSizeFunction(_clone4);
                   SelectSpiderSizeFunction(_clone5);
                
                   _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                
                   _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone4.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
                   _clone5.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.15f, 0f);
               } 
                // normal spider (5 - m)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.15f, 0f);
                }
                // realistic spider (5 - m)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.1f, 0f);
                }
            }
            
            // size l
            if (selectedGazeSize == 3)
            {
                // cartoon spider (5 - l)
                if (selectedSpider == 0)
               {
                   _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                   //clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                   _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                   SelectSpiderSizeFunction(_clone);
                   SelectSpiderSizeFunction(_clone2);
                   SelectSpiderSizeFunction(_clone3);
                   SelectSpiderSizeFunction(_clone4);
                   SelectSpiderSizeFunction(_clone5);
                
                   _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                
                   _clone.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone2.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone3.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone4.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
                   _clone5.transform.GetChild(3).GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.18f, 0f);
               } 
                // normal spider (5 - l)
                if (selectedSpider == 1)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));

                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 2.4f, 0f);
                }
                // realistic spider (5 - l)
                if (selectedSpider == 2)
                {
                    _clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone2 = Instantiate(prefab, spawnPoint2.position, spawnPoint2.rotation * Quaternion.Euler (0f, 270f, 0f));
                    _clone3 = Instantiate(prefab, spawnPoint3.position, spawnPoint3.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone4 = Instantiate(prefab, spawnPoint4.position, spawnPoint4.rotation * Quaternion.Euler (0f, 180f, 0f));
                    _clone5 = Instantiate(prefab, spawnPoint5.position, spawnPoint5.rotation * Quaternion.Euler (0f, 180f, 0f));
                
                    SelectSpiderSizeFunction(_clone);
                    SelectSpiderSizeFunction(_clone2);
                    SelectSpiderSizeFunction(_clone3);
                    SelectSpiderSizeFunction(_clone4);
                    SelectSpiderSizeFunction(_clone5);
                
                    _clone.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone2.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone3.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone4.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone5.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                
                    _clone.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone2.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone3.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone4.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                    _clone5.transform.GetChild(2).GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.25f, 0f);
                }
            }
        }
    }

    // change clone gameobject scale using code (using multiply as different assets have different scales)
    // need to call capsule collider and individualise the center as different scales will have different heights (prevent it from floating on the table)
    
    
    
    // set instantiated spider model prefab to selected size
    private void SelectSpiderSizeFunction(GameObject spiderClone)
    {
        // size s
        if (selectedGazeSize == 1)
        {
            // get the scale of the instantiated spider model prefab's transform
            Vector3 selectedScale = spiderClone.transform.localScale;
            // multiple the scale to get the desired size
            selectedScale *= 1f;
            // assign the selected scale to the instantiated spider model prefab's transform
            spiderClone.transform.localScale = selectedScale;
        }
        
        // size m
        if (selectedGazeSize == 2)
        {
            Vector3 selectedScale = spiderClone.transform.localScale;
            selectedScale *= 1.5f;
            spiderClone.transform.localScale = selectedScale;
        }
        
        // size l
        if (selectedGazeSize == 3)
        {
            Vector3 selectedScale = spiderClone.transform.localScale;
            selectedScale *= 3f;
            spiderClone.transform.localScale = selectedScale;
        }
    }
}
