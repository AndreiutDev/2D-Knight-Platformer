using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : MonoBehaviour
{
    public float immunityDuration;
    public float immunityDurationTimer;
    public bool isImmune => immunityDurationTimer > 0;

    private void Update()
    {
        immunityDurationTimer -= Time.deltaTime;
    }
    public void GainImmunity()
    {
        immunityDurationTimer = immunityDuration;
    }
}
