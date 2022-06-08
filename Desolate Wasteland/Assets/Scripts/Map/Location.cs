using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    private bool captured = false;

    public GameObject OnMapMessagePanel;
    public TextMeshProUGUI promptText;

    public void SetCaptured(bool b)
    {
        captured = b;
    }

    public bool GetCaptured()
    {
        return captured;
    }

    private void Start()
    {
        OnMapMessagePanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        promptText = OnMapMessagePanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        GameEventSystem.Instance.OnNewTurn += AddResource;
    }

    public void AddResource()
    {
        if (captured)
        {
            switch (gameObject.name)
            {
                case "Industrial Park":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Electronics = SaveSerial.Electronics + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś Park Industrialny, zdobyto " + amount + " elektroniki";
                        break;
                    }
                case "Scrapyard":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Scrap = SaveSerial.Scrap + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś złomowisko, zdobyto " + amount + " złomu";
                        break;
                    }
                case "Plastics":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Plastic = SaveSerial.Plastic + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś 'Plastics', zdobyto " + amount + " plastiku";
                        break;
                    }
                case "Shoping Center":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Vitals = SaveSerial.Vitals + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś Sklep, zdobyto " + amount + " pożywienia";
                        break;
                    }
            }
        }

    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnNewTurn -= AddResource;
    }
}
