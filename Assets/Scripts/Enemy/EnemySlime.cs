using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1f;
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
    public float distanceLimit = 3f; 
    private float distanceMoved = 0f; 



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        direction = Vector3.right;
    }

    void Update()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransfrom = player.transform;
        }
        healbar.localScale.x = currentHealth;
        if (playerTransfrom != null && Vector3.Distance(transform.position, playerTransfrom.position) <= chaseDistance)
        {
            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);

            }
            else
            {
                transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
            }
            distanceMoved += speed * Time.deltaTime;
            if (distanceMoved >= distanceLimit)
            {
                distanceMoved = 0f; 
                direction = -direction; 
            }

            /*isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector3(0.03f, 0.5f), 0, wallerLayerMask);
            Debug.Log(isWallTouch);
            if (isWallTouch)
            {
                if (direction == Vector3.right)
                {
                    transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
                    direction = Vector3.left;
                }
                else
                {
                    transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
                    direction = Vector3.right;
                }
                isWallTouch = false;
            }*/

        }


    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (direction == Vector3.right)
            {
                transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                direction = Vector3.left;
            }
            else
            {
                transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                direction = Vector3.right;
            }
            isWallTouch = false;
        }
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlaySound();
            healbar.gameObject.SetActive(true);

            //Quaternion rotation = collision.gameObject.transform.rotation;
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

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }





}
