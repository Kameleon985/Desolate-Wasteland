using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyHandler : MonoBehaviour
{
    public void campIncrease()
    {
        if (SaveSerial.BarracksBuild)
        {
            SaveSerial.CampMeleeUnit += 3; //To Determine
            UICamp.Instance.campMeleeAmount.text = SaveSerial.CampMeleeUnit+"";
        }
        if (SaveSerial.ShootingRangeBuild)
        {
            SaveSerial.CampRangeUnit += 2; //To Determine
            UICamp.Instance.campRangeAmount.text = SaveSerial.CampRangeUnit + "";
        }
        if (SaveSerial.ArmoryBuild)
        {
            SaveSerial.CampEliteUnit += 1; //To Determine
            UICamp.Instance.campEliteAmount.text = SaveSerial.CampEliteUnit + "";
        }
    }    

    public void BarrackBuiltIncrease()
    {
        if (SaveSerial.BarracksBuild)
        {
            SaveSerial.CampMeleeUnit += 3; //To Determine
            UICamp.Instance.campMeleeAmount.text = SaveSerial.CampMeleeUnit + "";
        }
    }
    public void ShootingRangeBuiltIncrease()
    {
        if (SaveSerial.ShootingRangeBuild)
        {
            SaveSerial.CampRangeUnit += 2; //To Determine
            UICamp.Instance.campRangeAmount.text = SaveSerial.CampRangeUnit + "";
        }
    }
    public void ArmoryBuiltIncrease()
    {
        if (SaveSerial.ArmoryBuild)
        {
            SaveSerial.CampEliteUnit += 1; //To Determine
            UICamp.Instance.campEliteAmount.text = SaveSerial.CampEliteUnit + "";
        }
    }

    public static void ArmyCampTransfer(int amount, string armyType)
    {
        Debug.Log("ArmyCampTransfer called \n Amount: " + amount + " armyType: " + armyType);
        if (armyType.Equals("Melee"))
        {
            SaveSerial.MeleeUnit += amount;
            Debug.Log("Transferred " + amount + " of Melee Units");
            SaveSerial.CampMeleeUnit -= amount;
            UICamp.Instance.campMeleeAmount.text = SaveSerial.CampMeleeUnit + "";
            UICamp.Instance.MeleeAmount.text = SaveSerial.MeleeUnit + "";
        } else if (armyType.Equals("Range"))
        {
            SaveSerial.RangeUnit += amount;
            Debug.Log("Transferred " + amount + " of Ranged Units");
            SaveSerial.CampRangeUnit -= amount;
            UICamp.Instance.campRangeAmount.text = SaveSerial.CampRangeUnit + "";
            UICamp.Instance.RangeAmount.text = SaveSerial.RangeUnit + "";
        }
        else if (armyType.Equals("Elite"))
        {
            SaveSerial.EliteUnit += amount;
            Debug.Log("Transferred " + amount + " of Elite Units");
            SaveSerial.CampEliteUnit -= amount;
            UICamp.Instance.campEliteAmount.text = SaveSerial.CampEliteUnit + "";
            UICamp.Instance.EliteAmount.text = SaveSerial.EliteUnit + "";
        } else {
            Debug.LogError("Unknown army type in transfer attempt: ["+ armyType+"]");
        }
    }

    public void ArmyCostPerTurn()
    {
        //for each 3 melee units eat 1 vitals
        //for each 2 range units eat 1 vitals
        //for each 1 elite units eat 1 vitals

        int meleeSoldiersAmount = SaveSerial.MeleeUnit;
        int rangeSoldiersAmount = SaveSerial.RangeUnit;
        int eliteSoldiersAmount = SaveSerial.EliteUnit;
        int toEat = 0;

        for(int i = 0; i <= meleeSoldiersAmount; i += 3)
        {
            //toEat++;
            //Debug.Log("meleeEat i:"+i);
            //Debug.Log("ToEat: "+toEat);
            if(i % 3 == 0 && i!=0)
            {
                //Debug.Log("meleeEat i-1: " + i);
                
                toEat++;

                //Debug.Log("Melee toEat: " + toEat);
            }
        }
        for(int i = 0; i <= rangeSoldiersAmount; i += 2)
        {
            if(i % 2 == 0 && i!=0)
            {
                toEat++;
                //Debug.Log("Range toEat: " + toEat);
            }
        }

        for(int i = 0; i < eliteSoldiersAmount; i++)
        {
            Debug.Log(i);
            if(eliteSoldiersAmount > 0)
            {
                toEat++;
                //Debug.Log("Elite toEat: " + toEat);
            }            
        }
        int tempResult = SaveSerial.Vitals - toEat;
        //Debug.Log("This turn cost equals: " + toEat + " M: "+SaveSerial.Vitals+" = "+tempResult);
        SaveSerial.Vitals -= toEat;
        if (toEat > 0 && SaveSerial.Vitals < toEat) 
        {
            //inflict penalty
            Debug.Log("Inflict penalty for lack of Vitals");
            SaveSerial.Vitals = 0;
        }
        UIUpdate.Instance.UpdateUIValues();
    }
}
