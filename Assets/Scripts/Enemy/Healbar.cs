using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healbar : MonoBehaviour
{
    public Vector3 localScale;

    void Start()
    {
        localScale = transform.localScale;
    }

    void Update()
    {
        transform.localScale = localScale;
    }
}
