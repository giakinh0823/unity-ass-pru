using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySlime : BaseEnemy
{
    private                  Animator animator;
    private                  float    damage        = 0.05f;
    public                   float    currentHealth = 1f;
    private                  float    maxHealth     = 1f;
    [SerializeField] private Healbar  healbar;

    public float   speed = 2.0f;
    public Vector3 direction;

    bool                                    isWallTouch = false;
    public                   LayerMask      wallerLayerMask;
    [SerializeField] private Transform      wallCheckPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public  Transform playerTransfrom;
    public  bool      isChasing;
    public  float     chaseDistance;
    public  float     distanceLimit = 3f;
    private float     distanceMoved = 0f;

    TimerEnemy timers;
    public int damageSlime = 20;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        direction        = Vector3.right;
        timers           = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();

        this.transform.localScale = Vector3.one * 3f;
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
            healbar.gameObject.SetActive(true);

            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                this.spriteRenderer.flipX =  true;
                transform.position        += Vector3.left * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                this.spriteRenderer.flipX =  false;
                transform.position        += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                this.spriteRenderer.flipX =  false;
                transform.position        += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                this.spriteRenderer.flipX =  true;
                transform.position        += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            this.spriteRenderer.flipX = this.direction.x > 0;

            distanceMoved += speed * Time.deltaTime;
            if (distanceMoved >= distanceLimit)
            {
                distanceMoved = 0f;
                direction     = -direction;
            }

            if (timers.isFinish)
            {
                if (currentHealth < maxHealth)
                {
                    healbar.gameObject.SetActive(true);
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
        }

        animator.SetFloat("Health", currentHealth);
        

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject, 2f);
        }

        healbar.localScale.x = currentHealth;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            this.spriteRenderer.flipX = rotation.x * Vector3.right.x > 0;
            PlaySound();
            currentHealth -= GetDameGun();
        }
    }

    public void AttackPlayer()
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                playerController.TakeDamage(damageSlime + level + 2);
            }
        }
    }

    public void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }


    private void OnDestroy()
    {
        if (this.QuestPlayerController is { IsReadyToUse: true }) this.QuestPlayerController.QuestSlime--;
    }
}