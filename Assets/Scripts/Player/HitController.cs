using System.Collections;
using System.Collections.Generic;
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

    private MyPlayerActions playerInput;
    private InputAction attackInput;

    public float dameKnifePlayerAttackEnemy = 0.5f;
    public float dameGunPlayerAttackEnemy = 0.25f;

    private void Awake()
    {
        playerInput = new MyPlayerActions();
    }


    void Start()
    {
        hit = 0;
        slidingController = gameObject.GetComponent<SlidingController>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (attackInput.triggered)
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
    }

    public void AttackEnemyByArm()
    {
    float dameArmPlayerAttackEnemy = 0.2f;

    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemy)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, go.transform.position)) <= 2f)
            {
                if(go.layer== 21) {
                    EnemyTurtle enemyTurtle = go.GetComponent<EnemyTurtle>();
                    Vector2 rotation = enemyTurtle.transform.localScale;
                    if (rotation.x * transform.localScale.x > 0)
                    {
                        if(transform.position.x > enemyTurtle.transform.position.x)
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
                    
                    enemyTurtle.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if(go.layer== 18)
                {
                    EnemyMushroom enemyMushroom = go.GetComponent<EnemyMushroom>();
                    Vector2 rotation = enemyMushroom.transform.localScale;
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
                    enemyMushroom.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 19)
                {
                    EnemySnake enemySnake = go.GetComponent<EnemySnake>();
                    Vector2 rotation = enemySnake.transform.localScale;
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
                    enemySnake.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 20)
                {
                    EnemySlime enemySlime = go.GetComponent<EnemySlime>();
                    Vector2 rotation = enemySlime.transform.localScale;
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
                    enemySlime.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 17)
                {
                    EnemyBird enemyBird = go.GetComponent<EnemyBird>();
                    Vector2 rotation = enemyBird.transform.localScale;
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
                    enemyBird.currentHealth -= dameArmPlayerAttackEnemy;
                }

            }
        }
       
    }

    public void AttackEnemyByKnife()
    {
        float dameArmPlayerAttackEnemy = 0.5f;

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemy)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, go.transform.position)) <= 2f)
            {
                if (go.layer == 21)
                {
                    EnemyTurtle enemyTurtle = go.GetComponent<EnemyTurtle>();
                    Vector2 rotation = enemyTurtle.transform.localScale;
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

                    enemyTurtle.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 18)
                {
                    EnemyMushroom enemyMushroom = go.GetComponent<EnemyMushroom>();
                    Vector2 rotation = enemyMushroom.transform.localScale;
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
                    enemyMushroom.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 19)
                {
                    EnemySnake enemySnake = go.GetComponent<EnemySnake>();
                    Vector2 rotation = enemySnake.transform.localScale;
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
                    enemySnake.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 20)
                {
                    EnemySlime enemySlime = go.GetComponent<EnemySlime>();
                    Vector2 rotation = enemySlime.transform.localScale;
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
                    enemySlime.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if (go.layer == 17)
                {
                    EnemyBird enemyBird = go.GetComponent<EnemyBird>();
                    Vector2 rotation = enemyBird.transform.localScale;
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
                    enemyBird.currentHealth -= dameArmPlayerAttackEnemy;
                }

            }
        }

    }


    private void OnEnable()
    {
        attackInput = playerInput.Player.Attack;
        attackInput.Enable();
    }

    private void OnDisable()
    {
        attackInput = playerInput.Player.Attack;
        attackInput.Disable();
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
