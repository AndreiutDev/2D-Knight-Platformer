using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private Popup nonStaticDamagePopupPrefab;
    [SerializeField]
    private Popup nonStaticEnemyDamagePopupPrefab;
    [SerializeField]
    private Popup nonstaticCheckpointPopupPrefab;

    public static Popup damagePopupPrefab;

    public static Popup enemyDamagePopupPrefab;

    public static Popup checkpointPopupPrefab;

    public static void InstantiateDamagePopup(Transform damagedEntity, Vector3 offset, int damage)
    {
        damagePopupPrefab.TextSetup(damage.ToString());
        Instantiate(damagePopupPrefab, damagedEntity.position + offset, Quaternion.identity);
    }
    public static void InstantiateEnemyDamagePopup(Transform damagedEntity, Vector3 offset, int damage)
    {
        enemyDamagePopupPrefab.TextSetup("-" + damage.ToString());
        Instantiate(enemyDamagePopupPrefab, damagedEntity.position + offset, Quaternion.identity);
    }
    public static void InstantiateCheckpointPopup(Transform checkpoint, Vector3 offset)
    {
        Instantiate(checkpointPopupPrefab, checkpoint.position + offset, Quaternion.identity);
    }
    public void OnAfterDeserialize()
    {
        damagePopupPrefab = nonStaticDamagePopupPrefab;
        enemyDamagePopupPrefab = nonStaticEnemyDamagePopupPrefab;
        checkpointPopupPrefab = nonstaticCheckpointPopupPrefab;
    }

    public void OnBeforeSerialize()
    {
        
    }
}