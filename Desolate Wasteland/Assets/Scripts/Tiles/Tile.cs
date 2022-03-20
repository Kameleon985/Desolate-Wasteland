using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer;
    public GameObject highlight;
    public string tileName;

    public bool isWakable;

    public BaseUnit OccupiedUnit;
    public bool isWalkableFinal => isWakable && OccupiedUnit == null;

    public virtual void init(int x, int y)
    {
     
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        BattleMenuMenager.instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        BattleMenuMenager.instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        if (BattleMenager.instance.gameState != GameState.HeroesTurn) return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHero != null && isWakable)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.occupiedTile = this;
    }
}
