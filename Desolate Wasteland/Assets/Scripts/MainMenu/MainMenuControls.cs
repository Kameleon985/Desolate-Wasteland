using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    public string _mainMapScene;
    private string saveToLoad;

    public void NewGame()
    {
        SceneManager.LoadScene(_mainMapScene);
        SaveSerial.NewGameSetData();

    }

    public void Continue()
    {
        SceneManager.LoadScene(_mainMapScene);
    }

    public void LoadGameMenu()
    {
        //To call methods that load in save data
        SceneManager.LoadScene(_mainMapScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
