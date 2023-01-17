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

    private void Awake()
    {
        GameEventSystem.Instance.OnNewTurn += increaseResourcesDueToBuildings;
        GameEventSystem.Instance.OnNewWeek += campIncrease;
        GameEventSystem.Instance.OnNewTurn += ArmyHandler.ArmyCostPerTurn;
    }

    public static void campIncrease()
    {
        ArmyHandler.BarrackBuiltIncrease();
        ArmyHandler.ShootingRangeBuiltIncrease();
        ArmyHandler.ArmoryBuiltIncrease();
    }

    public void IncrementRound()
    {
        //TempPlayerData.CurrentRound++;
        //UIUpdate.Instance.UpdateRound(TempPlayerData.CurrentRound);

        SaveSerial.CurrentRound++;
        UIUpdate.Instance.UpdateRound(SaveSerial.CurrentRound);
        GameEventSystem.Instance.NewTurn();
        GameEventSystem.Instance.PlayerMovement(10);



        //WARNING THIS HAS TO BE AT THE END OF THE METHOD!



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

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnNewTurn -= increaseResourcesDueToBuildings;
        GameEventSystem.Instance.OnNewWeek -= campIncrease;
        GameEventSystem.Instance.OnNewTurn -= ArmyHandler.ArmyCostPerTurn;

    }
}
