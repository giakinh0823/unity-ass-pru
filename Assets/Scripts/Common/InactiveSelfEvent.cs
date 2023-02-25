using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveSelfEvent : MonoBehaviour
{
    private void InActiveSelf()
    {
        Debug.Log("Inactive Self");
        gameObject.SetActive(false);
    }
}
