using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class QuestPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		
	}

    private Dictionary<int, int> items()
    {
	    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
	    GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

	    Dictionary<int, int> map = new Dictionary<int, int>();
	    

		map.Add(0, coins.Count());

	    return map;
    }

}
