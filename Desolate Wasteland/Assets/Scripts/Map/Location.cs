using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool captured = true;
    private void Start()
    {
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
                        SaveSerial.Electronics = SaveSerial.Electronics + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        break;
                    }
                case "Scrapyard":
                    {
                        SaveSerial.Scrap = SaveSerial.Scrap + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        break;
                    }
                case "Plastics":
                    {
                        SaveSerial.Plastic = SaveSerial.Plastic + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        break;
                    }
                case "Shoping Center":
                    {
                        SaveSerial.Vitals = SaveSerial.Vitals + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
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
