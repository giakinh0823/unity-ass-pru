using System.Collections;
using System.Collections.Generic;
using Common;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectionItemController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float            _collectionTime = 10f;
    [SerializeField] private AudioSource      audioTimer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 13)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Timer"))
        {
            CountDownTimer countDowObjects = FindObjectOfType<CountDownTimer>();
            countDowObjects.TimeLeft += _collectionTime;
            this.audioTimer.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            PlayerLocalData.Instance.CurrentCoin += 1;
            PlayerLocalData.Instance.Save();
            this.audioTimer.Play();
            Destroy(collision.gameObject);

            var questPlayerController = FindObjectOfType<QuestPlayerController>();
            if (questPlayerController is { IsReadyToUse: true }) questPlayerController.QuestCoin--;
        }
    }
}