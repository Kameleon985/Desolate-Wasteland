using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonPropListButton : MonoBehaviour
{
    public SaveManager manager;
    public Text btnName;
    public string relatedToThisSave;
    public GameObject parentRow;

    public void setButtonName(string buttonName, string thisSave, GameObject parentRow)
    {
        btnName.text = buttonName;
        relatedToThisSave = thisSave;
        this.parentRow = parentRow;
    }

    public void OnClick()
    {
        manager.delete(relatedToThisSave, parentRow);

    }
}
