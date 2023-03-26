using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected QuestPlayerController QuestPlayerController => FindObjectOfType<QuestPlayerController>();
    private   float                 dameGun = 0.25f;


    public float GetDameGun()
    {
        return Mathf.Clamp(dameGun - PlayerLocalData.Instance.CurrentPlayerLevel * 0.01f, 0.1f, 0.25f) + PlayerLocalData.Instance.BuffGunDamage;
    }


    public void SetDameGun(float dameGun)
    {
        this.dameGun = dameGun;
    }
}