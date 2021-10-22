    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderSpiderAI : MonoBehaviour
{
    // spider movement speed
    public float movementSpeed = 0.002f;
    // spider rotation speed
    public float rotationSpeed = 0.5f;
    // when spider is engaging in wandering behavior (default false)
    private bool _isWandering;
    // when spider is rotating to the left (default false)
    private bool _isRotatingLeft;
    // when spider is rotating to the right (default false)
    private bool _isRotatingRight;
    // when spider is engaging in avoiding behavior (default false)
    private bool _isAvoiding;
    // when spider is engaging in walking behavior (default false)
    private bool _isWalking;
    private Animator _animator;
    
    void Awake()
    {
        // reference animator component attached to spider prefab
        _animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        // having this if statement so that the Wander coroutine is not
        // started 50 times per second (FixedUpdate fps)
        if (_isWandering == false)
        {
            StartCoroutine(Wander());
        }

        // if the spider is rotating to the right 
        if (_isRotatingRight)
        {
            // rotates to the right at predefined spider rotation speed
            transform.Rotate(transform.up * rotationSpeed);
        }
        
        // if the spider is rotating to the left
        if (_isRotatingLeft)
        {
            // rotates to the left at predefined spider rotation speed
            transform.Rotate(transform.up * -rotationSpeed);
        }
        
        // if the spider is engaging in avoiding behavior 
        if (_isAvoiding)
        {
            // randomize spider movement speed when avoiding
            float randomAvoidMoveSpeed = Random.Range(0, 0.0003f);
            // randomize spider rotating speed when avoiding
            float randomAvoidRotateSpeed = Random.Range(1, 2.5f);
            transform.position += transform.forward * randomAvoidMoveSpeed;
            // rotates to the right at randomized spider rotation speed
            transform.Rotate(transform.up * randomAvoidRotateSpeed);
        }

        // if the spider is walking
        if (_isWalking)
        {
            // moves the spider forward at predefined spider movement speed
            transform.position += transform.forward * movementSpeed;
            // transition from idle animation to walk animation
            _animator.SetBool("walk", true);
        }
        else
        {
            // transition from walk animation to idle animation
            _animator.SetBool("walk", false);
        }
    }
    
    // when the spider's collider begun touching other colliders 
    private void OnCollisionEnter(Collision other)
    {
        // if the collider that is collided with is not the table_2 collider
        if (other.gameObject.name != "table_2")
        { 
            // stop spider walking behavior
            _isWalking = false;
            StartCoroutine(Avoid()); 
        }
    }
    
    // for every frame the spider's collider is touching other colliders
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name != "table_2")
        { 
            _isWalking = false;
            StartCoroutine(Avoid()); 
        }
    }

    // when the spider's collider stops touching other colliders
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name != "table_2")
        {
            // stop spider rotating animation
            _animator.SetBool("rotate", false);
            StopCoroutine(Avoid());
        }
    }
    
    // when the spider's collider collides with colliders that have 'Is Trigger' checked
    private void OnTriggerEnter(Collider other)
    {
        _isWalking = false;
        StartCoroutine(Avoid());
    }
    
    // for every frame the spider's collider collides with colliders that have 'Is Trigger' checked
    private void OnTriggerStay(Collider other)
    {
        _isWalking = false;
        StartCoroutine(Avoid());
    }
    
    // when the spider's collider stops colliding with colliders that have 'Is Trigger' checked'
    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("rotate", false);
        StopCoroutine(Avoid());
    }
    

    IEnumerator Avoid()
    {
            float avoidTime = Random.Range(0.1f, 1.0f);
            _isAvoiding = true;
            _animator.SetBool("rotate", true);
            yield return new WaitForSeconds(avoidTime);
            _isAvoiding = false;
    }
    
    IEnumerator Wander()
    {
        // randomize the time taken for spider rotation
        float rotationTime = Random.Range(0.5f,1);
        // randomize the time taken in between spider rotation
        float rotateWait = Random.Range(0.1f,0.3f);
        // randomize the direction (left or right) that the spider rotates in 
        int rotateLeftOrRight = Random.Range(1, 3);
        // randomize the time taken before spider engages in walking behavior
        float walkWait = Random.Range(0.5f, 3);
        // randomize the time taken for spider walking behavior
        float walkTime = Random.Range(0.5f,3);
        
        // ensure that the Wander coroutine is not entered in
        // every frame as Wander coroutine is started if is_Wandering = false
        _isWandering = true;
        
        // wait for randomized walkWait duration 
        yield return new WaitForSeconds(walkWait);
        // engage the spider in walking behavior
        _isWalking = true;
        // spider walks for randomized walkTime duration 
        yield return new WaitForSeconds(walkTime);
        // stop spider walking behavior
        _isWalking = false;
        // wait for randomized rotateWait duration 
        yield return new WaitForSeconds(rotateWait);
        
        switch (rotateLeftOrRight)
        {
            // 1 - rotate right
            case 1:
                // spider starts rotating to the right
                _isRotatingRight = true;
                // transition from idle animation to rotate animation
                _animator.SetBool("rotate", true);
                // spider rotates for randomized rotationTime duration 
                yield return new WaitForSeconds(rotationTime);
                // stops spider's rotation to the right
                _isRotatingRight = false;
                // transition from rotate animation to idle animation
                _animator.SetBool("rotate", false);
                break;
            // 2 - rotate left 
            case 2:
                _isRotatingLeft = true;
                _animator.SetBool("rotate", true);
                yield return new WaitForSeconds(rotationTime);
                _isRotatingLeft = false;
                _animator.SetBool("rotate", false);
                break;
        }
        
        // start the Wander coroutine again as Wander coroutine
        // is started if is_Wandering = false
        _isWandering = false;
    }
}
