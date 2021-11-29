using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifecycle : MonoBehaviour
{
    public float lifetime;
    public bool die;
    void Update() {
    if(die == true)
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
}
