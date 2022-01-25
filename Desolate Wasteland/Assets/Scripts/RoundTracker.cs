using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    public void IncrementRound()
    {
        //TempPlayerData.CurrentRound++;
        //UIUpdate.Instance.UpdateRound(TempPlayerData.CurrentRound);

        SaveSerial.CurrentRound++;
        UIUpdate.Instance.UpdateRound(SaveSerial.CurrentRound);
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
