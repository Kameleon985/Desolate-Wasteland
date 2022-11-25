using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    public Text movePoints;
    public Text potentialCost;
    public GameObject canvas;


    private void Awake()
    {

    }
    public void Start()
    {
        GameEventSystem.Instance.OnPlayerMovement += StatusUpdate;
        GameEventSystem.Instance.OnPlayerClick += CostUpdate;
        //canvas.SetActive(false);
    }

    public void StatusUpdate()
    {
        if (SaveSerial.onMapMovementPoints != 0)
        {
            movePoints.text = string.Format("Move Points: {0:#0.0}", SaveSerial.onMapMovementPoints);
        }
        else
        {
            movePoints.text = string.Format("Move Points: 0");
        }

    }

    public void CostUpdate(float cost)
    {
        if (cost != 0)
        {
            potentialCost.text = string.Format("Cost: {0:#0.0}", cost);
        }
        else
        {
            potentialCost.text = string.Format("Cost: 0");
        }

    }

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnPlayerMovement -= StatusUpdate;
        GameEventSystem.Instance.OnPlayerClick -= CostUpdate;
    }
}
