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

    public GameObject rowTemplate;

    public string[] saveFilesFullPath;
    public List<string> saveFilesJustNames;
    public List<string> saveFilesNamesAndDates;
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
        saveFilesNamesAndDates.Clear();
        GetLoadFiles();

        for (int i = 0; i <= saveFilesJustNames.Count - 1; i++)
        {
            GameObject newRow = Instantiate(rowTemplate);
            buttons.Add(newRow);
            newRow.SetActive(true);
            foreach (Transform eachChild in newRow.transform)
            {               
                    Debug.Log("Child found. Mame: " + eachChild.name);
            }

            GameObject saveButton = newRow.transform.Find("Button").gameObject;
            GameObject deleteButton = newRow.transform.Find("ButtonDel").gameObject;
            Debug.Log(deleteButton.name + "!");
            //button.GetComponent<ButtonListButton>().setButtonName(saveFilesJustNames[i]);
            saveButton.GetComponent<LoadButtonPropListButton>().setButtonName(saveFilesNamesAndDates[i]);
            deleteButton.GetComponent<DeleteButtonPropListButton>().setButtonName("Usuń", saveFilesJustNames[i], newRow);

            newRow.transform.SetParent(rowTemplate.transform.parent, false);
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

        //GetFileNames();
        GetFileNamesAndDates();
    }

    private void GetFileNames()
    {
        foreach(string file in saveFilesFullPath)
        {
            saveFilesJustNames.Add(Path.GetFileName(file).Substring(0, Path.GetFileName(file).Length-4));
        }
    }

    private void GetFileNamesAndDates()
    {
        foreach (string file in saveFilesFullPath)
        {
            saveFilesJustNames.Add(Path.GetFileName(file).Substring(0, Path.GetFileName(file).Length - 4));
            saveFilesNamesAndDates.Add(Path.GetFileName(file).Substring(0, Path.GetFileName(file).Length - 4) + " | "+ File.GetLastWriteTime(file));
        }
    }

    public void load(string fileName)
    {
        string cutString = fileName.Substring(0, fileName.Length - 22);

        SceneManager.LoadScene("Map");
        SaveSerial.LoadGame(cutString+".dat");
    }    

    public void save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        string fileName = saveNameInputField.text;

        GameEventSystem.Instance.SaveButton();

        SaveSerial.SaveGame(fileName);
    }

    public void delete(string saveToDelete, GameObject rowToDelete)
    {
        Debug.Log("Delete request: " + saveToDelete);

        string filePath = Application.persistentDataPath + "/saves/" + saveToDelete + ".dat";

        if (!File.Exists(filePath))
        {
            Debug.LogError("File '" + saveToDelete + ".dat' DOESN'T EXIST");
        } else
        {
            File.Delete(filePath);
            Destroy(rowToDelete);
            Debug.Log("Succefully deleted save");
        }
    }
}
