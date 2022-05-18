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
        //List<BaseEnemy> enemyList = UnitManager.Instance.enemyList;
        //List<BaseHero> heroList = UnitManager.Instance.heroList;

        var queue = initiativQueue.GetComponentsInChildren<Image>();

        queue[0].sprite = meleeImg;
        queue[0].color = Color.blue;
        queue[1].sprite = meleeImg;
        queue[1].color = Color.red;
        queue[2].sprite = rangeImg;
        queue[2].color = Color.blue;
        queue[3].sprite = rangeImg;
        queue[3].color = Color.red;
        queue[4].sprite = eliteImg;
        queue[4].color = Color.blue;

        BaseUnit[] arr = { new MeleeUnit(), new RangedUnit(), new EliteUnit(), new MeleeEnemy(), new RangeEnemy(), new EliteEnemy() };

        int n = arr.Length;
        for(int i = 0; i < n - 1 ; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if(arr[j].getInitiative() > arr[ j + 1].getInitiative())
                {
                    BaseUnit tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                }
            }
        }

        System.Array.Reverse(arr);

        List<BaseUnit> initInitiative = arr.ToList();

        for (int i = 0 ; i < queue.Length ; i++)
        {
            
        }
    }

    public void updateQueue()
    {
        var queue = initiativQueue.GetComponentsInChildren<Image>();

        for (int i = 0 ; i < 4 ; i++)
        {
            queue[i].sprite = queue[i + 1].sprite;
            queue[i].color = queue[i + 1].color;
        }

        int rn = Random.Range(1, 4);
       

        if (count % 2 == 0) {
            switch (rn)
            {
                case 1:
                    queue[4].sprite = meleeImg;
                    queue[4].color = Color.blue;
                    break;
                case 2:
                    queue[4].sprite = rangeImg;
                    queue[4].color = Color.blue;
                    break;
                case 3:
                    queue[4].sprite = eliteImg;
                    queue[4].color = Color.blue;
                    break;
            }
        }
        else
        {
            switch (rn)
            {
                case 1:
                    queue[4].sprite = meleeImg;
                    queue[4].color = Color.red;
                    break;
                case 2:
                    queue[4].sprite = rangeImg;
                    queue[4].color = Color.red;
                    break;
                case 3:
                    queue[4].sprite = meleeImg;
                    queue[4].color = Color.red;
                    break;
            }
        }

        count++;
    }
}
