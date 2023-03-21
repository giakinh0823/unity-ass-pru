using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    private float dameGun = 0.25f;


    public float GetDameGun()
    {
        return dameGun;
    }


    public void SetDameGun(float dameGun)
    {
        this.dameGun = dameGun;
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
