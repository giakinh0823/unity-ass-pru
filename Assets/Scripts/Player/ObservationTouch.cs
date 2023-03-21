using Model;
using ScreenManager.Screens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationTouch : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private int damage = 100;
    public int damageGun = 30;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Observation")
        {
            playerController.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        if(collision.gameObject.tag == "BulletBird")
        {
            playerController.TakeDamage(damageGun + level + 2);
            Destroy(collision.gameObject);
        }
    }

   
}


