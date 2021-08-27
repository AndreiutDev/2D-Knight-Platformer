using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifecycle : MonoBehaviour
{
    public float lifetime;
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
