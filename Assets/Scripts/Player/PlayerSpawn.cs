using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnCheckpoint;
    public void Start()
    {
        this.transform.position = spawnCheckpoint.position;
    }
    public void SpawnPlayerToCheckpoint()
    {
        if (spawnCheckpoint != null)
        {
            transform.position = spawnCheckpoint.position;
        }
    }
}
