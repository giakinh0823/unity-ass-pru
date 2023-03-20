using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTimeController : MonoBehaviour
{
	[SerializeField]
	private PlayerController playerController;

	private int damage = 100;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Timer")
		{
			Destroy(collision.gameObject);
		}
	}
}
