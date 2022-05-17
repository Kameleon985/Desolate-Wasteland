using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Vector2 position = new Vector2();
    public float movePoints = 10;

    public void SetMovePoints(float points)
    {
        movePoints = points;
    }
}
