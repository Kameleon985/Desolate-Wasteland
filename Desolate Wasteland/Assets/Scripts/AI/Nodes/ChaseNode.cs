using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{

    private BaseEnemy gameObject;
    private AI ai;
    public ChaseNode(BaseEnemy gameObject, AI ai)
    {
        this.ai = ai;
        this.gameObject = gameObject;
    }

    public override NodeState Evaluate()
    {

        Tile target = GridManager.Instance.GetTileAtPosition(ai.GetClosestHero().position);
        //Debug.Log("Chasin");
        gameObject.Chase(target);

        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);

        return NodeState.SUCCESS;

    }


}
