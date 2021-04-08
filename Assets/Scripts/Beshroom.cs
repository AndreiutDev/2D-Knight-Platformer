using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beshroom : MonoBehaviour
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
        if (Vector2.Distance(playerTransform.position, transform.position) < chaseDistance && Vector2.Distance(playerTransform.position, transform.position) > arrivalDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, flyingSpeed * Time.deltaTime);
            transform.localScale = new Vector2((Mathf.Sign((transform.position - playerTransform.position).normalized.x)) * 2f, 2f);
        }
        
    }
}