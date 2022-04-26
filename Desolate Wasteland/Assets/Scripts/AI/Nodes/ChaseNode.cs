using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{

    private BaseEnemy gameObject;
    EnemyAI ai;
    public ChaseNode(BaseEnemy gameObject, EnemyAI ai)
    {

        this.gameObject = gameObject;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Tile target = GridManager.Instance.GetTileAtPosition(player.transform.position);

        gameObject.Chase(target);

        BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        return NodeState.SUCCESS;

    }


}
