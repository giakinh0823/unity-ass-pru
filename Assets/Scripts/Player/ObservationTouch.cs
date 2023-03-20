using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationTouch : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private int damage = 100;
    public int damageBird = 100;


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
            }
            else if (collision.gameObject.layer == 19)
            {
                Debug.Log("Snake");
            }
            else if (collision.gameObject.layer == 20)
            {
                Debug.Log("Slime");
            }
            else
            {
                Debug.Log("Turtle");
            }

        }
        if(collision.gameObject.tag == "BulletBird")
        {
            Debug.Log("Bullet");
        }
    }


}
