using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pile : MonoBehaviour
{

    public GameObject OnMapMessagePanel;
    public TextMeshProUGUI promptText;

    private void Start()
    {
        Debug.Log("STARTED");

        OnMapMessagePanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        promptText = OnMapMessagePanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        GameEventSystem.Instance.OnPilePickup += AddResources;
    }
    public void AddResources(GameObject pile)
    {
        if (pile.transform == this.transform)
        {
            string name = this.gameObject.name;
            switch (name)
            {
                case "Electronics":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Electronics = SaveSerial.Electronics + amount;
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Zdobyto " + amount + " elektroniki";

                        break;
                    }
                case "Metal":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Scrap = SaveSerial.Scrap + amount;
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Zdobyto " + amount + " złomu";

                        break;
                    }
                case "Plastics":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Plastic = SaveSerial.Plastic + amount;
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Zdobyto " + amount + " plastiku";

                        break;
                    }
                case "Food":
                    {
                        int amount = Random.Range(2, 10);

                        SaveSerial.Vitals = SaveSerial.Vitals + amount;
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);

                        OnMapMessagePanel.SetActive(true);
                        promptText.text = "Zdobyto " + amount + " pożywienia";

                        break;
                    }
            }
        }

    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnPilePickup -= AddResources;
    }
}
