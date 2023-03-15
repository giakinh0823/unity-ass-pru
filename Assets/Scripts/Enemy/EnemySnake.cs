using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private float currentHealth = 1f;
    [SerializeField]
    private Healbar healbar;
    private float dameArm = 0.1f;
    private float dameKnife = 0.2f;
    private float dameGun = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
    }

    private void Update()
    {
        healbar.localScale.x = currentHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") &&
            !collision.gameObject.CompareTag("ArmLeft") &&
            !collision.gameObject.CompareTag("ArmRight") &&
            !collision.gameObject.CompareTag("Knife") &&
            !collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetBool("IsAttack", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("IsAttack", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ArmLeft")
            || collision.gameObject.CompareTag("ArmRight"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            currentHealth -= dameArm;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }
        else if (collision.gameObject.CompareTag("Knife"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            currentHealth -= dameKnife;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            currentHealth -= dameGun;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }

    }

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
