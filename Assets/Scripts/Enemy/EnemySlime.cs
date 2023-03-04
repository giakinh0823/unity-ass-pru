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

    bool isWallTouch = false;
    public LayerMask wallerLayerMask;
    [SerializeField]
    private Transform wallCheckPoint;

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
        

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.03f, 0.5f), 0, wallerLayerMask);
        if (isWallTouch)
        {
            Flip();
        }

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


    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
