﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public event Action OnEnterLocation;
    public void EnterLocation(String name)
    {
        if (name == "Map")
        {
            SceneManager.LoadScene(name);
            StartCoroutine(LoadPosition());
        }
        else
        {
            OnEnterLocation?.Invoke();
            SceneManager.LoadScene(name);
        }

    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            OnEnterMap?.Invoke();
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
    }

    public event Action OnPlayerMovement;

    public void PlayerMovement(float points)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
        {
            SaveSerial.onMapMovementPoints = points;
        }
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
        Debug.Log(enemy.GetType().Name);
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


    public event Action<GameObject> OnPilePickup;

    public void PilePickup(GameObject pile)
    {
        OnPilePickup?.Invoke(pile);
    }
    public event Action OnSaveButton;

    public void SaveButton()
    {
        OnSaveButton?.Invoke();

    }

    public event Action OnNewTurn;

    public void NewTurn()
    {
        OnNewTurn?.Invoke();
    }


}