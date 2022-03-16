﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventSystem : MonoBehaviour
{
    PlayerData data;
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

    public GameEventSystem()
    {
        data = new PlayerData();
    }

    public event Action<PlayerData> OnEnterLocation;
    public void EnterLocation(String name)
    {
        if (name == "Map")
        {

            SceneManager.LoadScene(name);
            StartCoroutine(LoadPosition());

            Debug.Log(data.position);
        }
        else
        {
            OnEnterLocation?.Invoke(data);
            SceneManager.LoadScene(name);
            Debug.Log(data.position);
        }

    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 3)
        {
            OnEnterMap?.Invoke(data);
        }
    }
    public event Action<PlayerData> OnEnterMap;

    IEnumerator LoadPosition()
    {
        while (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Map"))
        {
            yield return null;
        }
        OnEnterMap?.Invoke(data);
        OnPlayerMovement?.Invoke(data);
        Debug.Log(data.movePoints);
    }

    public event Action<PlayerData> OnPlayerMovement;

    public void PlayerMovement(float points)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
        {
            data.SetMovePoints(points);
        }
        OnPlayerMovement?.Invoke(data);
    }

}