using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : BaseEnemy
{
    private                  Animator animator;
    private                  float    maxHealth     = 1f;
    public                   float    currentHealth = 1f;
    [SerializeField] private Healbar  healbar;

    TimerEnemy timers;
    public int damageMushroom = 20;
    bool       check;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        timers           = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();
    }

    void Update()
    {
        animator.SetFloat("Health", currentHealth);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                animator.SetBool("IsAttack", true);
                healbar.gameObject.SetActive(true);
            }
            else
            {
                animator.SetBool("IsAttack", false);
                if (timers.isFinish)
                {
                    if (currentHealth < maxHealth)
                    {
                        currentHealth    += currentHealth * 5 / 100;
                        timers.alarmTime =  1;
                        timers.StartTime();
                    }
                    else
                    {
                        healbar.gameObject.SetActive(false);
                    }
                }

            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 2f);
            }

            healbar.Percent = currentHealth;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            PlaySound();
            currentHealth -= GetDameGun();
        }
    }

    public void AttackPlayer()
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                playerController.TakeDamage(damageMushroom + level + 2);
            }
        }
    }


    public void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnDestroy()
    {
        if (this.QuestPlayerController  is { IsReadyToUse: true })  this.QuestPlayerController.QuestMushroom--;
    }
}