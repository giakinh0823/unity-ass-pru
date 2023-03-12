using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResizeBackground : MonoBehaviour
{
    private SpriteRenderer background;
    private Camera mainCamera;

    void Start()
    {
        background = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Resize();
    }

    void Resize()
    {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        // Resize camera
        float screenAspect = screenWidth / screenHeight;
        mainCamera.aspect = screenAspect;

        // Resize background
        float spriteHeight = background.sprite.bounds.size.y;
        float spriteWidth = background.sprite.bounds.size.x;

        float worldScreenHeight = mainCamera.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight * screenAspect;

        float scale = Mathf.Max(worldScreenWidth / spriteWidth, worldScreenHeight / spriteHeight);
        background.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
