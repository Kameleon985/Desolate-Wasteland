using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtackNode : Node
{
    private AI ai;
    public MeleeAtackNode(AI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform hero = ai.GetClosestHero();
        GridManager.Instance.GetTileAtPosition(hero.position).OccupiedUnit.GetComponent<BaseHero>().takeDamage(MeleeEnemy.GetDamage());
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
