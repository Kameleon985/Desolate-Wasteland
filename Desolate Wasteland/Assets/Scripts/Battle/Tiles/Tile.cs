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
