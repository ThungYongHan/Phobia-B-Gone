using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTableScript : MonoBehaviour
{
    void Start()
    {
        Physics.IgnoreLayerCollision(8,9);
    }

}
