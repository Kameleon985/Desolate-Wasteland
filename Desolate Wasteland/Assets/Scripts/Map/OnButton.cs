using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnButton : MonoBehaviour
{
    public void MouseEnters()
    {
        GameEventSystem.Instance.OverButtons(true);
    }

    public void MouseLeaves()
    {
        GameEventSystem.Instance.OverButtons(false);
    }
}
