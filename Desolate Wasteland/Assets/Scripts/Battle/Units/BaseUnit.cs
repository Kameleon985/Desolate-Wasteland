using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    public Faction faction;
    public string unitName;

    private Pathfinding pathfinding;

    public abstract int getInitiative();
    public abstract void setInitiative(int init);

    private void Awake()
    {
        GameEventSystem.Instance.OnHeroesTurn += HighlightCurrent;
    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnHeroesTurn -= HighlightCurrent;
    }

    public void HighlightCurrent()
    {
        var peek = BattleMenuMenager.instance.q1.Peek();
        //Debug.Log("peek = " + peek);
        //Debug.Log("this name = " + this.GetType().Name);
        if (peek.GetType().Name.Equals(this.GetType().Name))
        {
            occupiedTile.highlight.SetActive(true);
        }
    }

    public void Move(Tile targetTile)
    {
        //Debug.Log("MOVEING OOOOOOOOOOOOOO");
        StartCoroutine(cor(targetTile));

        GridManager.Instance.ClearAStarTiles();
        GridManager.Instance.ClearAllHighlightTiles();
        /*
        pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.FindPath(occupiedTile, targetTile);


        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {

                path[i].pathHighlight.SetActive(true);
                //Debug.Log("Moveing");
                //path[i].MoveUnit(this, new Vector3(path[i + 1].x, path[i + 1].y));
                path[i].SetUnit(this);
            }
        }*/
    }

    private IEnumerator cor(Tile targetTile)
    {
        pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.FindPath(occupiedTile, targetTile);
        //Debug.Log(targetTile.name + " = target tile");
        //Debug.Log(occupiedTile + " = occupied tile");
        //Debug.Log(path.Count + " = path Count");

        if (path != null)
        {
            //Debug.Log("Path not null");
            for (int i = 0; i < path.Count; i++)
            {
                /*if (i != path.Count - 1)
                {
                    path[i].pathHighlight.SetActive(true);
                }*/
                //Debug.Log("Moveing");
                path[i].MoveUnit(this, new Vector3(path[i].x, path[i].y));
                yield return new WaitForSeconds(0.1f);
            }
            path[path.Count - 1].SetUnit(this);
            occupiedTile = path[path.Count - 1];
            BattleMenuMenager.instance.UpdateQueue();
            if (BattleMenuMenager.instance.q1.Peek().faction == Faction.Enemy)
            {
                //UnitManager.Instance.EnemyTurn();
                //GameEventSystem.Instance.EnemyTurn(BattleMenuMenager.instance.initQueue.Peek());
                BattleMenager.instance.ChangeState(GameState.EnemiesTurn);
            }
            else
            {
                BattleMenager.instance.ChangeState(GameState.HeroesTurn);
            }
            //Debug.Log("Enough");

        }
    }

}