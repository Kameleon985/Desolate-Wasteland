using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int lowHealthThreshold;
    [SerializeField] private int healthRestoreRate;

    [SerializeField] private int chasingRange;


    //[SerializeField] private GameObject player;
    [SerializeField] private MeleeEnemy enemy;


    private BaseUnit closestHero;
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
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(enemy, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(enemy, this);
        HealthNode healthNode = new HealthNode(_currentHealth, lowHealthThreshold);
        IsCovered isCoveredNode = new IsCovered(enemy);
        ChaseNode chaseNode = new ChaseNode(enemy);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, enemy, this);
        //RangeNode shootingRangeNode = new RangeNode(shootingRange, player, transform);
        //ShootNode shootNode = new ShootNode(transform, this, player);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        //Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, chaseSequence });


    }

    private void TakeAction()
    {
        _currentHealth = GameObject.FindObjectOfType<MeleeEnemy>().getCurrentHealth();
        ConstructBehahaviourTree();
        if (topNode.Evaluate() == NodeState.FAILURE)
            BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        BattleMenager.instance.ChangeState(GameState.HeroesTurn);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void SetColor(Color color)
    {

    }

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    public void SetClosest(BaseUnit unit)
    {
        closestHero = unit;
    }
    public BaseUnit GetClosest()
    {
        return closestHero;
    }

    public float GetChasingRange()
    {
        return chasingRange;
    }
}