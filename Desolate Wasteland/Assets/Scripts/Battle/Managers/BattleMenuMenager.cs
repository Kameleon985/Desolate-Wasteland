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

    BaseUnit[] arr;
    public Queue<BaseUnit> initQueue;

    int queueCount;
    BaseUnit recentlyKilled;
    bool flag;

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

        List<BaseUnit> unitList = new List<BaseUnit>();
        foreach(BaseHero bh in heroList)
        {
            unitList.Add(bh);
        }
        foreach (BaseEnemy be in enemyList)
        {
            unitList.Add(be);
        }

        queueCount = unitList.Count;

        arr = unitList.ToArray();

        var queue = initiativQueue.GetComponentsInChildren<Image>();

        List<int> unitIndex = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
            switch (arr[i])
            {
                case MeleeUnit mu:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is MeleeUnit)].getInitiative());
                    break;
                case RangedUnit ru:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is RangedUnit)].getInitiative());
                    break;
                case EliteUnit eu:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is EliteUnit)].getInitiative());
                    break;
                case MeleeEnemy me:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is MeleeEnemy)].getInitiative());
                    break;
                case RangeEnemy re:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is RangeEnemy)].getInitiative());
                    break;
                case EliteEnemy ee:
                    arr[i].setInitiative(unitList[unitList.FindIndex(r => r is EliteEnemy)].getInitiative());
                    break;
            }
            }

            arr = BoubleSort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (i < arr.Length - 1)
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

            for (int i = 0; i < queue.Length; i++)
            {
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



        if (flag)
        {        
            List<BaseUnit> tmp = new List<BaseUnit>(initQueue.ToArray());
            int index = tmp.IndexOf(recentlyKilled);

            if(index <= 0)
            {
                flag = false;
                return;
            }

            tmp.Remove(recentlyKilled);
            initQueue = new Queue<BaseUnit>(tmp.ToArray());

            Debug.Log("index: " + index);
            queue[index - 1].sprite = queue[index].sprite;
            queue[index - 1].color = queue[index].color;

            for (int i = index; i < 4; i++)
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

            flag = false;
        }
        else
        {

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

        flag = false;
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

    public void UnitKilled(BaseUnit unit)
    {
        Debug.Log("Unit died: " + unit);
        recentlyKilled = unit;
        flag = true;
    }
}
