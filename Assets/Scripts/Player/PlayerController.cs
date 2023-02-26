using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public GameObject knife;
    [SerializeField]
    public GameObject gun;

    private int stateWeapon = 1;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        updateWeapon();
    }
    

    private void updateWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (stateWeapon)
            {
                case 0:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(false);
                    stateWeapon = 1;
                    break;
                case 1:
                    knife.gameObject.SetActive(true);
                    gun.gameObject.SetActive(false);
                    stateWeapon = 2;
                    break;
                case 2:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(true);
                    stateWeapon = 0;
                    break;
                default:
                    knife.gameObject.SetActive(false);
                    gun.gameObject.SetActive(false);
                    stateWeapon = 0; 
                    break;
            }
        }
        anim.SetBool("isKnife", knife.gameObject.activeSelf);
        anim.SetBool("isGun", gun.gameObject.activeSelf);
    }
}
