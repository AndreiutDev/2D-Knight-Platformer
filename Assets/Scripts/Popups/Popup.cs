using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public void TextSetup(string popupText)
    {
        this.GetComponent<TextMeshPro>().text = popupText;
    }
    void Update()
    {
        this.transform.position += moveSpeed * new Vector3(0, 1, 0) * Time.deltaTime;
    }
}
