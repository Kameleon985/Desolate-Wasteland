using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{

    public void AddVitals(int number)
    {
        Debug.Log("Vitals increase, was:" + SaveSerial.Vitals + ", increase by:" + number);
        SaveSerial.Vitals += number;
        UIUpdate.Instance.SetVitals(SaveSerial.Vitals);

    }
    public void AddScrap()
    {
        SaveSerial.Scrap++;
        UIUpdate.Instance.SetScrap(SaveSerial.Scrap);
    }
    public void AddScrap(int number)
    {
        SaveSerial.Scrap += number;
        UIUpdate.Instance.SetScrap(SaveSerial.Scrap);
    }

    public void AddPlastic()
    {

        SaveSerial.Plastic++;
        UIUpdate.Instance.SetPlastic(SaveSerial.Plastic);
    }
    public void AddPlastic(int number)
    {
        SaveSerial.Plastic += number;
        UIUpdate.Instance.SetPlastic(SaveSerial.Plastic);
    }

    public void AddElectronics()
    {

        SaveSerial.Electronics++;
        UIUpdate.Instance.SetElectronics(SaveSerial.Electronics);
    }

    public void AddElectronics(int number)
    {
        SaveSerial.Electronics += number;
        UIUpdate.Instance.SetElectronics(SaveSerial.Electronics);
    }

    //############ Removing Below

    public void RemoveVitals(int number)
    {

        SaveSerial.Vitals -= number;
        UIUpdate.Instance.SetVitals(SaveSerial.Vitals);
    }

    public void RemoveScrap(int number)
    {
        SaveSerial.Scrap -= number;
        UIUpdate.Instance.SetScrap(SaveSerial.Scrap);
    } 

    public void RemovePlastic(int number)
    {
        SaveSerial.Plastic -= number;
        UIUpdate.Instance.SetPlastic(SaveSerial.Plastic);
    }
    public void RemoveElectronics(int number)
    {
        SaveSerial.Electronics -= number;
        UIUpdate.Instance.SetElectronics(SaveSerial.Electronics);
    }

    public void RemoveResources(int scrap, int plastic, int electronics)
    {
        RemoveScrap(scrap);
        RemovePlastic(plastic);
        RemoveElectronics(electronics);
    }
}
