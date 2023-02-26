using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    private Animator animator;
    private float damage = 0.05f;
    public static float currentHealth = 1f;
    public GameObject healthBar;
    public Rigidbody2D rigidbody2D;

    public float moveSpeed = 2.0f;
    private float direction = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        rigidbody2D = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = moveSpeed;

        if (transform.position.x < -5f || transform.position.x > 5f)
        {
            direction = -direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        float currentMoveSpeed = moveSpeed * direction;

        transform.position += new Vector3(currentMoveSpeed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthBar = Instantiate(healthBar, new Vector2(transform.position.x, transform.position.y + 0.9f), Quaternion.identity);
            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= damage;
            Debug.Log(currentHealth);
            animator.SetFloat("Health", currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(healthBar);
                Destroy(gameObject, 3f);
            }
        }

    }

    

}
