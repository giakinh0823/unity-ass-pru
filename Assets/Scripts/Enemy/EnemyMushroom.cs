using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : BaseEnemy
{
    private Animator animator;
    private float maxHealth = 1f;
    private float currentHealth = 1f;
    [SerializeField]
    private Healbar healbar;

    TimerEnemy timers;
    public int damageMushroom = 20;

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
        animator.SetFloat("Health", currentHealth);
        animator.SetBool("IsAttack", true);
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
                animator.SetBool("IsAttack", false);
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
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            currentHealth -= GetDameArm();


        }
        else if (collision.gameObject.CompareTag("Knife"))
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
            currentHealth -= GetDameKnife();


            
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
        PlayerController playerController = player.GetComponent<PlayerController>();    
        if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
        {
            playerController.TakeDamage(damageMushroom + level + 2);
        }
    }



    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }


}
