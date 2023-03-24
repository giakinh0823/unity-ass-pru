using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healbar : MonoBehaviour
{
    public Slider slider;

    public float Percent
    {
        get => this.slider.value;
        set => this.slider.value = value;
    }
}
