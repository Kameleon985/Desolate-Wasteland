using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{

    private AI ai;

    public ShootNode(AI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform closest = ai.GetClosestHero();
        GridManager.Instance.GetTileAtPosition(closest.position).OccupiedUnit.GetComponent<BaseHero>().takeDamage(RangeEnemy.GetDamage());
        Debug.Log("shot ");
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
        return NodeState.SUCCESS;
    }

}