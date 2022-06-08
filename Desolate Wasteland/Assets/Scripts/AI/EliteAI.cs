using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EliteAI : AI
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int lowHealthThreshold;
    [SerializeField] private int healthRestoreRate;

    [SerializeField] private int chasingRange;
    [SerializeField] private int distanceRange;


    //[SerializeField] private GameObject player;
    [SerializeField] private EliteEnemy enemy;


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
        GameEventSystem.Instance.OnEliteTurn += TakeAction;
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
        EliteShootNode shootNode = new EliteShootNode(this, enemy);
        RangeNode distanceNode = new RangeNode(distanceRange, enemy, this);
        MoveToEliteRangeNode moveToRangeNode = new MoveToEliteRangeNode(enemy, this);
        AmmoCheckNode ammoCheckNode = new AmmoCheckNode(enemy);


        Sequence attackSequence = new Sequence(new List<Node> { attackRangeNode, shootNode });
        Sequence ammoCheckSequence = new Sequence(new List<Node> { ammoCheckNode, attackSequence });
        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, attackSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence moveSequence = new Sequence(new List<Node> { distanceNode, moveToRangeNode });

        topNode = new Selector(new List<Node> { mainCoverSequence, ammoCheckSequence, moveSequence });
    }

    public override void TakeAction()
    {

        _currentHealth = GameObject.FindObjectOfType<RangeEnemy>().getCurrentHealth();
        ConstructBehaviourTree();
        topNode.Evaluate();
        //if (topNode.Evaluate() == NodeState.FAILURE)
        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        //Debug.Log(BattleMenuMenager.instance.initQueue.First().faction + "==next turn");
        if (BattleMenuMenager.instance.initQueue.Peek().faction == Faction.Enemy)
        {
            //UnitManager.Instance.EnemyTurn();
            //GameEventSystem.Instance.EnemyTurn(BattleMenuMenager.instance.initQueue.Peek());
            BattleMenager.instance.ChangeState(GameState.EnemiesTurn);
        }
        else
        {
            BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        }
        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);
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
        GameEventSystem.Instance.OnEliteTurn -= TakeAction;
    }
}

