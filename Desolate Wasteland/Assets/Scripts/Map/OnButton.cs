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

    public void EnterBattle()
    {
        /*
        location.GetComponent<Location>().SetCaptured(true);
        OnEnterLocation?.Invoke(location);
        float[] l = { location.transform.position.x, location.transform.position.y };
        SaveSerial.captured.Add(l, true);
        SceneManager.LoadScene(location.name);
        */
    }
}
