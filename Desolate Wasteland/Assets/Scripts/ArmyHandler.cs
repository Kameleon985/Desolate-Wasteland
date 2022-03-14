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

}
