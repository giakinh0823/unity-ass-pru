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
    public int damageTurtle = 10;
    public int damageSnake = 30;
    public int damageSlime = 20;
    public int damageGun = 30;
    public Transform mushroomTransfrom;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Observation")
        {
            playerController.TakeDamage(damage);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        if (collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.layer == 17)
            {
            }else if(collision.gameObject.layer == 18)
            {
                playerController.TakeDamage(damageMushroom + level + 2);
            }
            else if (collision.gameObject.layer == 19)
            {
                playerController.TakeDamage(damageSnake + level + 2);
            }
            else if (collision.gameObject.layer == 20)
            {
                playerController.TakeDamage(damageSlime + level + 2);
            }
            else
            {
                playerController.TakeDamage(damageTurtle + level + 2);
            }

        }
        if(collision.gameObject.tag == "BulletBird")
        {
            playerController.TakeDamage(damageGun + level + 2);
            Destroy(collision.gameObject);
        }
    }*/

   
}


