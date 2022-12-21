using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundTracker : MonoBehaviour
{
    public ArmyHandler armyHandler;
    public ResourcesHandler resourcesHandler;
    private bool passedAtleastOnce = false;
    //public bool isHydroponicsBuilt = false;

    public void IncrementRound()
    {
        if (!passedAtleastOnce)
        {
            Debug.Log("RoundTracker incrementRound() has not been called before");
        }
        //TempPlayerData.CurrentRound++;
        //UIUpdate.Instance.UpdateRound(TempPlayerData.CurrentRound);

        SaveSerial.CurrentRound++;
        UIUpdate.Instance.UpdateRound(SaveSerial.CurrentRound);
        GameEventSystem.Instance.NewTurn();
        GameEventSystem.Instance.PlayerMovement(10);
        if (passedAtleastOnce)
        {
            GameEventSystem.Instance.OnNewTurn += increaseResourcesDueToBuildings;
        }


        //WARNING THIS HAS TO BE AT THE END OF THE METHOD!
        passedAtleastOnce = true;



        /*if (SceneManager.GetActiveScene().name.Equals("Camp"))
        {
            *//*if (SaveSerial.CurrentRound % 7 == 0)
            {
                armyHandler.campIncrease();

            }*//*

            armyHandler.ArmyCostPerTurn();
            if (isHydroponicsBuilt)
            {
                resourcesHandler.AddVitals(3);
            }
        }*/
    }

    public void increaseResourcesDueToBuildings()
    {
        if (SaveSerial.HydroponicsBuild)
        {
            resourcesHandler.AddVitals(3);
        }
    }
}
