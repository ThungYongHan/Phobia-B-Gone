/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PBSpiderScript : MonoBehaviour
{
    public float moveSpeed = 0.05f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;


   private Rigidbody rigidbodyComponent;
   private float m_Speed;
   private Animator animator;
   private int rdmmvm;
   
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
      m_Speed = 0.05f;
      animator = GetComponent<Animator>();
      //rdmmvm = Random.Range(1, 6);
      Debug.Log(Random.Range(1, 6));
    }
    
    void Update()
    {
        /*if (isWandering == false)
        {
            StartCoroutine(Wander());
        }#1#
    }

    /*IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait
    }#1#
    
    void FixedUpdate()
    { 
       transform.position += transform.forward * (Time.deltaTime * m_Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.Rotate(0,90,0);
    }
    
    

}
*/
