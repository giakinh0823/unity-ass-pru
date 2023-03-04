using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1f;
    [SerializeField]
    private Healbar healbar;

    public float speed = 2.0f;
    public float distance = 5.0f;

    private Vector3 startPos;
    private Vector3 localScale;
    private float dirX = -1f;
    private bool facingRight = false;
    private Rigidbody2D rb;
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);

        startPos = transform.position;
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        healbar.localScale.x = currentHealth;
        enemyObject = GameObject.FindGameObjectWithTag("Player");
        float distanceToEnemy = Vector2.Distance(transform.position, enemyObject.transform.position);

        if (distanceToEnemy <= 3)
        {
            Vector2 targetPosition = new Vector2(enemyObject.transform.position.x, transform.position.y);
            rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime));

        }
        else
        {
            float distanceMoved = Mathf.Abs(transform.position.x - startPos.x);
            if (distanceMoved >= distance)
            {
                dirX *= -1f;
                localScale.x *= -1;
                transform.localScale = localScale;
                startPos = transform.position;
            }

            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            CheckWhereToFace();
        }

    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healbar.gameObject.SetActive(true);

            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= damage;
            Debug.Log(currentHealth);
            animator.SetFloat("Health", currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 3f);
            }
        }

    }



}
