using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : BaseEnemy
{

    private Animator animator;
    private float maxHealth = 0.5f;
    public float currentHealth = 0.5f;
    [SerializeField]
    private Healbar healbar;

    TimerEnemy timers;
    public int damageTurtle = 10;
    bool check;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        timers = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();
    }

    private void Update()
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
                        currentHealth += currentHealth * 5 / 100;
                        timers.alarmTime = 1;
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
            healbar.localScale.x = currentHealth;




        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentHealth -= 0.000001f;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
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
            currentHealth -= GetDameGun();


        }
    }

    public void AttackPlayer()
    {
        int level = PlayerLocalData.Instance.CurrentPlayerLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                playerController.TakeDamage(damageTurtle + level + 2);
            }
        }
        
    }


    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

}
