﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> units;

    public BaseHero SelectedHero;

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 2;

        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawn();

            randomSpawnTile.SetUnit(spawnedHero);

        }

        BattleMenager.instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawn();

            randomSpawnTile.SetUnit(spawnedEnemy);

        }

        BattleMenager.instance.ChangeState(GameState.PrepareHeroes);
    }

    public void PrepareHeroes()
    {
        
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.faction == faction).OrderBy(n => Random.value).First().unitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        BattleMenuMenager.instance.ShowSelectedHero(hero);
    }
}