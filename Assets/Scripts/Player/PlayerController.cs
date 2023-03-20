using System.Collections;
using System.Collections.Generic;
using Model;
using ScreenManager.Popups;
using ScreenManager.Screens;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Animator anim;

    [SerializeField] public Joystick joystick;

    [SerializeField] public  GameObject  knife;
    [SerializeField] public  GameObject  gun;
    [SerializeField] private GunRotation gunRotation;

    private int maxHealth = 1000;
    private int currentHealth;
    private int reviveTime = 5;

    private int stateWeapon = 1;

    private MyPlayerActions playerInput;
    private InputAction     weaponInput;
    private GameplayScreen  gameplayScreen;

    public int ReviveTime
    {
        get => this.reviveTime;
        set
        {
            this.gameplayScreen.ReviveTime = this.reviveTime = value;
            this.UpdatePlayerLocalData();
        }
    }

    public int CurrentHealth
    {
        get => this.currentHealth;
        set => this.gameplayScreen.HealthPercent = (float)(this.currentHealth = value) / this.maxHealth;
    }

    private void Awake()
    {
        playerInput     = new MyPlayerActions();
        this.reviveTime = PlayerLocalData.Instance.CurrentPlayerReviveTime;
    }

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        this.gameplayScreen = ScreenManager.ScreenManager.Instance.GetScreen<GameplayScreen>();
        this.ReHealth();
    }

    private void Update()
    {
        updateWeapon();
    }

    public void UpdatePlayerLocalData()
    {
        PlayerLocalData.Instance.CurrentPlayerReviveTime = this.ReviveTime;
        PlayerLocalData.Instance.Save();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("current Health" + currentHealth);
        this.CurrentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            if (this.ReviveTime > 0)
            {
                this.gameplayScreen.ScreenManager.OpenScreen<DeadPopup>(this);
            }
            else
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    public void ReHealth()
    {
        this.CurrentHealth = this.maxHealth;
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