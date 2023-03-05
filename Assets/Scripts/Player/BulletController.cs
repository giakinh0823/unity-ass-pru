using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public ExplosionBulletGround explosionBulletGround;

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Instantiate(explosionBulletGround, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(explosionBulletGround, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
