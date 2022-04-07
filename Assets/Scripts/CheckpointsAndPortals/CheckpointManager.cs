using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public Checkpoint[] checkpointList;
    public Checkpoint activeCheckpoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    public void ActivateCheckpoint(Checkpoint currentCheckpoint)
    {
        for (int index = 0; index < checkpointList.Length; index++)
        {
            if (checkpointList[index] == currentCheckpoint)
            {
                PlayerPrefs.SetInt("ActiveCheckpointIndex", index);
                checkpointList[index].SetActive();
                activeCheckpoint = currentCheckpoint;
            }
            else
            {
                checkpointList[index].SetInactive();
            }
        }
    }
    public void ResetCheckpoint()
    {
        PlayerPrefs.SetInt("ActiveCheckpointIndex", 0);
    }
    public void SetAllCheckpointsInactive()
    {
        for (int index = 0; index < checkpointList.Length; index++)
        {
            checkpointList[index].SetInactive();
        }
    }
}
