using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCovered : Node
{
    private GameObject target;
    private Transform origin;

    public IsCovered(GameObject target)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin.position, target.transform.position - origin.position);
        if (hit)
        {
            if (hit.collider.transform != target)
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }

}
