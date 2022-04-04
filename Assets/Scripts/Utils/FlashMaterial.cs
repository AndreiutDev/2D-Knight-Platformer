using System.Collections;

using UnityEngine;


public class FlashMaterial : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [SerializeField] private Material flashMaterial;

    #endregion
    #region Private Fields


    private SpriteRenderer spriteRenderer;


    private Material originalMaterial;


    private Coroutine flashRoutine;

    private Coroutine complexFlashRoutine;

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
    public void ComplexFlash(Color primaryColor, Color secondaryColor, float duration)
    {
        if (complexFlashRoutine != null)
        {
            StopCoroutine(complexFlashRoutine);
        }
        complexFlashRoutine = StartCoroutine(ComplexFlashRoutine(primaryColor, secondaryColor, duration));
    }
    public void Flash(Color color, float duration)
    {

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine(color, duration));
    }

    private IEnumerator FlashRoutine(Color color, float duration)
    {

        spriteRenderer.material = flashMaterial;


        flashMaterial.color = color;


        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }
    private IEnumerator ComplexFlashRoutine(Color primaryColor, Color secondaryColor, float duration)
    {
        spriteRenderer.material = flashMaterial;
        for (int flashTimes = 0; flashTimes <= 5; flashTimes++)
        {
            flashMaterial.color = primaryColor;

            yield return new WaitForSeconds(duration);

            flashMaterial.color = secondaryColor;

            yield return new WaitForSeconds(duration);
        }
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

    #endregion
}
