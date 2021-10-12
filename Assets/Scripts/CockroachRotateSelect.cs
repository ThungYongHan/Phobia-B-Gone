using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachRotateSelect : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.05f, 0 * Time.deltaTime);
    }
}