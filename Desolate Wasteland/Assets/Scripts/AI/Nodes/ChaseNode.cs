using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{

    private GameObject gameObject;
    Pathfinding pf;
    float timer = 0;
    public ChaseNode(GameObject gameObject)
    {

        this.gameObject = gameObject;
        pf = new Pathfinding();
    }

    public override NodeState Evaluate()
    {
        GridManager.Instance.GetTileAtPosition(gameObject.transform.position);
        //int distance = Pathfinding.CalculateDistance(GridManager.Instance.GetTileAtPosition(gameObject.transform.position), GridManager.Instance.GetTileAtPosition(target.transform.position));

        if (0 > 3)
        {

            //target.transform.position = Vector3.Lerp(transform.position, target.transform.position, timer);
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, Time.deltaTime * 5);
            timer += Time.deltaTime;
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }


}
