using Constant;
using UnityEngine;

public class RandomeColorBackground : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = ColorBackgroundConstant.GetRandomColor();
    }
}