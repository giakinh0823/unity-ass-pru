using System.Collections;
using System.Collections.Generic;
using Common;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private SlidingController slidingController;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private ShootController shootController;

    public AudioSource soundShoots;
    public AudioSource soundSlashing;


    private int hit;

    public float dameGunPlayerAttackEnemy = 0.25f;

    void Start()
    {
        hit               = 0;
        slidingController = gameObject.GetComponent<SlidingController>();
        playerController  = gameObject.GetComponent<PlayerController>();

        FindObjectOfType<InputManager>().attack += this.OnAttack;
    }

    private void OnAttack()
    {
        if (hit >= 2 && !slidingController.isWallSliding)
        {
            playerController.anim.SetTrigger("isLongHit");
            hit = 0;
        }
        else if (!slidingController.isWallSliding)
        {
            playerController.anim.SetTrigger("isShortHit");
            hit++;
        }
    }

    public void AttackEnemyByArm()
    {
        // Giảm sát thương mỗi level đi 0.01 tối đa còn 0.1
        float dameArmPlayerAttackEnemy = Mathf.Clamp(0.2f - PlayerLocalData.Instance.CurrentPlayerLevel * 0.01f, 0.1f, 0.2f);

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemy)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, go.transform.position)) <= 2f)
            {
                if (go.layer == 21)
                {
                    EnemyTurtle enemyTurtle = go.GetComponent<EnemyTurtle>();
                    Vector2     rotation    = enemyTurtle.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyTurtle.transform.position.x)
                        {
                            enemyTurtle.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyTurtle.transform.position.x)
                        {
                            enemyTurtle.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemyTurtle.PlaySound();
                    enemyTurtle.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 18)
                {
                    EnemyMushroom enemyMushroom = go.GetComponent<EnemyMushroom>();
                    Vector2       rotation      = enemyMushroom.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyMushroom.transform.position.x)
                        {
                            enemyMushroom.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyMushroom.transform.position.x)
                        {
                            enemyMushroom.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemyMushroom.PlaySound();
                    enemyMushroom.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 19)
                {
                    EnemySnake enemySnake = go.GetComponent<EnemySnake>();
                    Vector2    rotation   = enemySnake.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemySnake.transform.position.x)
                        {
                            enemySnake.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemySnake.transform.position.x)
                        {
                            enemySnake.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemySnake.PlaySound();
                    enemySnake.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 20)
                {
                    EnemySlime enemySlime = go.GetComponent<EnemySlime>();
                    Vector2    rotation   = enemySlime.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemySlime.transform.position.x)
                        {
                            enemySlime.transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemySlime.transform.position.x)
                        {
                            enemySlime.transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
                        }
                    }
                    enemySlime.PlaySound();
                    enemySlime.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 17)
                {
                    EnemyBird enemyBird = go.GetComponent<EnemyBird>();
                    Vector2   rotation  = enemyBird.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyBird.transform.position.x)
                        {
                            enemyBird.transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyBird.transform.position.x)
                        {
                            enemyBird.transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                        }
                    }
                    enemyBird.PlaySound();
                    enemyBird.currentHealth -= dameArmPlayerAttackEnemy;
                }
            }
        }
    }

    public void AttackEnemyByKnife()
    {
        // Giảm sát thương của player mỗi level đi 0.05, giảm tối đa 0.4 tức còn 0.1
        float dameKnifePlayerAttackEnemy = Mathf.Clamp(0.5f - PlayerLocalData.Instance.CurrentPlayerLevel * 0.05f, 0.1f, 1f);

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemy)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, go.transform.position)) <= 2f)
            {
                if (go.layer == 21)
                {
                    EnemyTurtle enemyTurtle = go.GetComponent<EnemyTurtle>();
                    Vector2     rotation    = enemyTurtle.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyTurtle.transform.position.x)
                        {
                            enemyTurtle.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyTurtle.transform.position.x)
                        {
                            enemyTurtle.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemyTurtle.PlaySound();
                    enemyTurtle.currentHealth -= dameKnifePlayerAttackEnemy;
                }
                else if (go.layer == 18)
                {
                    EnemyMushroom enemyMushroom = go.GetComponent<EnemyMushroom>();
                    Vector2       rotation      = enemyMushroom.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyMushroom.transform.position.x)
                        {
                            enemyMushroom.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyMushroom.transform.position.x)
                        {
                            enemyMushroom.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemyMushroom.PlaySound();
                    enemyMushroom.currentHealth -= dameKnifePlayerAttackEnemy;
                }
                else if (go.layer == 19)
                {
                    EnemySnake enemySnake = go.GetComponent<EnemySnake>();
                    Vector2    rotation   = enemySnake.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemySnake.transform.position.x)
                        {
                            enemySnake.transform.localScale = new Vector3(-0.7990404f, 0.824f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemySnake.transform.position.x)
                        {
                            enemySnake.transform.localScale = new Vector3(0.7990404f, 0.824f, 1);
                        }
                    }
                    enemySnake.PlaySound();
                    enemySnake.currentHealth -= dameKnifePlayerAttackEnemy;
                }
                else if (go.layer == 20)
                {
                    EnemySlime enemySlime = go.GetComponent<EnemySlime>();
                    Vector2    rotation   = enemySlime.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemySlime.transform.position.x)
                        {
                            enemySlime.transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemySlime.transform.position.x)
                        {
                            enemySlime.transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
                        }
                    }
                    enemySlime.PlaySound();
                    enemySlime.currentHealth -= dameKnifePlayerAttackEnemy;
                }
                else if (go.layer == 17)
                {
                    EnemyBird enemyBird = go.GetComponent<EnemyBird>();
                    Vector2   rotation  = enemyBird.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if (transform.position.x > enemyBird.transform.position.x)
                        {
                            enemyBird.transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                        }
                    }
                    else
                    {
                        if (transform.position.x < enemyBird.transform.position.x)
                        {
                            enemyBird.transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                        }
                    }
                    enemyBird.PlaySound();
                    enemyBird.currentHealth -= dameKnifePlayerAttackEnemy;
                }
            }
        }
    }

    private void SoundHitGun()
    {
        soundShoots.Play();
    }

    private void SoundHitSlashing()
    {
        soundSlashing.Play();
    }

    private void ShootHitGun()
    {
        shootController.Shoot();
    }
}