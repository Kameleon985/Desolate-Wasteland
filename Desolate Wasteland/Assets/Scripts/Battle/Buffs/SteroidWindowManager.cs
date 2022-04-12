using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteroidWindowManager : MonoBehaviour
{

    public GameObject steroidManagerWindow;

    public void switchWindowActive()
    {
        steroidManagerWindow.SetActive(!steroidManagerWindow.activeSelf);
    }
}
