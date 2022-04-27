﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    protected List<Node> nodes = new List<Node>();

    public Inverter(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                case NodeState.FAILURE:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                default:
                    break;
            }
        }
        return _nodeState;
    }
}
