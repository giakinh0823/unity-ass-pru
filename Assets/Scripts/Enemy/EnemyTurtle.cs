using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : MonoBehaviour
{

    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 0.5f;
    [SerializeField]
    private Healbar healbar;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("Enemy1AnimaTancong"); 
        }
        else if (collision.gameObject.CompareTag("ArmLeft") || collision.gameObject.CompareTag("ArmLeft"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= damage;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }else if (collision.gameObject.CompareTag("Knife"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= 0.07f;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }else if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= 0.06f;


            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsAttack", false);
        }

    }

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

}
