using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShroom : MonoBehaviour
{
    //Cached component references
    public Transform playerTransform;
    public Rigidbody2D beeshroomRigidbody;
    //config
    public float flyingSpeed = 3f;
    public float chaseDistance = 10f;
    public float arrivalDistance = 0.5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        beeshroomRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        
    }
}