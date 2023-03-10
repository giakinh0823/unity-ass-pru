using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1.5f;
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
            PlaySound();
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            Debug.Log(currentHealth);
            currentHealth -= damage;


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
