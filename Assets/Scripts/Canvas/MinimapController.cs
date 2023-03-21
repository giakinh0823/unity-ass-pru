using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject showMap;
    private Animator animShowMap;

    private bool isShowMap;

    void Start()
    {
        isShowMap = false;
        animShowMap = showMap.GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isShowMap = !isShowMap;
        if (isShowMap)
        {
            showMap.SetActive(true);
            animShowMap.SetTrigger("openShowMap");
        }
        else
        {
            Debug.Log("closeShowMap");
            animShowMap.SetTrigger("closeShowMap");
        }
    }
}
