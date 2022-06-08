using System;
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
        GameEventSystem.Instance.OnLocationCapture += CapturedPrompt;
    }

    private void CapturedPrompt(Vector2 obj)
    {
        Debug.Log(obj.x + "," + obj.y);
        if ((transform.position.x - 1.5f < obj.x && obj.x < transform.position.x + 1.5f) && (transform.position.y - 1.5f < obj.y && obj.y < transform.position.y + 1.5f))
        {

            switch (gameObject.name)
            {
                case "Industrial Park":
                    {
                        int amount = UnityEngine.Random.Range(2, 10);

                        SaveSerial.Electronics = SaveSerial.Electronics + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś Park Industrialny, zdobyto " + amount + " elektroniki";
                        break;
                    }
                case "Scrapyard":
                    {
                        int amount = UnityEngine.Random.Range(2, 10);

                        SaveSerial.Scrap = SaveSerial.Scrap + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś złomowisko, zdobyto " + amount + " złomu";
                        break;
                    }
                case "Shoping Center":
                    {
                        int amount = UnityEngine.Random.Range(2, 10);

                        SaveSerial.Plastic = SaveSerial.Plastic + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś 'Plastics', zdobyto " + amount + " plastiku";
                        break;
                    }
                case "Hydrophonics":
                    {
                        int amount = UnityEngine.Random.Range(2, 10);

                        SaveSerial.Vitals = SaveSerial.Vitals + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Przejąłeś Sklep, zdobyto " + amount + " pożywienia";
                        break;
                    }
            }
        }
    }

    public void AddResource()
    {
        if (captured)
        {
            switch (gameObject.name)
            {
                case "Industrial Park":
                    {
                        int amount = 3;

                        SaveSerial.Electronics = SaveSerial.Electronics + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        //OnMapMessagePanel.SetActive(true);
                        //promptText.text = "Przejąłeś Park Industrialny, zdobyto " + amount + " elektroniki";
                        break;
                    }
                case "Scrapyard":
                    {
                        int amount = 3;

                        SaveSerial.Scrap = SaveSerial.Scrap + amount;
                        UIUpdate.Instance.UpdateUIValues();

                        //OnMapMessagePanel.SetActive(true);
                        //promptText.text = "Przejąłeś złomowisko, zdobyto " + amount + " złomu";
                        break;
                    }
                case "Shoping Center":
                    {
                        int amount = 3;

                        SaveSerial.Plastic = SaveSerial.Plastic + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        //OnMapMessagePanel.SetActive(true);
                        //promptText.text = "Przejąłeś 'Plastics', zdobyto " + amount + " plastiku";
                        break;
                    }
                case "Hydrophonics":
                    {
                        int amount = 5;

                        SaveSerial.Vitals = SaveSerial.Vitals + amount;
                        UIUpdate.Instance.UpdateUIValues();


                        //OnMapMessagePanel.SetActive(true);
                        //promptText.text = "Przejąłeś Sklep, zdobyto " + amount + " pożywienia";
                        break;
                    }
            }
        }

    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnNewTurn -= AddResource;
        GameEventSystem.Instance.OnLocationCapture -= CapturedPrompt;
    }
}
