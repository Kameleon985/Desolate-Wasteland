using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuMenager : MonoBehaviour
{
    public static BattleMenuMenager instance;

    public GameObject selectedHeroObject;
    public GameObject tileObject;
    public GameObject tileUnitObject;
    public GameObject currentGameSatteObject;

    public GameObject endPrepareHeroes;
    public GameObject endSteroids;
    public GameObject endSteroidsButton;

    public GameObject initiativQueue;

    public Sprite meleeImg;
    public Sprite rangeImg;
    public Sprite eliteImg;

    int count = 1;
    BaseUnit[] arr = { new MeleeUnit(), new RangedUnit(), new EliteUnit(), new MeleeEnemy(), new RangeEnemy(), new EliteEnemy() };
    Queue<BaseUnit> initQueue;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }

        tileObject.GetComponentInChildren<Text>().text = tile.tileName;
        tileObject.SetActive(true);

        if (tile.OccupiedUnit)
        {
            tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.unitName;
            tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if(hero == null)
        {
            selectedHeroObject.SetActive(false);
            return;
        }

        selectedHeroObject.GetComponentInChildren<Text>().text = hero.unitName;
        selectedHeroObject.SetActive(true);
    }

    public void ShowCurrentGameState()
    {
        currentGameSatteObject.GetComponentInChildren<Text>().text = BattleMenager.instance.gameState.ToString();
    }

    public void EndPrepareHeroes()
    {
        endPrepareHeroes.SetActive(false);
        endSteroids.SetActive(false);
        endSteroidsButton.SetActive(false);
        initiativQueue.SetActive(true);

        setInitQueue();

        BattleMenager.instance.ChangeState(GameState.HeroesTurn);
    }

    public void setInitQueue()
    {
        List<BaseEnemy> enemyList = UnitManager.Instance.enemyList;
        List<BaseHero> heroList = UnitManager.Instance.heroList;

        var queue = initiativQueue.GetComponentsInChildren<Image>();

        int[] accualInitiativeArr = { heroList[1].getInitiative(), heroList[2].getInitiative(), heroList[0].getInitiative(), enemyList[1].getInitiative(), enemyList[2].getInitiative(), enemyList[0].getInitiative() };

        for (int i = 0 ; i < arr.Length ; i++)
        {
            arr[i].setInitiative(accualInitiativeArr[i]);
        }

        arr = BoubleSort(arr);

        for (int i = 0; i < arr.Length; i++)
        {
            if (i < 5)
            {
                if (arr[i].getInitiative() == arr[i + 1].getInitiative())
                {
                    int rn = Random.Range(1, 3);
                    if (rn == 1)
                    {
                        BaseUnit tmp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = tmp;
                    }
                }
            }
        }

        initQueue = new Queue<BaseUnit>(arr);

        for (int i = 0; i < queue.Length; i++) {
            switch (initQueue.Dequeue())
            {
                case MeleeUnit mu:
                    queue[i].sprite = meleeImg;
                    queue[i].color = Color.blue;
                    initQueue.Enqueue(mu);
                    break;
                case RangedUnit ru:
                    queue[i].sprite = rangeImg;
                    queue[i].color = Color.blue;
                    initQueue.Enqueue(ru);
                    break;
                case EliteUnit eu:
                    queue[i].sprite = eliteImg;
                    queue[i].color = Color.blue;
                    initQueue.Enqueue(eu);
                    break;
                case MeleeEnemy me:
                    queue[i].sprite = meleeImg;
                    queue[i].color = Color.red;
                    initQueue.Enqueue(me);
                    break;
                case RangeEnemy re:
                    queue[i].sprite = rangeImg;
                    queue[i].color = Color.red;
                    initQueue.Enqueue(re);
                    break;
                case EliteEnemy ee:
                    queue[i].sprite = eliteImg;
                    queue[i].color = Color.red;
                    initQueue.Enqueue(ee);
                    break;
            }
        }
    }

    public void updateQueue()
    {
        var queue = initiativQueue.GetComponentsInChildren<Image>();

        for (int i = 0; i < 4; i++)
        {
            queue[i].sprite = queue[i + 1].sprite;
            queue[i].color = queue[i + 1].color;
        }

        switch (initQueue.Dequeue())
        {
            case MeleeUnit mu:
                queue[4].sprite = meleeImg;
                queue[4].color = Color.blue;
                initQueue.Enqueue(mu);
                break;
            case RangedUnit ru:
                queue[4].sprite = rangeImg;
                queue[4].color = Color.blue;
                initQueue.Enqueue(ru);
                break;
            case EliteUnit eu:
                queue[4].sprite = eliteImg;
                queue[4].color = Color.blue;
                initQueue.Enqueue(eu);
                break;
            case MeleeEnemy me:
                queue[4].sprite = meleeImg;
                queue[4].color = Color.red;
                initQueue.Enqueue(me);
                break;
            case RangeEnemy re:
                queue[4].sprite = rangeImg;
                queue[4].color = Color.red;
                initQueue.Enqueue(re);
                break;
            case EliteEnemy ee:
                queue[4].sprite = eliteImg;
                queue[4].color = Color.red;
                initQueue.Enqueue(ee);
                break;
        }
    }

    BaseUnit[] BoubleSort(BaseUnit[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j].getInitiative() > arr[j + 1].getInitiative())
                {
                    BaseUnit tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                }
            }
        }

        System.Array.Reverse(arr);

        return arr;
    }
}
