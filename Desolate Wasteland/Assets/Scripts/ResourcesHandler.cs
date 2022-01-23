using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{

    public void AddVitals()
    {
        TempPlayerData.VitalsCount++;
        UIUpdate.Instance.SetVitals(TempPlayerData.VitalsCount);
    }
    public void AddScrap()
    {
        TempPlayerData.ScrapCount++;
        UIUpdate.Instance.SetScrap(TempPlayerData.ScrapCount);
    }
    public void AddPlastic()
    {
        TempPlayerData.PlasticCount++;
        UIUpdate.Instance.SetPlastic(TempPlayerData.PlasticCount);
    }
    public void AddElectronics()
    {
        TempPlayerData.ElectronicsCount++;
        UIUpdate.Instance.SetElectronics(TempPlayerData.ElectronicsCount);
    }

    //############ Removing Below

    public void RemoveVitals()
    {
        TempPlayerData.VitalsCount--; //TO-DO remove amount dependent on how many units are recruited
        
        UIUpdate.Instance.SetVitals(TempPlayerData.VitalsCount);

        //TO-DO what to do if below 0
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
