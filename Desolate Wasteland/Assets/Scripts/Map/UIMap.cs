using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    public Text movePoints;
    public Text potentialCost;

    public void Start()
    {
        GameEventSystem.Instance.OnPlayerMovement += StatusUpdate;
        GameEventSystem.Instance.OnPlayerClick += CostUpdate;
    }

    public void StatusUpdate(PlayerData data)
    {
        if (data.movePoints != 0)
        {
            movePoints.text = string.Format("Move Points: {0:#0.0}", data.movePoints);
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
