using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected QuestPlayerController QuestPlayerController => FindObjectOfType<QuestPlayerController>();
    private   float                 dameGun = 0.25f;


    public float GetDameGun()
    {
        return dameGun;
    }


    public void SetDameGun(float dameGun)
    {
        this.dameGun = dameGun;
    }
}