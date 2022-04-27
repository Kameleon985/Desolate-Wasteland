using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCovered : Node
{
    private BaseUnit enemy;
    private Transform origin;

    public IsCovered(BaseUnit enemy)
    {
        this.enemy = enemy;
        this.origin = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override NodeState Evaluate()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin.position, enemy.transform.position - origin.position, Vector2.Distance(origin.position, enemy.transform.position), LayerMask.GetMask("Terrain"));
        if (hit)
        {
            if (hit.transform.CompareTag("Terrain"))
            {
                //Debug.Log("covered");
                return NodeState.SUCCESS;
            }
        }
        //Debug.Log("hit");
        return NodeState.FAILURE;
    }

}
