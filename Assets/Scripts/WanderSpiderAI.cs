    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderSpiderAI : MonoBehaviour
{
    public float moveSpeed = 0.004f;
    //public float moveSpeed = 0.002f;
    public float rotSpeed = 0.6f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isAvoiding = false;
    private bool isWalking = false;
    
    private Animator animator;
    // public Rigidbody rb;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Move()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * rotSpeed);
           //transform.Rotate(0, 0.1f, 0 * Time.deltaTime);

        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * -rotSpeed);
            //transform.Rotate(0, 0.1f, 0 * Time.deltaTime);

        }
        
        if (isAvoiding == true)
        {
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                transform.position += transform.forward * 0.0001f;
                transform.Rotate(transform.up * 2f);
            }
            else if (random == 1)
            {
                transform.position += transform.forward * 0f;
                transform.Rotate(transform.up * 1.0f);
            }
            else if (random == 2)
            {
                transform.position += transform.forward * 0.0003f;
                transform.Rotate(transform.up * 2.5f);
            }
            else if (random == 3)
            {
                transform.position += transform.forward * 0.00015f;
                transform.Rotate(transform.up * 1.5f);
            }
            /*else if (random == 2)
            {
                transform.position += transform.forward * 0f;
                transform.Rotate(transform.up * 1.5f);
            }*/
            //transform.Rotate(transform.up * rotSpeed);
            /*int rotate = Random.Range(1, 3);
            if (rotate == 1)
            {
                transform.Rotate(transform.up * 2.0f);
            }
            else
            {
                transform.Rotate(transform.up * - 2.0f);
            }*/
        }

        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed;
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        isWalking = false;
        StartCoroutine(Avoid());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "table_2")
        { 
            isWalking = false;
            StartCoroutine(Avoid()); 
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name != "table_2")
        { 
            isWalking = false;
            StartCoroutine(Avoid()); 
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name != "table_2")
        {
            Debug.Log("testcold");
            animator.SetBool("rotate", false);
            StopCoroutine(Avoid());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("test");
        animator.SetBool("rotate", false);
        StopCoroutine(Avoid());
    }
    

    IEnumerator Avoid()
    {
        float avoidTime = Random.Range(0.1f, 1.0f);
            isAvoiding = true;
            animator.SetBool("rotate", true);
            yield return new WaitForSeconds(avoidTime);
            isAvoiding = false;
    }
    
    IEnumerator Wander()
    {
        float rotTime = Random.Range(0.5f,1.0f);
        float rotateWait = Random.Range(0.1f,0.3f);
        float rotateLorR = Random.Range(1, 3);
        float walkWait = Random.Range(0.5f, 3);
        float walkTime = Random.Range(0.5f,3.0f);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            animator.SetBool("rotate", true);
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
            animator.SetBool("rotate", false);
        }
        
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            animator.SetBool("rotate", true);
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
            animator.SetBool("rotate", false);

        }
        
        isWandering = false;
    }
}
