using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{

    public void xd()
    {
        GameObject g = new GameObject();
        g.name = "Map";
        GameEventSystem.Instance.EnterLocation(g);
    }
}
