using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] destinations;
    public int currentDestination;
    public float speed;
    public float minDistance;
    public void ChangeDestination()
    {
        if (currentDestination == destinations.Length - 1)
        {
            currentDestination = 0;
        }
        else
        {
            currentDestination++;
        }
    }
    public void MoveTowardsDestination()
    {
        if (Vector2.Distance(transform.position, destinations[currentDestination].position) <= minDistance)
        {
            ChangeDestination();
        }
        transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestination].position, speed * Time.deltaTime);
    }
    void Update()
    {
        MoveTowardsDestination();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
        collision.collider.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
        collision.collider.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
    }
}
