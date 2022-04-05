using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public float RippleDuration = 1f;
    public float RippleTimer = 0f;
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    void Update()
    {
        if (RippleTimer > 0)
        {
            this.Amount = this.MaxAmount;
            Vector3 pos = Input.mousePosition;
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);
            RippleTimer -= Time.deltaTime;
        }
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }
    public void RippleEffect()
    {
        RippleTimer = RippleDuration;
    }
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}
