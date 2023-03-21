using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : BaseEnemy
{
    private Animator animator;
    private float maxHealth = 1f;
    public float currentHealth = 1f;
    [SerializeField]
    private Healbar healbar;

    TimerEnemy timers;
    public int damageMushroom = 20;
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

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null )
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                currentHealth -= 0.000001f;
                check = true;
                animator.SetFloat("Health", currentHealth);

                if (check)
                {
                    animator.SetBool("IsAttack", true);
                }
            }
            else
            {
                check = false;
                animator.SetBool("IsAttack", false);
            }

            if (timers.isFinish)
            {
                healbar.gameObject.SetActive(true);

                if (!check)
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
                        check = false;
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
                playerController.TakeDamage(damageMushroom + level + 2);
            }
        }
        
    }



    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }


}
