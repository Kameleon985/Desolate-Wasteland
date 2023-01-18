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
            int restor = 0;
            //Debug.Log("covered");
            if (enemy is MeleeEnemy)
            {
                restor = MeleeEnemy.maxHealth / 4;
                if (restor + MeleeEnemy.currentHealth > MeleeEnemy.maxHealth)
                {
                    MeleeEnemy.currentHealth = MeleeEnemy.maxHealth;
                }
                else
                {
                    MeleeEnemy.currentHealth = restor + MeleeEnemy.currentHealth;
                }
                var unit = (MeleeEnemy)enemy;
                unit.setUnitUIData();
            }
            else if (enemy is RangeEnemy)
            {
                restor = RangeEnemy.maxHealth / 4;
                if (restor + RangeEnemy.currentHealth > RangeEnemy.maxHealth)
                {
                    RangeEnemy.currentHealth = RangeEnemy.maxHealth;
                }
                else
                {
                    RangeEnemy.currentHealth = restor + RangeEnemy.currentHealth;
                }
                var unit = (RangeEnemy)enemy;
                unit.setUnitUIData();
            }
            else if (enemy is EliteEnemy)
            {
                restor = EliteEnemy.maxHealth / 4;
                if (restor + EliteEnemy.currentHealth > EliteEnemy.maxHealth)
                {
                    EliteEnemy.currentHealth = EliteEnemy.maxHealth;
                }
                else
                {
                    EliteEnemy.currentHealth = restor + EliteEnemy.currentHealth;
                }
                var unit = (EliteEnemy)enemy;
                unit.setUnitUIData();
            }
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
        //Debug.Log("hit");
        return NodeState.FAILURE;
    }

}
