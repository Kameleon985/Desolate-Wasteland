using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    public Text movePoints;

    public void Start()
    {
        GameEventSystem.Instance.OnPlayerMovement += StatusUpdate;
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

    private void OnDestroy()
    {
        GameEventSystem.Instance.OnPlayerMovement -= StatusUpdate;
    }
}
