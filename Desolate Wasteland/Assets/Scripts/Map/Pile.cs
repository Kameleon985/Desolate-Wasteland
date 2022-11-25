using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pile : MonoBehaviour
{

    public GameObject OnMapMessagePanel;
    public TextMeshProUGUI promptText;
    public MapGrid grid;

    private void Start()
    {
        //Debug.Log("STARTED");

        GameObject canvas = GameObject.Find("Canvas");
        OnMapMessagePanel = canvas.transform.GetChild(0).gameObject;
        promptText = OnMapMessagePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        //promptText = OnMapMessagePanel.GetComponentInChildren<TextMeshProUGUI>();
        grid = GameObject.Find("Grid").GetComponent<MapGrid>();
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
                case "Chems":
                    {
                        int amount = Random.Range(2, 10);
                        SaveSerial.ChemA = SaveSerial.ChemA + amount;
                        promptText.text = "Zdobyto " + amount + " ChemA,";
                        amount = Random.Range(2, 10);
                        SaveSerial.ChemB = SaveSerial.ChemB + amount;
                        promptText.text += "\n" + amount + " ChemB,";
                        amount = Random.Range(2, 10);
                        SaveSerial.ChemC = SaveSerial.ChemC + amount;
                        promptText.text += "\n" + amount + " ChemC,";
                        amount = Random.Range(2, 10);
                        SaveSerial.ChemD = SaveSerial.ChemD + amount;
                        promptText.text += "\n" + amount + " ChemD";
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);

                        OnMapMessagePanel.SetActive(true);

                        break;
                    }
            }
            float[] p = { transform.position.x, transform.position.y };
            var en = SaveSerial.piles.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Current.Key[0] == p[0] && en.Current.Key[1] == p[1])
                {
                    SaveSerial.piles.Remove(en.Current.Key);
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
