using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    private float dameArm = 0.2f;
    private float dameGun = 0.25f;
    private float dameKnife = 0.5f;
     
    public float GetDameArm()
    {
        return dameArm;
    }

    public float GetDameGun()
    {
        return dameGun;
    }

    public float GetDameKnife()
    {
        return dameKnife;
    }

    public void SetDameArm(float dameArm)
    {
        this.dameArm = dameArm;
    }

    public void SetDameGun(float dameGun)
    {
        this.dameGun = dameGun;
    }

    public void SetDameKnife(float dameKnife)
    {
        this.dameKnife = dameKnife;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}