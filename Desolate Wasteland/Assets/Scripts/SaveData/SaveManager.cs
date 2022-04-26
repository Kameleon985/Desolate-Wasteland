using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public TMP_InputField saveNameInputField;


    public GameObject buttonTemplate;

    public string[] saveFilesFullPath;
    public List<string> saveFilesJustNames;
    public Button confirmSaveBtn;

    public List<GameObject> buttons;

    public void wakeUp()
    {
        buttons = new List<GameObject>();
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        clearButtons();
        saveFilesJustNames.Clear();
        saveFilesFullPath = new string[0];
        GetLoadFiles();

        for (int i = 0; i <= saveFilesJustNames.Count - 1; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(button);
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().setButtonName(saveFilesJustNames[i]);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public void clearButtons()
    {
        if (buttons.Count > 0)
        {
            foreach(GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

    private void Update()
    {
        if(saveNameInputField != null && confirmSaveBtn != null)
        {
            if (string.IsNullOrEmpty(saveNameInputField.text))
            {
                confirmSaveBtn.interactable = false;
            }
            else
            {
                confirmSaveBtn.interactable = true;
            }
        }
        
    }

    public void GetLoadFiles()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFilesFullPath = Directory.GetFiles(Application.persistentDataPath + "/saves/");
        
        GetFileNames();
    }

    private void GetFileNames()
    {
        foreach(string file in saveFilesFullPath)
        {
            saveFilesJustNames.Add(Path.GetFileName(file).Substring(0, Path.GetFileName(file).Length-4));
        }
    }

    public void load(string fileName)
    {
        SceneManager.LoadScene("Map");
        SaveSerial.LoadGame(fileName+".dat");
        Debug.Log("Done?");
    }    

    public void save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        string fileName = saveNameInputField.text;
        SaveSerial.SaveGame(fileName);
    }
}
