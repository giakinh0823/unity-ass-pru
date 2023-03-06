using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Animator anim;

    [SerializeField]
    public Joystick joystick;

    [SerializeField]
    public GameObject knife;
    [SerializeField]
    public GameObject gun;
    [SerializeField]
    private GunRotation gunRotation;

    private int maxHealth = 1000;
    private int currentHealth;

    public HealthBarPlayer healthBarPlayer;
    private int stateWeapon = 1;

    private MyPlayerActions playerInput;
    private InputAction weaponInput;

    private void Awake()
    {
        playerInput = new MyPlayerActions();
    }


    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        currentHealth = maxHealth;
        healthBarPlayer.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        updateWeapon();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("current Health"+ currentHealth);
        currentHealth -= damage;
        healthBarPlayer.SetHealth(currentHealth);
    }
    

    private void updateWeapon()
    {
        if (weaponInput.triggered)
        {
            switch (stateWeapon)
            {
                case 0:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(false);
                    gunRotation.gunSprite.SetActive(false);
                    stateWeapon = 1;
                    break;
                case 1:
                    knife.gameObject.SetActive(true);
                    gun.gameObject.SetActive(false);
                    gunRotation.gunSprite.SetActive(false);
                    stateWeapon = 2;
                    break;
                case 2:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(true);
                    gunRotation.gunSprite.SetActive(true);
                    stateWeapon = 0;
                    break;
                default:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(false);
                    gunRotation.gunSprite.SetActive(false);
                    stateWeapon = 0; 
                    break;
            }
        }
        anim.SetBool("isKnife", knife.gameObject.activeSelf);
        anim.SetBool("isGun", gun.gameObject.activeSelf);
    }

    private void OnEnable()
    {
        weaponInput = playerInput.Player.Weapon;
        weaponInput.Enable();
    }

    private void OnDisable()
    {
        weaponInput = playerInput.Player.Weapon;
        weaponInput.Disable();
    }
}
