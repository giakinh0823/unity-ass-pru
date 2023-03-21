using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySlime : BaseEnemy
{
    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1f;
    private float maxHealth = 1f;
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
    
    TimerEnemy timers;
    public int damageSlime = 20;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        direction = Vector3.right;
        timers = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();
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


        }
        animator.SetFloat("Health", currentHealth);
        if (timers.isFinish)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += currentHealth * 5 / 100;
                timers.alarmTime = 1;
                timers.StartTime();
            }
            else
            {
                healbar.gameObject.SetActive(false);
                return;
            }
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject, 2f);
        }
        healbar.localScale.x = currentHealth;


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
                gameObject.transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
            }
            currentHealth -= GetDameArm();


        }
        else if (collision.gameObject.CompareTag("Knife"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
            }
            currentHealth -= GetDameKnife();



        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
            }
            currentHealth -= GetDameGun();



        }


    }

    public void AttackPlayer()
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
        {
            playerController.TakeDamage(damageSlime + level + 2);
        }
    }

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }





}
