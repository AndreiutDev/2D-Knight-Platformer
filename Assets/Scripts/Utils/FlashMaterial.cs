using System.Collections;

using UnityEngine;


public class FlashMaterial : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    #endregion
    #region Private Fields


    private SpriteRenderer spriteRenderer;


    private Material originalMaterial;


    private Coroutine flashRoutine;

    #endregion

    #endregion


    #region Methods

    #region Unity Callbacks

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;


        flashMaterial = new Material(flashMaterial);
    }

    #endregion

    public void Flash(Color color)
    {

        if (flashRoutine != null)
        {

            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color)
    {

        spriteRenderer.material = flashMaterial;


        flashMaterial.color = color;


        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    #endregion
}
