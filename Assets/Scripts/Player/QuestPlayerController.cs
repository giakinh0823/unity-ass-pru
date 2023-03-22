using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class QuestPlayerController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI mushRoom;
	[SerializeField] private TextMeshProUGUI snake;
	[SerializeField] private TextMeshProUGUI slime;
	[SerializeField] private TextMeshProUGUI turtle;
	[SerializeField] private TextMeshProUGUI bird;
	[SerializeField] private TextMeshProUGUI coin;
	private bool isInit = false;

	// Start is called before the first frame update
	void Start()
    {

	}

	// Update is called once per frame
	void Update()
	{
		Dictionary<int, int> itemMap = itemsLevel();
		LevelGeneration levelGeneration = FindObjectOfType<LevelGeneration>();

		if (levelGeneration.stopGeneration && !isInit)
		{
			itemMap = itemsLevel();
			if (itemMap != null && itemMap.Count > 0)
			{
				bird.text = itemMap.ContainsKey(17) ? itemMap[17].ToString() : "0";
				mushRoom.text = itemMap.ContainsKey(18) ? itemMap[18].ToString() : "0";
				snake.text = itemMap.ContainsKey(19) ? itemMap[19].ToString() : "0";
				slime.text = itemMap.ContainsKey(20) ? itemMap[20].ToString() : "0";
				turtle.text = itemMap.ContainsKey(21) ? itemMap[21].ToString() : "0";
				coin.text = itemMap.ContainsKey(0) ? itemMap[0].ToString() : "0";
				isInit = true;
			}
		}
	}

	private Dictionary<int, int> itemsLevel()
	{
		int levelCurrent = PlayerLocalData.Instance.CurrentPlayerLevel;
		Debug.Log(levelCurrent.ToString());
		System.Random random = new System.Random();

		Dictionary<int, int> itemMapsLevel = items();
		Dictionary<int, int> levelEnemies = new Dictionary<int, int>();
		//int numEnemies = 1;
		int initNumEnemies = 3;
		int numEnemiesToGet = initNumEnemies + levelCurrent - 1;
		levelEnemies.Clear();
		while (numEnemiesToGet > 0)
		{
			int enemy = itemMapsLevel.Keys.ElementAt(random.Next(itemMapsLevel.Count)); 
			//int numEnemy = Math.Min(numEnemies, itemMapsLevel[enemy]);
			if (levelEnemies.ContainsKey(enemy)) 
			{
				levelEnemies[enemy] += 1;
			}
			else
			{
				levelEnemies.Add(enemy, 1); 
			}
			numEnemiesToGet -= 1;
		}
		return levelEnemies;
	}

	private Dictionary<int, int> items()
	{
		GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

		Dictionary<int, int> map = new Dictionary<int, int>();
		foreach (GameObject go in enemy)
		{
			if (go.layer == 17)
			{
				if (!map.ContainsKey(17))
				{
					map.Add(17, 1);
				}
				else
				{
					map[17] += 1;
				}
			}

			if (go.layer == 18)
			{
				if (!map.ContainsKey(18))
				{
					map.Add(18, 1);
				}
				else
				{
					map[18] += 1;
				}
			}

			if (go.layer == 19)
			{
				if (!map.ContainsKey(19))
				{
					map.Add(19, 1);
				}
				else
				{
					map[19] += 1;
				}
			}

			if (go.layer == 20)
			{
				if (!map.ContainsKey(20))
				{
					map.Add(20, 1);
				}
				else
				{
					map[20] += 1;
				}

			}

			if (go.layer == 21)
			{
				if (!map.ContainsKey(21))
				{
					map.Add(21, 1);
				}
				else
				{
					map[21] += 1;
				}
			}
		}

		map.Add(0, coins.Count());

		return map;
	}

}
