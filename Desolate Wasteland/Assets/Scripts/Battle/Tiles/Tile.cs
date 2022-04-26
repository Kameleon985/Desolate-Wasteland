using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer;
    public GameObject highlight;
    public GameObject pathHighlight;
    public string tileName;

    public bool isWakable;

    public BaseUnit OccupiedUnit;
    public bool isWalkableFinal => isWakable && OccupiedUnit == null;

    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;

    public Tile previouseTile;

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
        if (BattleMenager.instance.gameState == GameState.PrepareHeroes)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.faction == Faction.Hero)
                {
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                }
            }
            else
            {
                if (UnitManager.Instance.SelectedHero != null && isWakable)
                {
                    if (x < 2)
                    {
                        SetUnit(UnitManager.Instance.SelectedHero);
                        UnitManager.Instance.SetSelectedHero(null);
                    }
                }
            }
        }
        else
        {
            if (BattleMenager.instance.gameState != GameState.HeroesTurn) return;

            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.faction == Faction.Hero)
                {
                    //Select
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedHero != null)
                    {
                        //Attack
                        if (IsNeighborOccupied(UnitManager.Instance.SelectedHero.occupiedTile))
                        {
                            var enemy = (MeleeEnemy)OccupiedUnit;
                            enemy.takeDamage(UnitManager.Instance.SelectedHero.attackDamage);
                            UnitManager.Instance.SetSelectedHero(null);
                            BattleMenager.instance.ChangeState(GameState.EnemiesTurn);
                        }
                    }
                }
            }
            else
            {
                if (UnitManager.Instance.SelectedHero != null && isWakable)
                {
                    //Move
                    UnitManager.Instance.SelectedHero.Move(this);
                    SetUnit(UnitManager.Instance.SelectedHero);
                    UnitManager.Instance.SetSelectedHero(null);
                    GridManager.Instance.ClearAStarTiles();
                    BattleMenager.instance.ChangeState(GameState.EnemiesTurn);
                }
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

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public bool IsNeighborOccupied(Tile tile)
    {
        if (tile.x - 1 >= 0)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y)).OccupiedUnit != null) return true;
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y - 1)).OccupiedUnit != null) return true;
            if (tile.y + 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y + 1)).OccupiedUnit != null) return true;
        }
        if (tile.x + 1 < GridManager.Instance.width)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y)).OccupiedUnit != null) return true;
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y - 1)).OccupiedUnit != null) return true;
            if (tile.y + 1 <= GridManager.Instance.height)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y + 1)).OccupiedUnit != null) return true;
        }
        if (tile.y - 1 >= 0) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y - 1)).OccupiedUnit != null) return true;
        if (tile.y - 1 >= 0) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y + 1)).OccupiedUnit != null) return true;

        return false;
    }
}
