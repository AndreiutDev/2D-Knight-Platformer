using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    public bool isCheckpointActive;

    private Animator checkpointAnimator;


    private float activateAnimationTimer;
    private void Start()
    {
        checkpointAnimator = GetComponent<Animator>();
        PlayInactiveAnimation();
    }
    public void PlayActivateAnimation()
    {
        checkpointAnimator.Play("CheckpointActivate");
    }
    public void PlayInactiveAnimation()
    {
        checkpointAnimator.Play("CheckpointInactive");
    }
    public void PlayIdleAnimation()
    {
        checkpointAnimator.Play("CheckpointIdle");
    }
    public void SetActive()
    {
        isCheckpointActive = true;
        activateAnimationTimer = 1.4f;
        PlayActivateAnimation();
    }
    public void SetInactive()
    {
        isCheckpointActive = false;
        PlayInactiveAnimation();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckpointManager checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        if (isCheckpointActive == false && collision.gameObject.GetComponent<Player>() != null)
        {
            checkpointManager.ActivateCheckpoint(this);
            collision.gameObject.GetComponent<Player>().playerSpawn.spawnCheckpoint = spawnPoint;
        }
    }
    private void Update()
    {
        if (activateAnimationTimer >= 0)
        {
            activateAnimationTimer -= Time.deltaTime;
        }
        else
        {
            if (isCheckpointActive)
            {
                PlayIdleAnimation();
            }
            else
            {
                PlayInactiveAnimation();
            }
        }
    }
}
