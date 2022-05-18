using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    public Color baseColor;
    public Color offsetColor;


    public override void init(int x, int y, GameObject notClickableThrough)
    {
        var isOffset = (x + y) % 2 == 1;

        if (isOffset)
        {
            offsetColor.a = 255;
            renderer.color = offsetColor;
        }
        else
        {
            baseColor.a = 255;
            renderer.color = baseColor;
        }

        this.x = x;
        this.y = y;
        gCost = int.MaxValue;
        CalculateFCost();
        previouseTile = null;
        this.notClickableThrough = notClickableThrough;
    }
}
