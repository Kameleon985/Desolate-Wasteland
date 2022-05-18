using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCovered : Node
{
    private BaseUnit enemy;
    private List<Transform> origin = new List<Transform>();

    public IsCovered(BaseUnit enemy)
    {
        this.enemy = enemy;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            origin.Add(p.transform);
        }
    }

    public override NodeState Evaluate()
    {
        int i = 0;
        foreach (Transform t in origin)
        {
            RaycastHit2D hit = Physics2D.Raycast(t.position, enemy.transform.position - t.position, Vector2.Distance(t.position, enemy.transform.position), LayerMask.GetMask("Terrain"));
            if (hit)
            {
                if (hit.transform.CompareTag("Terrain"))
                {
                    i += 1;
                }
            }
        }
        if (i == origin.Count)
        {
            //Debug.Log("covered");
            return NodeState.SUCCESS;
        }
        //Debug.Log("hit");
        return NodeState.FAILURE;
    }

}
