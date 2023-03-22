using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class QuestPlayerController : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] private TextMeshProUGUI mushRoom;
    [SerializeField] private TextMeshProUGUI snake;
    [SerializeField] private TextMeshProUGUI slime;
    [SerializeField] private TextMeshProUGUI turtle;
    [SerializeField] private TextMeshProUGUI bird;
    [SerializeField] private TextMeshProUGUI coin;

    private Dictionary<Layers, int> questEnemy;
    private int                     questCoin;

    public int QuestMushroom
    {
        get => this.questEnemy[Layers.Mushroom];
        set
        {
            if (value < 0) return;
            this.mushRoom.text               = value.ToString();
            this.mushRoom.color              = value <= 0 ? Color.green : Color.red;
            this.questEnemy[Layers.Mushroom] = value;
        }
    }

    public int QuestSnake
    {
        get => this.questEnemy[Layers.Snake];
        set
        {
            if (value < 0) return;
            this.snake.text               = value.ToString();
            this.snake.color              = value <= 0 ? Color.green : Color.red;
            this.questEnemy[Layers.Snake] = value;
        }
    }

    public int QuestSlime
    {
        get => this.questEnemy[Layers.Slime];
        set
        {
            if (value < 0) return;
            this.slime.text               = value.ToString();
            this.slime.color              = value <= 0 ? Color.green : Color.red;
            this.questEnemy[Layers.Slime] = value;
        }
    }

    public int QuestTurtle
    {
        get => this.questEnemy[Layers.Turtle];
        set
        {
            if (value < 0) return;
            this.turtle.text               = value.ToString();
            this.turtle.color              = value <= 0 ? Color.green : Color.red;
            this.questEnemy[Layers.Turtle] = value;
        }
    }

    public int QuestBird
    {
        get => this.questEnemy[Layers.Bird];
        set
        {
            if (value < 0) return;
            this.bird.text               = value.ToString();
            this.bird.color              = value <= 0 ? Color.green : Color.red;
            this.questEnemy[Layers.Bird] = value;
        }
    }

    public int QuestCoin
    {
        get => this.questCoin;
        set
        {
            if (value < 0) return;
            this.coin.text  = value.ToString();
            this.coin.color = value <= 0 ? Color.green : Color.red;
            this.questCoin  = value;
        }
    }

    public bool IsReadyToUse => this.questEnemy is not null;

    public bool IsQuestCompleted => this.questEnemy.Values.All(x => x <= 0) && this.questCoin <= 0;

    #endregion

    private IEnumerator InitData()
    {
        var levelGeneration = FindObjectOfType<LevelGeneration>();

        yield return new WaitUntil(() => levelGeneration.stopGeneration);

        (this.questEnemy, this.questCoin) = GenerateQuest();

        this.QuestMushroom = this.questEnemy.ContainsKey(Layers.Mushroom) ? this.questEnemy[Layers.Mushroom] : 0;
        this.QuestSnake    = this.questEnemy.ContainsKey(Layers.Snake) ? this.questEnemy[Layers.Snake] : 0;
        this.QuestSlime    = this.questEnemy.ContainsKey(Layers.Slime) ? this.questEnemy[Layers.Slime] : 0;
        this.QuestTurtle   = this.questEnemy.ContainsKey(Layers.Turtle) ? this.questEnemy[Layers.Turtle] : 0;
        this.QuestBird     = this.questEnemy.ContainsKey(Layers.Bird) ? this.questEnemy[Layers.Bird] : 0;
        this.QuestCoin     = this.questCoin;
    }

    private void Start()
    {
        this.StartCoroutine(this.InitData());
    }

    private static (Dictionary<Layers, int> questEnemy, int questCoin) GenerateQuest()
    {
        var currentLevel = PlayerLocalData.Instance.CurrentPlayerLevel;
        var random       = new Random();

        var layerToCount = CalculateEnemyLayerToCount();
        var coinCount    = GetCoinCount();
        var totalEnemy   = layerToCount.Values.Sum();

        // quest amount config
        const int minKilledEnemy   = 3;
        var       killedEnemyCount = Math.Clamp(minKilledEnemy + currentLevel - 1, minKilledEnemy, totalEnemy);

        var questEnemy = layerToCount.ToDictionary(x => x.Key, x => 0);

        for (var i = 0; i < killedEnemyCount;)
        {
            var randomLayer = layerToCount.Keys.ElementAt(random.Next(layerToCount.Count));
            if (layerToCount[randomLayer] == 0) continue;
            questEnemy[randomLayer]   += 1;
            layerToCount[randomLayer] -= 1;
            i++;
        }

        // lượng coin cần kiếm được sẽ random từ 0 đến 1/2 số coin hiện có trong map
        return (questEnemy, random.Next(coinCount) % (coinCount / 2 == 0 ? 1 : coinCount / 2));
    }

    private static Dictionary<Layers, int> CalculateEnemyLayerToCount()
    {
        var enemy = GameObject.FindGameObjectsWithTag("Enemy");

        var result = new Dictionary<Layers, int>();
        foreach (var go in enemy)
        {
            var layer = (Layers)go.layer;
            if (result.ContainsKey(layer))
                result[layer] += 1;
            else
                result.Add(layer, 1);
        }

        return result;
    }

    private static int GetCoinCount()
    {
        var coins = GameObject.FindGameObjectsWithTag("Coin");

        return coins.Length;
    }
}