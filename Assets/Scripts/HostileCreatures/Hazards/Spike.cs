using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    //Config
    [SerializeField] private float spikeEmergeDistance;

    //Cached component references
    public Transform playerTransform;
    public Animator spikeAnimator;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spikeAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < spikeEmergeDistance)
        {
            spikeAnimator.SetBool("Emerge", true);
            spikeAnimator.SetBool("Hide", false);

        }
        else
        {
            spikeAnimator.SetBool("Hide", true);
            spikeAnimator.SetBool("Emerge", false);
        }
    }
}
