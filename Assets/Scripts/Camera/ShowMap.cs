using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private GameObject showMap;

    private bool isShowMap;

    void Start()
    {
        isShowMap = false;
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        isShowMap = !isShowMap;
        showMap.SetActive(isShowMap);
    }
}
