using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;

    public int[] currentEnemies;
    public GameObject location;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        location = new GameObject();
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public event Action<GameObject> OnEnterLocation;
    public event Action<Vector2> OnLocationCapture;
    public event Action<Vector2> OnScoutBattle;
    public void EnterLocation(GameObject location)
    {

        if (location.name == "Map")
        {
            SceneManager.LoadScene(location.name);
            //Debug.Log("Ay yo?");
            StartCoroutine(LoadPosition());

            //Debug.Log("yoooooooo");
        }
        else if (location.name == "Camp")
        {
            OnEnterLocation?.Invoke(location);
            SceneManager.LoadScene(location.name);
        }
        else if (location.name == "Random")
        {
            OnEnterLocation?.Invoke(location);
            GameObject g = new GameObject("random");
            g.AddComponent<Location>();
            EnterBattle(g.GetComponent<Location>().generateDefendingArmy(5));
            SceneManager.LoadScene("Random");
        }
        else
        {
            //Debug.Log("Is already captured? " + location.GetComponent<Location>().GetCaptured());
            if (!location.GetComponent<Location>().GetCaptured())
            {
                this.location = location;
                /*
                location.GetComponent<Location>().SetCaptured(true);
                OnEnterLocation?.Invoke(location);
                float[] l = { location.transform.position.x, location.transform.position.y };
                SaveSerial.captured.Add(l, true);
                SceneManager.LoadScene(location.name);
                */
                Vector2 v = new Vector2(location.transform.position.x, location.transform.position.y);
                OnScoutBattle?.Invoke(v);

            }
        }

    }


    public void ConfirmEnter()
    {
        location.GetComponent<Location>().SetCaptured(true);
        OnEnterLocation?.Invoke(location);
        float[] l = { Mathf.Floor(location.transform.position.x), Mathf.Floor(location.transform.position.y) };
        SaveSerial.captured.Add(l, true);

        SceneManager.LoadScene(location.name);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            OnEnterMap?.Invoke();
        }
        else if (level > 2)
        {
            OnEnterBattle?.Invoke(currentEnemies);
        }
    }
    public event Action OnEnterMap;

    IEnumerator LoadPosition()
    {
        while (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Map"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        OnEnterMap?.Invoke();
        OnPlayerMovement?.Invoke();
        yield return new WaitForSeconds(0.1f);
        OnLocationCapture?.Invoke(new Vector2(SaveSerial.onMapPosition[0], SaveSerial.onMapPosition[1]));
    }

    public event Action OnPlayerMovement;

    public void PlayerMovement(float points)
    {
        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
        //{
        SaveSerial.onMapMovementPoints = points;
        //}
        OnPlayerMovement?.Invoke();
    }

    public event Action<float> OnPlayerClick;

    public void PlayerClick(float cost)
    {
        OnPlayerClick?.Invoke(cost);
    }

    public event Action<BaseUnit> OnUnitTurn;
    public event Action OnMeleeTurn;
    public event Action OnRangeTurn;
    public event Action OnEliteTurn;

    public void EnemyTurn(BaseUnit enemy)
    {
        //OnUnitTurn?.Invoke(enemy);
        //Debug.Log(enemy.faction + " current faction");
        //Debug.Log(enemy.GetType().Name + " unit name on enemy turn");
        if (enemy.faction == Faction.Enemy)
        {
            //Debug.Log(enemy.GetType().Name);
            switch (enemy.GetType().Name)
            {
                case "MeleeEnemy":
                    {
                        OnMeleeTurn?.Invoke();
                        break;
                    }
                case "RangeEnemy":
                    {
                        OnRangeTurn?.Invoke();
                        break;
                    }
                case "EliteEnemy":
                    {
                        OnEliteTurn?.Invoke();
                        break;
                    }
            }
        }

    }


    public event Action<GameObject> OnPilePickup;

    public void PilePickup(GameObject pile)
    {
        OnPilePickup?.Invoke(pile);
    }

    public event Action<GameObject> OnSaveButton;

    public void SaveButton()
    {
        GameObject g = new GameObject();
        OnSaveButton?.Invoke(g);

    }

    public event Action OnNewWeek;


    public event Action OnNewTurn;

    public void NewTurn()
    {
        if (SaveSerial.CurrentRound % 7 == 0 && SaveSerial.CurrentRound != 0)
        {
            OnNewWeek?.Invoke();
        }
        //GridManager.Instance.ClearAllHighlightTiles();
        //GridManager.Instance.ClearAStarTiles();
        OnNewTurn?.Invoke();
    }

    public event Action OnHeroesTurn;

    public void HeroTurn()
    {
        //Debug.Log("Hero turn");
        OnHeroesTurn?.Invoke();
    }

    public event Action<bool> OnOverButtons;
    public void OverButtons(bool mouseOver)
    {
        OnOverButtons?.Invoke(mouseOver);
    }

    public event Action<int[]> OnEnterBattle;

    public void EnterBattle(int[] enemies)
    {
        currentEnemies = new int[3];
        OnEnterBattle?.Invoke(enemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            currentEnemies[i] = enemies[i];
        }
    }

}