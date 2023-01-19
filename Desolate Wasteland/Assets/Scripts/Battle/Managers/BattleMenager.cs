using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMenager : MonoBehaviour
{
    public static BattleMenager instance;
    public GameState gameState;
    public int[] enemies;

    private void Awake()
    {
        instance = this;
        GameEventSystem.Instance.OnEnterBattle += LoadEnemies;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
        BattleMenuMenager.instance.ShowCurrentGameState();
    }

    private void Update()
    {
        BattleMenuMenager.instance.ShowCurrentGameState();

        //Debug.Log(SceneManager.GetActiveScene().name);
        if (UnitManager.Instance.enemyList.Count == 0)
        {
            //Debug.Log("You win!");
            if (SceneManager.GetActiveScene().name.Equals("Factory"))
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                GameObject g = new GameObject();
                g.name = "Map";
                GameEventSystem.Instance.EnterLocation(g);
            }
        }
        if (UnitManager.Instance.heroList.Count == 0)
        {
            //Debug.Log("You lose!");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void LoadEnemies(int[] enemies)
    {
        this.enemies = new int[3];
        for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies[i] = enemies[i];
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
                UnitManager.Instance.SpawnEnemies(enemies);
                break;
            case GameState.PrepareHeroes:
                UnitManager.Instance.PrepareHeroes();
                break;
            case GameState.HeroesTurn:
                //Debug.Log("bm hero turn");
                GameEventSystem.Instance.HeroTurn();
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.EnemyTurn();
                break;
        }
    }

    public void OnDestroy()
    {
        GameEventSystem.Instance.OnEnterBattle -= LoadEnemies;
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


