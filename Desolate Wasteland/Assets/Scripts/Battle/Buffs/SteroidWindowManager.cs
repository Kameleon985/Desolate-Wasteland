using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteroidWindowManager : MonoBehaviour
{

    public GameObject steroidManagerWindow;
    public Toggle MeleeToggle;
    public Toggle RangeToggle;
    public Toggle EliteToggle;

    public Toggle BuffAToggle;
    public Toggle BuffBToggle;
    public Toggle BuffCToggle;

    public Button administerBuffs;

    public Text BuffAAmount;
    public Text BuffBAmount;
    public Text BuffCAmount;

    private void Start()
    {
        administerBuffs.interactable = false;
        MeleeToggle.isOn = false;
        RangeToggle.isOn = false;
        EliteToggle.isOn = false;

        BuffAToggle.isOn = false;
        BuffBToggle.isOn = false;
        BuffCToggle.isOn = false;
    }

    private void Update()
    {
        if(SaveSerial.BuffA > 0)
        {
            BuffAToggle.interactable = true;
        }
        else
        {
            BuffAToggle.interactable = false;
        }
        if (SaveSerial.BuffB > 0)
        {
            BuffBToggle.interactable = true;
        }
        else
        {
            BuffBToggle.interactable = false;
        }
        if (SaveSerial.BuffC > 0)
        {
            BuffCToggle.interactable = true;
        }
        else
        {
            BuffCToggle.interactable = false;
        }

        if ((MeleeToggle.isOn || RangeToggle.isOn || EliteToggle.isOn) && (BuffAToggle.isOn || BuffBToggle.isOn || BuffCToggle.isOn))
        {
            administerBuffs.interactable = true;
        }
        else
        {
            administerBuffs.interactable = false;
        }
    }

    public void SwitchWindowActive()
    {
        steroidManagerWindow.SetActive(!steroidManagerWindow.activeSelf);
    }

    public void AdministerBuffsToUnit()
    {
        if (MeleeToggle.isOn)
        {
            if (BuffAToggle.isOn)
            {
                MeleeUnit.buffAGiven = true;
                SaveSerial.BuffA -= 1;
                Debug.Log("Melee Unit : BuffA" +
                    " Increased maxHealth");//What to decrease?
            }else if (BuffBToggle.isOn)
            {
                MeleeUnit.buffBGiven = true;
                SaveSerial.BuffB -= 1;
                Debug.Log("Melee Unit : BuffB" +
                    " Increased attackDamage");//What to decrease?
            }else if (BuffCToggle.isOn)
            {
                MeleeUnit.buffCGiven = true;
                SaveSerial.BuffC -= 1;
                Debug.Log("Melee Unit : BuffC" +
                    " Increased initiative");//What to decrease?
            }
            else
            {
                Debug.LogError("Melee Unit : BUFF-EXCEPTION");
            }

            MeleeToggle.interactable = false;
        }
        else if(RangeToggle.isOn)
        {
            if (BuffAToggle.isOn)
            {
                RangedUnit.buffAGiven = true;
                SaveSerial.BuffA -= 1;
                Debug.Log("Ranged Unit : BuffA" +
                    " Increased maxHealth");//What to decrease?
            }
            else if (BuffBToggle.isOn)
            {
                RangedUnit.buffBGiven = true;
                SaveSerial.BuffB -= 1;
                Debug.Log("Ranged Unit : BuffB" +
                    " Increased attackDamage");//What to decrease?
            }
            else if (BuffCToggle.isOn)
            {
                RangedUnit.buffCGiven = true;
                SaveSerial.BuffC -= 1;
                Debug.Log("Ranged Unit : BuffC" +
                    " Increased initiative");//What to decrease?
            }
            else
            {
                Debug.LogError("Ranged Unit : BUFF-EXCEPTION");
            }
            RangeToggle.interactable = false;
        }
        else if (EliteToggle.isOn)
        {
            if (BuffAToggle.isOn)
            {
                EliteUnit.buffAGiven = true;
                SaveSerial.BuffA -= 1;
                Debug.Log("Elite Unit : BuffA" +
                    " Increased maxHealth");//What to decrease?
            }
            else if (BuffBToggle.isOn)
            {
                EliteUnit.buffBGiven = true;
                SaveSerial.BuffB -= 1;
                Debug.Log("Elite Unit : BuffB" +
                    " Increased attackDamage");//What to decrease?
            }
            else if (BuffCToggle.isOn)
            {
                EliteUnit.buffCGiven = true;
                SaveSerial.BuffC -= 1;
                Debug.Log("Elite Unit : BuffC" +
                    " Increased initiative");//What to decrease?
            }
            else
            {
                Debug.LogError("Elite Unit : BUFF-EXCEPTION");
            }

            EliteToggle.interactable = false;
        }
        else
        {
            Debug.LogError("Unit-EXCEPTION : BUFFING");
        }

        MeleeToggle.isOn = false;
        RangeToggle.isOn = false;
        EliteToggle.isOn = false;

        BuffAToggle.isOn = false;
        BuffBToggle.isOn = false;
        BuffCToggle.isOn = false;

        

        UpdateBuffAmountsUI();

    }

    void UpdateBuffAmountsUI()
    {
        BuffAAmount.text = "" + SaveSerial.BuffA;
        BuffBAmount.text = "" + SaveSerial.BuffB;
        BuffCAmount.text = "" + SaveSerial.BuffC;
    }

    public void DebugAddBuffs() // DEBUG TO REMOVE
    {
        SaveSerial.BuffA = 5;
        SaveSerial.BuffB = 5;
        SaveSerial.BuffC = 5;
        UpdateBuffAmountsUI();
    }

}
