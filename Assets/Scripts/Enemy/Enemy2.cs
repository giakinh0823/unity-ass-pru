using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator animator;
    public static float currentHealth;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = animator.GetFloat("Health");
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(healthBar, new Vector2(transform.position.x, transform.position.y + 0.7f), Quaternion.identity);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -=0.05f;
            Debug.Log(currentHealth);
            animator.SetFloat("Health", currentHealth);
            animator.SetBool("IsAttack", true);
            if (currentHealth <= 0)
            {
                Destroy(gameObject, 3f);
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



}
