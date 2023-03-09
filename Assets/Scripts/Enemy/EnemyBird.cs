using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{

    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1.5f;
    [SerializeField]
    private Healbar healbar;

    public float speed = 2.0f;
    public Vector3 direction;

    bool isWallTouch = false;
    public LayerMask wallerLayerMask;
    [SerializeField]
    private Transform wallCheckPoint;

    public Transform playerTransfrom;
    public bool isChasing;
    public float chaseDistance;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        direction = Vector3.right;
    }

    void Update()
    {
        healbar.localScale.x = currentHealth;
        if (isChasing)
        {
            if (transform.position.x > playerTransfrom.position.x)
            {
                transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransfrom.position.x)
            {
                transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playerTransfrom.position) < chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                transform.position += (Vector3)(direction * speed * Time.deltaTime);

                isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector3(0.03f, 0.5f), 0, wallerLayerMask);
                if (isWallTouch)
                {
                    if (direction == Vector3.right)
                    {
                        transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                        direction = Vector3.left;
                    }
                    else
                    {
                        transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                        direction = Vector3.right;
                    }
                    isWallTouch = false;
                }
            }
            
        }


    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healbar.gameObject.SetActive(true);

            Quaternion rotation = collision.gameObject.transform.rotation;
            //gameObject.transform.rotation = rotation;
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
