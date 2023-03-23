using System.Collections;
using System.Collections.Generic;
using Common;
using Constant;
using Model;
using ScreenManager.Popups;
using ScreenManager.Screens;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private int reviveTime = 5;

    private int stateWeapon = 1;

    private GameplayScreen gameplayScreen;

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

        FindObjectOfType<InputManager>().changeWeapon += this.updateWeapon;
    }

    private void Update()
    {
        anim.SetBool("isKnife", knife.gameObject.activeSelf);
        anim.SetBool("isGun", gun.gameObject.activeSelf);
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
        switch (stateWeapon)
        {
            case 0:
                knife.gameObject.SetActive(false);
                gun.gameObject.SetActive(false);
                gunRotation.gunSprite.SetActive(false);
                stateWeapon = StateWeaponConstant.KNIFE;
                break;
            case 1:
                knife.gameObject.SetActive(true);
                gun.gameObject.SetActive(false);
                gunRotation.gunSprite.SetActive(false);
                stateWeapon = StateWeaponConstant.GUN;
                break;
            case 2:
                knife.gameObject.SetActive(false);
                gun.gameObject.SetActive(true);
                gunRotation.gunSprite.SetActive(true);
                stateWeapon = StateWeaponConstant.HAND;
                break;
            default:
                knife.gameObject.SetActive(false);
                gun.gameObject.SetActive(false);
                gunRotation.gunSprite.SetActive(false);
                stateWeapon = 0;
                break;
        }
    }
}