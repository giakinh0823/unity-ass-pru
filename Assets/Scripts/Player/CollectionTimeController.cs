using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class CollectionTimeController : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	[SerializeField] private float _collectionTime = 10f;
	[SerializeField] private AudioSource audioSource;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Timer")
		{

			CountDownTimer countDowObjects = FindObjectOfType<CountDownTimer>();
			countDowObjects.TimeLeft+= _collectionTime;

			audioSource.Play();
			Destroy(collision.gameObject);
		} else audioSource.Stop();
	}
}
