using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class CollectionTimeController : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Timer")
		{

			CountDownTimer countDowObjects = FindObjectOfType<CountDownTimer>();
			countDowObjects.TimeLeft+= 30f;
			Destroy(collision.gameObject);
		}
	}
}
