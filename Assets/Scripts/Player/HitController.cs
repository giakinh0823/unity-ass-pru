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


    private int hit;

    private MyPlayerActions playerInput;
    private InputAction attack;

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
        if (attack.triggered)
        {
            if (hit >= 2 && !slidingController.isWallSliding)
            {
                playerController.anim.SetTrigger("isLongHit");
                hit = 0;
            }
            else if(!slidingController.isWallSliding)
            {
                playerController.anim.SetTrigger("isShortHit");
                hit++;
            }
        }
    }

    private void OnEnable()
    {
        attack = playerInput.Player.Attack;
        attack.Enable();
    }

    private void OnDisable()
    {
        attack = playerInput.Player.Attack;
        attack.Disable();
    }
}
