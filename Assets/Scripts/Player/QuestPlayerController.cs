using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
	    LevelGeneration levelGeneration = FindObjectOfType<LevelGeneration>();
	    if (levelGeneration.stopGeneration)
	    {
			Dictionary<int, int> itemMap = items();

			bird.text = itemMap[17].ToString();
			mushRoom.text = itemMap[18].ToString();
			snake.text = itemMap[19].ToString();
			slime.text = itemMap[20].ToString();
			turtle.text = itemMap[21].ToString();
			coin.text = itemMap[0].ToString();

	    }
		
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
