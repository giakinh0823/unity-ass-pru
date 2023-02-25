using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator animator;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = animator.GetInteger("Health");

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentHealth-=10;
            Debug.Log(currentHealth);
            animator.SetInteger("Health", currentHealth);
            if(currentHealth <= 0)
            {
                Destroy(gameObject, 3f);
            }
        }
    }



}
