using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public static UIUpdate Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Text VitalsCount;
    public Text ScrapCount;
    public Text PlasticCount;
    public Text ElectronicsCount;

    public Text RoundTracker;

    public void SetVitals(int number)
    {
        VitalsCount.text = number + "";
        SaveSerial.Vitals = number;
    }
    public void SetScrap(int number)
    {
        ScrapCount.text = number + "";
        SaveSerial.Scrap = number;
    }
    public void SetPlastic(int number)
    {
        PlasticCount.text = number + "";
        SaveSerial.Plastic = number;
    }
    public void SetElectronics(int number)
    {
        ElectronicsCount.text = number + "";
        SaveSerial.Electronics = number;
    }

    public void UpdateRound(int number)
    {
        RoundTracker.text = "Dzień " + number;
        SaveSerial.CurrentRound = number;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
