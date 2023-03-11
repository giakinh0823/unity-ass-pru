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


    private int hit;

    private MyPlayerActions playerInput;
    private InputAction attackInput;

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

    private void ShootHitGun()
    {
        shootController.Shoot();
    }
}
