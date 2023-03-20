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
    public int damageMushroom = 20;
    public int damageSnake = 30;
    public int damageSlime = 20;
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

        if (collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.layer == 17)
            {
                Debug.Log("Bird");
            }else if(collision.gameObject.layer == 18)
            {
                Debug.Log("Mushroom");
                playerController.TakeDamage(damageMushroom);
            }
            else if (collision.gameObject.layer == 19)
            {
                Debug.Log("Snake");
                playerController.TakeDamage(damageSnake);
            }
            else if (collision.gameObject.layer == 20)
            {
                Debug.Log("Slime");
                playerController.TakeDamage(damageSlime);
            }
            else
            {
                Debug.Log("Turtle");
                playerController.TakeDamage(damageTurtle);
            }

        }
        if(collision.gameObject.tag == "BulletBird")
        {
            Debug.Log("Bullet");
            playerController.TakeDamage(damageGun);
            Destroy(collision.gameObject);
        }
    }


}
