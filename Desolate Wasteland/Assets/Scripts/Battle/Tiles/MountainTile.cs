using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : Tile
{
    public override void init(int x, int y, GameObject notClickableThrough)
    {
        this.notClickableThrough = notClickableThrough;
    }
}
