using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GizmoTest : MonoBehaviour
{
    public float radius;
    private void Start()
    {
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}