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
        GameEventSystem.Instance.OnUnitTurn += TakeAction;
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
        HealthNode healthNode = new HealthNode(enemy.getCurrentHealth(), lowHealthThreshold);
        IsCovered isCoveredNode = new IsCovered(enemy);
        RangeNode attackRangeNode = new RangeNode(enemy.attackRange, enemy, this);
        EliteShootNode shootNode = new EliteShootNode(this, enemy);
        RangeNode distanceNode = new RangeNode(distanceRange, enemy, this);
        MoveToEliteRangeNode moveToRangeNode = new MoveToEliteRangeNode(enemy, this);
        AmmoCheckNode ammoCheckNode = new AmmoCheckNode(enemy);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, enemy, this);
        ChaseNode chaseNode = new ChaseNode(enemy, this);
        IsInMeleeRange isInMeleeRangeNode = new IsInMeleeRange(enemy, this);
        MeleeAtackNode meleeAtackNode = new MeleeAtackNode(this);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence moveSequence = new Sequence(new List<Node> { distanceNode, moveToRangeNode });
        Sequence attackSequence = new Sequence(new List<Node> { attackRangeNode, shootNode });
        Sequence ammoCheckSequence = new Sequence(new List<Node> { ammoCheckNode, attackSequence });
        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, ammoCheckSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });
        Sequence meleeSequence = new Sequence(new List<Node> { isInMeleeRangeNode, meleeAtackNode });



        topNode = new Selector(new List<Node> { mainCoverSequence, ammoCheckSequence, meleeSequence, chaseSequence, moveSequence });
    }

    public override void TakeAction()
    {

        _currentHealth = GameObject.FindObjectOfType<EliteEnemy>().getCurrentHealth();
        ConstructBehaviourTree();
        topNode.Evaluate();
        //Debug.Log("health: " + enemy.getCurrentHealth());
        //Debug.Log("threshold: " + lowHealthThreshold);
        BattleMenager.instance.ChangeState(GameState.HeroesTurn);
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
        GameEventSystem.Instance.OnUnitTurn -= TakeAction;
    }
}

