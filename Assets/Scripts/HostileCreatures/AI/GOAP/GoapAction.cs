using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    public HashSet<KeyValuePair<string, object>> preconditions;
    public HashSet<KeyValuePair<string, object>> effects;

    public float cost = 1f;
    public bool inRange = false;
    public GameObject target;
    public abstract bool CheckProceduralPreconditions(GameObject agent);
    public void DoResetAction()
    {
        inRange = false;
        target = null;
        ResetAction();
    }
    public abstract bool Perform(GameObject agent);

    public abstract bool RequiresInRange();

    public bool IsInRange()
    {
        return inRange;
    }

    public void SetInRange(bool val)
    {
        inRange = val;
    }
    public abstract void ResetAction();

    public abstract bool IsDone();

    public void AddPrecondition(string key, object value)
    {
        preconditions.Add(new KeyValuePair<string, object>(key, value));
    }
    public void RemovePrecondition(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> keyValuePair in preconditions)
        {
            if (keyValuePair.Key.Equals(key))
                remove = keyValuePair;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            preconditions.Remove(remove);
    }
    public void AddEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }
    public void RemoveEffect(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> keyValuePair in effects)
        {
            if (keyValuePair.Key.Equals(key))
                remove = keyValuePair;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            effects.Remove(remove);
    }
}
