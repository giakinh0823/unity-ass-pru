using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpriteRerender : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject prefab;

    private Sprite sprite;

    private void Start()
    {
        sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update()
    {
        UpdateSpriteGround();
    }

    public void UpdateSpriteGround()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(prefab.tag);
        foreach (GameObject gameObject in gameObjects)
        {
            SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.sprite = sprite;
            }
        }
    }
}
