using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    public SaveManager manager;
    public Text btnName;

    public void setButtonName(string buttonName)
    {
        btnName.text = buttonName;
    }

    public void OnClick()
    {
        manager.load(btnName.text+"");
    }
}
