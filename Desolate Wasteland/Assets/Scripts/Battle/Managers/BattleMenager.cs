using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMenager : MonoBehaviour
{
    public static BattleMenager instance;
    public GameState gameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
        BattleMenuMenager.instance.ShowCurrentGameState();
    }

    private void Update()
    {
        BattleMenuMenager.instance.ShowCurrentGameState();

        if (UnitManager.Instance.enemyList.Count == 0)
        {
            Debug.Log("You win!");
            GameEventSystem.Instance.EnterLocation("Map");
        }
        if (UnitManager.Instance.heroList.Count == 0)
        {
            Debug.Log("You lose!");
            SceneManager.LoadScene("Map");
        }
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PrepareHeroes:
                UnitManager.Instance.PrepareHeroes();
                break;
            case GameState.HeroesTurn:
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.EnemyTurn();
                break;
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    PrepareHeroes = 3,
    HeroesTurn = 4,
    EnemiesTurn = 5
}
