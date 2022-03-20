using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuMenager : MonoBehaviour
{
    public static BattleMenuMenager instance;

    public GameObject selectedHeroObject;
    public GameObject tileObject;
    public GameObject tileUnitObject;
    

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
}
