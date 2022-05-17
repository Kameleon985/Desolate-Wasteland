using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer;
    public GameObject highlight;
    public GameObject pathHighlight;
    public GameObject rangeHighlight;
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

    public GameObject notClickableThrough;


    public virtual void init(int x, int y, GameObject notClickableThrough)
    {
        this.notClickableThrough = notClickableThrough;
    }

    private void OnMouseEnter()
    {
        if (notClickableThrough.activeSelf)
        {
            Debug.Log("D: " + notClickableThrough.activeSelf);
        }
        else
        {
            Debug.Log("DX: " + notClickableThrough.activeSelf);
            highlight.SetActive(true);
            BattleMenuMenager.instance.ShowTileInfo(this);
        }
        
    }

    private void OnMouseExit()
    {
        if (notClickableThrough.activeSelf)
        {
            //Do nothing
        }
        else
        {
            highlight.SetActive(false);
            BattleMenuMenager.instance.ShowTileInfo(null);
        }
        
    }

    private void OnMouseDown()
    {
        if (notClickableThrough.activeSelf)
        {
            //Do nothing
        }
        else
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
                        if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().sprite == BattleMenuMenager.instance.meleeImg)
                        {
                            if (OccupiedUnit is MeleeUnit)
                            {
                                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                            }
                        }
                        else if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().sprite == BattleMenuMenager.instance.rangeImg)
                        {
                            if (OccupiedUnit is RangedUnit)
                            {
                                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                            }
                        }
                        if (UnitManager.Instance.SelectedHero is RangedUnit)
                        {
                            var rangeHero = (RangedUnit)UnitManager.Instance.SelectedHero;
                            for (int i = 0; i < GridManager.Instance.height; i++)
                            {
                                if (rangeHero.occupiedTile.x >= rangeHero.attackRange)
                                {
                                    if (rangeHero.occupiedTile.x + rangeHero.attackRange >= GridManager.Instance.width)
                                    {
                                        GridManager.Instance.GetTileAtPosition(new Vector2(rangeHero.occupiedTile.x - rangeHero.attackRange, i)).rangeHighlight.SetActive(true);
                                    }
                                    else
                                    {
                                        GridManager.Instance.GetTileAtPosition(new Vector2(rangeHero.occupiedTile.x + rangeHero.attackRange, i)).rangeHighlight.SetActive(true);
                                        GridManager.Instance.GetTileAtPosition(new Vector2(rangeHero.occupiedTile.x - rangeHero.attackRange, i)).rangeHighlight.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GridManager.Instance.GetTileAtPosition(new Vector2(rangeHero.occupiedTile.x + rangeHero.attackRange, i)).rangeHighlight.SetActive(true);
                                }
                            }
                        }
                        else if (UnitManager.Instance.SelectedHero is MeleeUnit)
                        {
                            GridManager.Instance.ClearAllHighlightTiles();
                        }
                    }
                    else
                    {
                        if (UnitManager.Instance.SelectedHero != null)
                        {
                            //Attack
                            var enemy = (BaseEnemy)OccupiedUnit;

                            if (UnitManager.Instance.SelectedHero is MeleeUnit)
                            {
                                if (IsNeighborOccupied(UnitManager.Instance.SelectedHero.occupiedTile))
                                {
                                    var heroUnit = (MeleeUnit)UnitManager.Instance.SelectedHero;
                                    enemy.takeDamage(heroUnit.getAttackDamage());

                                    Debug.Log("attackDmg: " + heroUnit.getAttackDamage());

                                    UnitManager.Instance.SetSelectedHero(null);
                                    BattleMenuMenager.instance.updateQueue();
                                    UnitManager.Instance.EnemyTurn();
                                }
                            }
                            else if (UnitManager.Instance.SelectedHero is RangedUnit)
                            {
                                var heroUnit = (RangedUnit)UnitManager.Instance.SelectedHero;
                                if (enemy.occupiedTile.x >= heroUnit.occupiedTile.x)
                                {
                                    if (enemy.occupiedTile.x <= heroUnit.occupiedTile.x + heroUnit.attackRange)
                                    {
                                        enemy.takeDamage(heroUnit.getAttackDamage());
                                        UnitManager.Instance.SetSelectedHero(null);
                                        BattleMenuMenager.instance.updateQueue();
                                        UnitManager.Instance.EnemyTurn();
                                    }
                                }
                                else if (enemy.occupiedTile.x < heroUnit.occupiedTile.x)
                                {
                                    if (enemy.occupiedTile.x >= heroUnit.occupiedTile.x - heroUnit.attackRange)
                                    {
                                        enemy.takeDamage(heroUnit.getAttackDamage());
                                        UnitManager.Instance.SetSelectedHero(null);
                                        BattleMenuMenager.instance.updateQueue();
                                        UnitManager.Instance.EnemyTurn();
                                    }
                                }

                                Debug.Log("attackDmg: " + heroUnit.getAttackDamage());
                            }
                            GridManager.Instance.ClearAllHighlightTiles();
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
                        GridManager.Instance.ClearAllHighlightTiles();
                        BattleMenuMenager.instance.updateQueue();
                    }
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
