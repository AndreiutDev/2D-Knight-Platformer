using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{
    public int health;
    void Start()
    {

    }
    public abstract void Behaviour();
    public abstract void TakeDamage(int damage);

    public IEnumerator InvokeTakeDamageWithDelay(int damage)
    {
        yield return new WaitForSeconds(0.1f);
        TakeDamage(damage);
    }
}
