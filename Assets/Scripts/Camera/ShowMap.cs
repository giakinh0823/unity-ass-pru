using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject showMap;

    private bool isShowMap;

    void Start()
    {
        isShowMap = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isShowMap = !isShowMap;
        showMap.SetActive(isShowMap);
    }
}
