using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private void Start()
    {
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
                        SaveSerial.Electronics = SaveSerial.Electronics + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);
                        break;
                    }
                case "Metal":
                    {
                        SaveSerial.Scrap = SaveSerial.Scrap + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);
                        break;
                    }
                case "Plastics":
                    {
                        SaveSerial.Plastic = SaveSerial.Plastic + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);
                        break;
                    }
                case "Food":
                    {
                        SaveSerial.Vitals = SaveSerial.Vitals + Random.Range(2, 10);
                        UIUpdate.Instance.UpdateUIValues();
                        Destroy(this.gameObject);
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
