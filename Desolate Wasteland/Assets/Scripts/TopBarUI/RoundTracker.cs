using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameEventSystem.Instance.NewTurn();
        GameEventSystem.Instance.PlayerMovement(10);

        if (SceneManager.GetActiveScene().name.Equals("Camp"))
        {
            if (SaveSerial.CurrentRound % 7 == 0)
            {
                armyHandler.campIncrease();

            }

            armyHandler.ArmyCostPerTurn();
            if (isHydroponicsBuilt)
            {
                resourcesHandler.AddVitals(3);
            }
        }


    }
}
