using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    public ArmyHandler armyHandler;
    public ResourcesHandler resourcesHandler;
    public bool isHydroponicsBuilt = false;

    public void IncrementRound()
    {
        //TempPlayerData.CurrentRound++;
        //UIUpdate.Instance.UpdateRound(TempPlayerData.CurrentRound);

        SaveSerial.CurrentRound++;
        UIUpdate.Instance.UpdateRound(SaveSerial.CurrentRound);

        if(SaveSerial.CurrentRound % 7 == 0)
        {
            armyHandler.campIncrease();
            
        }

        armyHandler.ArmyCostPerTurn();
        Debug.Log("hydro: " + isHydroponicsBuilt);
        if (isHydroponicsBuilt)
        {
            resourcesHandler.AddVitals(3);
        }
        
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
