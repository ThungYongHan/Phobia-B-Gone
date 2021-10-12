/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpareWanderSpider : MonoBehaviour
{
    public float moveSpeed = 0.009f;
    public float rotSpeed = 1.5f;
    private Collider collid;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private int id;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * rotSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * -rotSpeed);
        }

        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed;
        }#1#
        //collid = GetComponent<Collider>();
        //id = GetInstanceID();
    }

    void FixedUpdate()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * rotSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * -rotSpeed);
        }

        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed;
        }
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "table_2")
        {
            Debug.Log("collidetable");
        }
        if (other.gameObject.name == "TableColliders")
        {
            Debug.Log("colliders");
            transform.Rotate(transform.up * -rotSpeed);
        }
        else
        { 
            /*if (isWandering == false)
            {
                StartCoroutine(Wander());
            }#1#

            /*if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * rotSpeed);
            }#1#
            
            /*if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * -rotSpeed);
            }#1#

            /*if (isWalking == true)
            {
                transform.position += transform.forward * moveSpeed;
            }#1#
 
        }
    }
    
    /*private void OnCollisionEnter(Collision enter)
    {
        if (enter.gameObject.name == "table_2")
        {
            Debug.Log("collidetable");
        }
        if (enter.gameObject.name == "TableColliders")
        {
            Debug.Log("colliders");
        }
        else
        { 
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }

            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * rotSpeed);
            }
            
            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * -rotSpeed);
            }

            if (isWalking == true)
            {
                transform.position += transform.forward * moveSpeed;
            }
            Debug.Log("collide");
        }

    }#1#
    
    IEnumerator Wander()
    {
        /*int rotTime = Random.Range(1,3);
        int rotateWait = Random.Range(1,4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 3);#1#
        float rotTime = Random.Range(0.1f,1.0f);
        float rotateWait = Random.Range(0.1f,0.3f);
        float rotateLorR = Random.Range(1, 3);
        float walkWait = Random.Range(0.1f, 3);
        float walkTime = Random.Range(0.5f,2.0f);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }

        isWandering = false;
    }
    
    
    /*private void OnTriggerEnter(Collider other)
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * rotSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * -rotSpeed);
        }

        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed;
        }
    }#1#

    /*private void OnCollisionEnter(Collision dataFromCollision)
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(90.0f, 0.0f, 0.0f);
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(90.0f, 0.0f, 0.0f);
        }

        /*if (isRotatingLeft == true)
        {
            transform.Rotate(transform.right * -rotSpeed);
        }#2#
        /*if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * rotSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * -rotSpeed);
        }#2#

        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed;
        }
        
        /*if (dataFromCollision.gameObject.GetInstanceID() != id)#2#
        if (dataFromCollision.gameObject.name == "2spider")
        {
            Debug.Log("HIT SPIDER");
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }

            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * rotSpeed);
            }

            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * -rotSpeed);
            }

            if (isWalking == true)
            {
                transform.position += transform.forward * moveSpeed;
            }
        }
        
    }#1#
}
*/
