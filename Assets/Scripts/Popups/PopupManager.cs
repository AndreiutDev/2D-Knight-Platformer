using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private Popup nonStaticDamagePopupPrefab;

    public static Popup damagePopupPrefab;

    public static void InstantiateDamagePopup(Transform damagedEntity, Vector3 offset, int damage)
    {
        damagePopupPrefab.TextSetup(damage.ToString());
        Instantiate(damagePopupPrefab, damagedEntity.position + offset, Quaternion.identity);
    }

    public void OnAfterDeserialize()
    {
        damagePopupPrefab = nonStaticDamagePopupPrefab;
    }

    public void OnBeforeSerialize()
    {
        
    }
}