using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healbar4 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 localScale;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = Enemy4.currentHealth;
        transform.localScale = localScale;
    }
}
