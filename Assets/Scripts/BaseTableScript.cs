using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTableScript : MonoBehaviour
{
   // private BoxCollider bc;
    
    // Start is called before the first frame update
    void Start()
    {
       // bc = GetComponent<BoxCollider>();
        Physics.IgnoreLayerCollision(8,9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
