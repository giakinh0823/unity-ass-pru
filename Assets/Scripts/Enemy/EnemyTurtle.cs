using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : MonoBehaviour
{

    private Animator animator;
    private float maxHealth = 0.5f;
    private float currentHealth = 0.5f;
    [SerializeField]
    private Healbar healbar;
    private float dameArm = 0.2f;
    private float dameKnife = 0.5f;
    private float dameGun = 0.25f;
    TimerEnemy timers;

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
            if(rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
            }
            currentHealth -= dameArm;


        }else if (collision.gameObject.CompareTag("Knife"))
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
            currentHealth -= dameKnife;


        }else if (collision.gameObject.CompareTag("Bullet"))
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
            currentHealth -= dameGun;


        }
    }
    

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

}
