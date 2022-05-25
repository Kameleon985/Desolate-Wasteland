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

    public Vector3 positionV3;


    public virtual void init(int x, int y, GameObject notClickableThrough)
    {
        this.notClickableThrough = notClickableThrough;
        positionV3 = new Vector3(x, y, 0);
    }

    private void OnMouseEnter()
    {
        if (notClickableThrough.activeSelf)
        {
            //Do nothing
        }
        else
        {
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
                        else if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().sprite == BattleMenuMenager.instance.eliteImg)
                        {
                            if (OccupiedUnit is EliteUnit)
                            {
                                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                            }
                        }
                        if (UnitManager.Instance.SelectedHero is RangedUnit)
                        {
                            GridManager.Instance.ClearAllHighlightTiles();
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
                        else if (UnitManager.Instance.SelectedHero is EliteUnit)
                        {
                            GridManager.Instance.ClearAllHighlightTiles();
                            var eliteHero = (EliteUnit)UnitManager.Instance.SelectedHero;
                            if (eliteHero.getAmmo() > 0)
                            {
                                for (int i = 0; i < GridManager.Instance.height; i++)
                                {
                                    if (eliteHero.occupiedTile.x >= eliteHero.attackRange)
                                    {
                                        if (eliteHero.occupiedTile.x + eliteHero.attackRange >= GridManager.Instance.width)
                                        {
                                            GridManager.Instance.GetTileAtPosition(new Vector2(eliteHero.occupiedTile.x - eliteHero.attackRange, i)).rangeHighlight.SetActive(true);
                                        }
                                        else
                                        {
                                            GridManager.Instance.GetTileAtPosition(new Vector2(eliteHero.occupiedTile.x + eliteHero.attackRange, i)).rangeHighlight.SetActive(true);
                                            GridManager.Instance.GetTileAtPosition(new Vector2(eliteHero.occupiedTile.x - eliteHero.attackRange, i)).rangeHighlight.SetActive(true);
                                        }
                                    }
                                    else
                                    {
                                        GridManager.Instance.GetTileAtPosition(new Vector2(eliteHero.occupiedTile.x + eliteHero.attackRange, i)).rangeHighlight.SetActive(true);
                                    }
                                }
                            }
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
                                var occupiedNeighbours = NeighborOccupied(UnitManager.Instance.SelectedHero.occupiedTile);

                                if (occupiedNeighbours != null)
                                {
                                    var heroUnit = (MeleeUnit)UnitManager.Instance.SelectedHero;
                                    foreach (Tile tile in occupiedNeighbours)
                                    {

                                        if (enemy.occupiedTile.x == tile.x && enemy.occupiedTile.y == tile.y)
                                        {
                                            enemy.takeDamage(heroUnit.getAttackDamage());
                                            UnitManager.Instance.SetSelectedHero(null);
                                            BattleMenuMenager.instance.updateQueue();
                                            //UnitManager.Instance.EnemyTurn();
                                            if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                            {
                                                UnitManager.Instance.EnemyTurn();
                                            }
                                            break;
                                        }
                                    }
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
                                        //UnitManager.Instance.EnemyTurn();
                                        if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                        {
                                            UnitManager.Instance.EnemyTurn();
                                        }
                                    }
                                }
                                else if (enemy.occupiedTile.x < heroUnit.occupiedTile.x)
                                {
                                    if (enemy.occupiedTile.x >= heroUnit.occupiedTile.x - heroUnit.attackRange)
                                    {
                                        enemy.takeDamage(heroUnit.getAttackDamage());
                                        UnitManager.Instance.SetSelectedHero(null);
                                        BattleMenuMenager.instance.updateQueue();
                                        //UnitManager.Instance.EnemyTurn();
                                        if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                        {
                                            UnitManager.Instance.EnemyTurn();
                                        }
                                    }
                                }

                                //Debug.Log("attackDmg: " + heroUnit.getAttackDamage());
                            }
                            else if (UnitManager.Instance.SelectedHero is EliteUnit)
                            {
                                GridManager.Instance.ClearAllHighlightTiles();
                                var heroUnit = (EliteUnit)UnitManager.Instance.SelectedHero;
                                if (heroUnit.getAmmo() <= 0)
                                {
                                    var occupiedNeighbours = NeighborOccupied(UnitManager.Instance.SelectedHero.occupiedTile);

                                    if (occupiedNeighbours != null)
                                    {
                                        var eliteUnit = (EliteUnit)UnitManager.Instance.SelectedHero;
                                        foreach (Tile tile in occupiedNeighbours)
                                        {

                                            if (enemy.occupiedTile.x == tile.x && enemy.occupiedTile.y == tile.y)
                                            {
                                                enemy.takeDamage(eliteUnit.getAttackDamage());
                                                UnitManager.Instance.SetSelectedHero(null);
                                                BattleMenuMenager.instance.updateQueue();
                                                //UnitManager.Instance.EnemyTurn();
                                                if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                                {
                                                    UnitManager.Instance.EnemyTurn();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (enemy.occupiedTile.x >= heroUnit.occupiedTile.x)
                                    {
                                        if (enemy.occupiedTile.x <= heroUnit.occupiedTile.x + heroUnit.attackRange)
                                        {
                                            enemy.takeDamage(heroUnit.getRangeDamage());
                                            UnitManager.Instance.SetSelectedHero(null);
                                            BattleMenuMenager.instance.updateQueue();
                                            //UnitManager.Instance.EnemyTurn();
                                            if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                            {
                                                UnitManager.Instance.EnemyTurn();
                                            }
                                        }
                                    }
                                    else if (enemy.occupiedTile.x < heroUnit.occupiedTile.x)
                                    {
                                        if (enemy.occupiedTile.x >= heroUnit.occupiedTile.x - heroUnit.attackRange)
                                        {
                                            enemy.takeDamage(heroUnit.getRangeDamage());
                                            UnitManager.Instance.SetSelectedHero(null);
                                            BattleMenuMenager.instance.updateQueue();
                                            //UnitManager.Instance.EnemyTurn();
                                            if (BattleMenuMenager.instance.initiativQueue.GetComponentInChildren<Image>().color == Color.red)
                                            {
                                                UnitManager.Instance.EnemyTurn();
                                            }
                                        }
                                    }
                                    heroUnit.setAmmo(heroUnit.getAmmo() - 1);
                                }
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

                        //MoveUnit(UnitManager.Instance.SelectedHero);


                        //Debug.Log(this.OccupiedUnit.name + " x: " + this.OccupiedUnit.occupiedTile.x + " y: " + this.OccupiedUnit.occupiedTile.y);
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


    public void MoveUnit(BaseUnit unit, Vector3 nextTile)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.OccupiedUnit = null;
        //StartCoroutine(MoveToPosition(unit.transform, new Vector3(x,y,0), 0.5f));
        Debug.Log("currentTile: " + unit.transform.position.x + " : " + unit.transform.position.y);
        Debug.Log("nextTile: " + nextTile.x + " : " + nextTile.y);

        StartCoroutine(MoveToPosition(unit.transform, nextTile, 0.5f));        

        OccupiedUnit = unit;
        unit.occupiedTile = this;
    }


    IEnumerator MoveToPosition(Transform transform, Vector3 targetPosition, float timeTo)
    {
        
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeTo;
            transform.position = Vector3.Lerp(currentPos, targetPosition, t);
            //transform.position = new Vector3(targetPosition.x, targetPosition.y);
            yield return null;
        }
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    //Do przedyskutowania/usunięcia
    public bool IsNeighborOccupied(Tile tile)
    {
        if (tile.x - 1 >= 0)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y)).OccupiedUnit != null) return true;
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y - 1)).OccupiedUnit != null) return true;
            if (tile.y + 1 < GridManager.Instance.height)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y + 1)).OccupiedUnit != null) return true;
        }
        if (tile.x + 1 < GridManager.Instance.width)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y)).OccupiedUnit != null) return true;
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y - 1)).OccupiedUnit != null) return true;
            if (tile.y + 1 < GridManager.Instance.height)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y + 1)).OccupiedUnit != null) return true;
        }
        if (tile.y - 1 >= 0) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y - 1)).OccupiedUnit != null) return true;
        if (tile.y + 1 < GridManager.Instance.height) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y + 1)).OccupiedUnit != null) return true;

        return false;
    }

    public List<Tile> NeighborOccupied(Tile tile)
    {

        List<Tile> occpiedTiles = new List<Tile>();

        if (tile.x - 1 >= 0)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y)));
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y - 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y - 1)));
            if (tile.y + 1 < GridManager.Instance.height)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y + 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x - 1, tile.y + 1)));
        }
        if (tile.x + 1 < GridManager.Instance.width)
        {
            if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y)));
            if (tile.y - 1 >= 0)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y - 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y - 1)));
            if (tile.y + 1 < GridManager.Instance.height)
                if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y + 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x + 1, tile.y + 1)));
        }
        if (tile.y - 1 >= 0) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y - 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y - 1)));
        if (tile.y + 1 < GridManager.Instance.height) if (GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y + 1)).OccupiedUnit != null) occpiedTiles.Add(GridManager.Instance.GetTileAtPosition(new Vector2(tile.x, tile.y + 1)));

        if (occpiedTiles.Count != 0)
        {
            return occpiedTiles;
        }
        else
        {
            return null;
        }
    }
}
