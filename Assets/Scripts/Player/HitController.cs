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

    public void AttackEnemy()
    {
    float dameArmPlayerAttackEnemy = 0.2f;

    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemy)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, go.transform.position)) <= 2f)
            {
                if(go.layer== 21) {
                    EnemyTurtle enemyTurtle = go.GetComponent<EnemyTurtle>();
                    enemyTurtle.currentHealth -= dameArmPlayerAttackEnemy;
                }
                else if(go.layer== 18)
                {
                    EnemyMushroom enemyMushroom = go.GetComponent<EnemyMushroom>();
                    enemyMushroom.currentHealth -= dameArmPlayerAttackEnemy;
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
