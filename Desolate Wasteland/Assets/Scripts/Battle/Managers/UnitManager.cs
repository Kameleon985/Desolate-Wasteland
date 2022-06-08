using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> units;

    public BaseHero SelectedHero;

    public MeleeAi ai;
    public List<BaseHero> heroList;
    public List<BaseEnemy> enemyList;

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnHeroes()
    {
        var heroCount = 3;

        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero, heroCount, i);
            if (randomPrefab is MeleeUnit && SaveSerial.MeleeUnit != 0)
            {
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawn();
                randomSpawnTile.SetUnit(spawnedHero);
                heroList.Add(spawnedHero);
            }
            if (randomPrefab is RangedUnit && SaveSerial.RangeUnit != 0)
            {
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawn();
                randomSpawnTile.SetUnit(spawnedHero);
                heroList.Add(spawnedHero);
            }
            if (randomPrefab is EliteUnit && SaveSerial.EliteUnit != 0)
            {
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawn();
                randomSpawnTile.SetUnit(spawnedHero);
                heroList.Add(spawnedHero);
            }
        }

        BattleMenager.instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 3;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy, enemyCount, i);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawn();

            randomSpawnTile.SetUnit(spawnedEnemy);
            enemyList.Add(spawnedEnemy);
        }

        BattleMenager.instance.ChangeState(GameState.PrepareHeroes);
    }

    public void PrepareHeroes()
    {
        if (!BattleMenuMenager.instance.endPrepareHeroes.activeSelf)
        {
            BattleMenager.instance.ChangeState(GameState.HeroesTurn);
        }
    }

    private T GetRandomUnit<T>(Faction faction, int unitCount, int currentUnit) where T : BaseUnit
    {

        if (currentUnit != unitCount)
        {
            return (T)units.Where(u => u.faction == faction).ToList()[currentUnit].unitPrefab;
        }
        return null;

    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        BattleMenuMenager.instance.ShowSelectedHero(hero);
    }

    public void EnemyTurn()
    {
        //GameObject gameObject = units.Where(u => u.faction == Faction.Enemy && u.unitPrefab.unitName.Equals("MeleeEnemy")).First().unitPrefab.gameObject;
        //BaseUnit b = BattleMenuMenager.instance.initQueue.Dequeue();
        BattleMenuMenager.instance.updateQueue();
        GameEventSystem.Instance.EnemyTurn(BattleMenuMenager.instance.initQueue.Peek());
        //BattleMenuMenager.instance.initQueue.Enqueue(b);
        //gameObject.GetComponent<EnemyAI>().Invoke("Evaluate", 10);
        //Debug.Log(ai.gameObject.name);
        //Debug.Log("Enemy Turn");
        //BattleMenager.instance.ChangeState(GameState.HeroesTurn);
    }

    public List<ScriptableUnit> GetUnits()
    {
        return units;
    }
}
