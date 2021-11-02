    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderCockroachAI : MonoBehaviour
{
    public float moveSpeed = 0.002f;
    public float rotSpeed = 0.5f;
    private bool isWandering;
    private bool isRotatingLeft;
    private bool isRotatingRight;
    private bool isAvoiding;
    private bool isWalking;
    
    private Animator animator;
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

        }

        // if the spider is engaging in avoiding behavior 
        if (isAvoiding)
        {
            // randomize spider movement speed when avoiding
            float randomAvoidMoveSpeed = Random.Range(0, 0.0003f);
            // randomize spider rotating speed when avoiding
            float randomAvoidRotateSpeed = Random.Range(1, 2.5f);
            transform.position += transform.forward * randomAvoidMoveSpeed;
            // rotates to the right at randomized spider rotation speed
            transform.Rotate(transform.up * randomAvoidRotateSpeed);
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
            //Debug.Log("testcold");
            animator.SetBool("rotate", false);
            StopCoroutine(Avoid());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "table_2")
        { 
            isWalking = false;
            StartCoroutine(Avoid()); 
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        isWalking = false;
        StartCoroutine(Avoid());
    }
    
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("rotate", false);
        StopCoroutine(Avoid());
    }
    

    IEnumerator Avoid()
    {
        float avoidTime = Random.Range(0.5f, 1.0f);
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
