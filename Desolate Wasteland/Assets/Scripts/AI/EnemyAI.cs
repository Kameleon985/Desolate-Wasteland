using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [SerializeField] private float chasingRange;

    internal float GetcurrentHealth()
    {
        throw new NotImplementedException();
    }

    [SerializeField] private float shootingRange;


    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Cover[] avaliableCovers;



    private Material material;
    private Transform bestCoverSpot;
    private Transform transform;

    private Node topNode;

    private float _currentHealth;

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    public float currentHealth
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
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(avaliableCovers, enemy);
        GoToCoverNode goToCoverNode = new GoToCoverNode(transform, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCovered isCoveredNode = new IsCovered(enemy);
        ChaseNode chaseNode = new ChaseNode(enemy);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, enemy);
        //RangeNode shootingRangeNode = new RangeNode(shootingRange, player, transform);
        //ShootNode shootNode = new ShootNode(transform, this, player);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        //Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { chaseSequence });


    }

    private void Update()
    {

        topNode.Evaluate();

        _currentHealth += Time.deltaTime * healthRestoreRate;
    }


    public void TakeDamage(float damage)
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


}