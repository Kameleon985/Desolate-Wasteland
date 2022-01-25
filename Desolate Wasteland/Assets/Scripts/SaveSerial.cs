using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    //Resources
    public static int Vitals = 5;
    public static int Scrap = 0;
    public static int Plastic = 0;
    public static int Electronics = 0;

    //Round
    public static int CurrentRound = 1;

    //Camp
    public static bool LabBuild;
    public static bool MarketBuild;
    public static bool BarracksBuild;
    public static bool ShootingRangeBuild;
    public static bool ArmoryBuild;
    public static bool HydroponicsBuild;

    //PlayerPosition
    //TO-DO

    //OnMapLocationsCaptured
    //TO-DO

    //PlayerArmy
    public static int MeleeUnit;
    public static int RangeUnit;
    public static int EliteUnit;

    //Drugs
    //TO-DO


    public GameObject CampUI;
    public GameObject MainUI;


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedData.dat");
        SaveData data = new SaveData();


        //Resources
        data.savedVitals = Vitals;
        data.savedScrap = Scrap;
        data.savedPlastic = Plastic;
        data.savedElectronics = Electronics;

        //Round
        data.savedCurrentRound = CurrentRound;

        //Camp
        data.savedLabBuild = LabBuild;
        data.savedMarketBuild = MarketBuild;
        data.savedBarracksBuild = BarracksBuild;
        data.savedShootingRangeBuild = ShootingRangeBuild;
        data.savedArmoryBuild = ArmoryBuild;
        data.savedHydroponicsBuild = HydroponicsBuild;

        //PlayerPosition
        //TO-DO

        //OnMapLocationsCaptured
        //TO-DO

        //PlayerArmy
        data.savedMeleeUnit = MeleeUnit;
        data.savedRangeUnit = RangeUnit;
        data.savedEliteUnit = EliteUnit;

        //Drugs
        //TO-DO

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Data Saved to " + Application.persistentDataPath + "/SavedData.dat");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                   + "/SavedData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/SavedData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            //DATA
            //Resources
            Vitals = data.savedVitals;
            Scrap = data.savedScrap;
            Plastic = data.savedPlastic;
            Electronics = data.savedElectronics;

            //Round
            CurrentRound = data.savedCurrentRound;

            //Camp
            LabBuild = data.savedLabBuild;
            MarketBuild = data.savedMarketBuild;
            BarracksBuild = data.savedBarracksBuild;
            ShootingRangeBuild = data.savedShootingRangeBuild;
            ArmoryBuild = data.savedArmoryBuild;
            HydroponicsBuild = data.savedHydroponicsBuild;

            //PlayerPosition
            //TO-DO

            //OnMapLocationsCaptured
            //TO-DO

            //PlayerArmy
            MeleeUnit = data.savedMeleeUnit;
            RangeUnit = data.savedRangeUnit;
            EliteUnit = data.savedEliteUnit;

            //Drugs
            //TO-DO

            //UI UPDATE


            UIUpdate.Instance.UpdateRound(CurrentRound);
            UIUpdate.Instance.SetVitals(Vitals);
            UIUpdate.Instance.SetScrap(Scrap);
            UIUpdate.Instance.SetPlastic(Plastic);
            UIUpdate.Instance.SetElectronics(Electronics);

            //Camp UI UPDATE
            UICamp.Instance.updateFromSave(LabBuild, MarketBuild, BarracksBuild, ShootingRangeBuild, ArmoryBuild, HydroponicsBuild);
        }
        else
        {
            Debug.LogError("No save data!");
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SavedData.dat");

            //Resources
            Vitals = 5;
            Scrap = 0;
            Plastic = 0;
            Electronics = 0;

            //Round
            CurrentRound = 1;

            //Camp
            LabBuild = false;
            MarketBuild = false;
            BarracksBuild = false;
            ShootingRangeBuild = false;
            ArmoryBuild = false;
            HydroponicsBuild = false;

            //PlayerPosition
            //TO-DO

            //OnMapLocationsCaptured
            //TO-DO

            //PlayerArmy
            MeleeUnit = 4; //To determine
            RangeUnit = 0;
            EliteUnit = 0;

            //Drugs
            //TO-DO

            UIUpdate.Instance.UpdateUIValues();
            UICamp.Instance.UpdateUIValues();

        }
        else
        {
            Debug.Log("No save data to delete!");
        }
    }



    [Serializable]
    class SaveData
    {

        //Resources
        public int savedVitals;
        public int savedScrap;
        public int savedPlastic;
        public int savedElectronics;

        //Round
        public int savedCurrentRound;

        //Camp
        public bool savedLabBuild;
        public bool savedMarketBuild;
        public bool savedBarracksBuild;
        public bool savedShootingRangeBuild;
        public bool savedArmoryBuild;
        public bool savedHydroponicsBuild;

        //PlayerPosition
        //TO-DO

        //OnMapLocationsCaptured
        //TO-DO

        //PlayerArmy
        public int savedMeleeUnit;
        public int savedRangeUnit;
        public int savedEliteUnit;

    }
}
