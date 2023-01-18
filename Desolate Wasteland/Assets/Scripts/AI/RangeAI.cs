using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RangeAI : AI
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int lowHealthThreshold;
    [SerializeField] private int healthRestoreRate;

    [SerializeField] private int chasingRange;
    [SerializeField] private int distanceRange;


    //[SerializeField] private GameObject player;
    [SerializeField] private RangeEnemy enemy;


    private Transform closestHero;
    private Transform bestCoverSpot;
    private Transform transform;

    public Node topNode;

    private int _currentHealth;

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    public int currentHealth
    {
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Awake()
    {
        transform = gameObject.transform;

    }

    private void Start()
    {
        _currentHealth = startingHealth;
        GameEventSystem.Instance.OnRangeTurn += TakeAction;
        ConstructBehaviourTree();
    }

    public float GetChasingRange()
    {
        return chasingRange;
    }

    public override void ConstructBehaviourTree()
    {
        IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(enemy, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(enemy, this);
        HealthNode healthNode = new HealthNode(_currentHealth, lowHealthThreshold);
        IsCovered isCoveredNode = new IsCovered(enemy);
        RangeNode attackRangeNode = new RangeNode(enemy.attackRange, enemy, this);
        ShootNode shootNode = new ShootNode(this);
        RangeNode distanceNode = new RangeNode(distanceRange, enemy, this);
        MoveToRangeNode moveToRangeNode = new MoveToRangeNode(enemy, this);

        Sequence attackSequence = new Sequence(new List<Node> { attackRangeNode, shootNode }); ;
        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, attackSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence moveSequence = new Sequence(new List<Node> { distanceNode, moveToRangeNode });


        topNode = new Selector(new List<Node> { mainCoverSequence, attackSequence, moveSequence });
    }

    public override void TakeAction()
    {

        _currentHealth = GameObject.FindObjectOfType<RangeEnemy>().getCurrentHealth();
        ConstructBehaviourTree();
        if (topNode.Evaluate() == NodeState.FAILURE)
        {
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
        }
        //if (topNode.Evaluate() == NodeState.FAILURE)
        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        //Debug.Log(BattleMenuMenager.instance.initQueue.First().faction + "==next turn");


    }

    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public override void SetBestCoverSpot(Transform cover)
    {
        this.bestCoverSpot = cover;
    }

    public override Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    public override void SetClosestHero(Transform hero)
    {
        closestHero = hero;
    }

    public override Transform GetClosestHero()
    {
        return closestHero;
    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnRangeTurn -= TakeAction;
    }
}
