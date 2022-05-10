using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    //Resources
    public static int Vitals = 10;
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
    public static int MeleeUnit = 4; //To determine
    public static int RangeUnit = 2; //To determine
    public static int EliteUnit;

    //ArmyInCamp
    public static int CampMeleeUnit; //To determine
    public static int CampRangeUnit;
    public static int CampEliteUnit;

    //Drugs
    public static int ChemA;
    public static int ChemB;
    public static int ChemC;
    public static int ChemD;

    public static int BuffA;
    public static int BuffB;
    public static int BuffC;

    public static int[] RecipeBuffA = { -1 };
    public static int[] RecipeBuffB = { -1 };
    public static int[] RecipeBuffC = { -1 };



    //public GameObject CampUI;
    //public GameObject MainUI;


    public static void SaveGame(string saveName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saves/"+ saveName+".dat");
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
        //Debug.Log("TEST UNIT SAVE " + MeleeUnit+", " + RangeUnit+", " + EliteUnit);

        //ArmyInCamp
        data.savedCampMeleeUnit = CampMeleeUnit;
        data.savedCampRangeUnit = CampRangeUnit;
        data.savedCampEliteUnit = CampEliteUnit;

        //Drugs
        data.savedChemA = ChemA;
        data.savedChemB = ChemB;
        data.savedChemC = ChemC;
        data.savedChemD = ChemD;

        data.savedBuffA = BuffA;
        data.savedBuffB = BuffB;
        data.savedBuffC = BuffC;

        data.savedRecipeBuffA = RecipeBuffA;
        data.savedRecipeBuffB = RecipeBuffB;
        data.savedRecipeBuffC = RecipeBuffC;

    bf.Serialize(file, data);
        file.Close();
        Debug.Log("Data Saved to " + Application.persistentDataPath + "/saves/"+saveName+".dat");
    }

    public static void LoadGame(string fileName)
    {
        if (File.Exists(Application.persistentDataPath
                   + "/saves/"+fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/saves/" + fileName, FileMode.Open);
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

            //ArmyInCamp
            CampMeleeUnit = data.savedCampMeleeUnit;
            CampRangeUnit = data.savedCampRangeUnit;
            CampEliteUnit = data.savedCampEliteUnit;

            //Drugs
            ChemA = data.savedChemA;
            ChemB = data.savedChemB;
            ChemC = data.savedChemC;
            ChemD = data.savedChemD;

            BuffA = data.savedBuffA;
            BuffB = data.savedBuffB;
            BuffC = data.savedBuffC;

            RecipeBuffA = data.savedRecipeBuffA;
            RecipeBuffB = data.savedRecipeBuffB;
            RecipeBuffC = data.savedRecipeBuffC;

    //UI UPDATE


            UIUpdate.Instance.UpdateRound(CurrentRound);
            UIUpdate.Instance.SetVitals(Vitals);
            UIUpdate.Instance.SetScrap(Scrap);
            UIUpdate.Instance.SetPlastic(Plastic);
            UIUpdate.Instance.SetElectronics(Electronics);

            //Camp UI UPDATE
            UICamp.Instance.updateFromSave(LabBuild, MarketBuild, BarracksBuild, ShootingRangeBuild, ArmoryBuild, HydroponicsBuild);

            Debug.Log(fileName + "loaded succesfully");
        }
        else
        {
            Debug.LogError("Tried "+Application.persistentDataPath+ "/saves/" + fileName+".dat");
            Debug.LogError("No save data!");
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SavedData.dat");

            //Resources
            Vitals = 10;
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

            //ArmyInCamp
            CampMeleeUnit = 0;
            CampRangeUnit = 0;
            CampEliteUnit = 0;

            //Drugs
            ChemA = 0;
            ChemB = 0;
            ChemC = 0;
            ChemD = 0;

            BuffA = 0;
            BuffB = 0;
            BuffC = 0;

            RecipeBuffA[0] = -1;
            RecipeBuffB[0] = -1;
            RecipeBuffC[0] = -1;

            UIUpdate.Instance.UpdateUIValues();
            UICamp.Instance.UpdateUIValues();

        }
        else
        {
            Debug.Log("No save data to delete!");
        }
    }

    public static void NewGameSetData()
    {
        

            //Resources
            Vitals = 10;
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

            //ArmyInCamp
            CampMeleeUnit = 0;
            CampRangeUnit = 0;
            CampEliteUnit = 0;

            //Drugs
            ChemA = 0;
            ChemB = 0;
            ChemC = 0;
            ChemD = 0;

            BuffA = 0;
            BuffB = 0;
            BuffC = 0;

            RecipeBuffA[0] = -1;
            RecipeBuffB[0] = -1;
            RecipeBuffC[0] = -1;

            UIUpdate.Instance.UpdateUIValues();
            UICamp.Instance.UpdateUIValues();

        
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

        //ArmyInCamp
        public int savedCampMeleeUnit;
        public int savedCampRangeUnit;
        public int savedCampEliteUnit;

        //Drugs
        public int savedChemA;
        public int savedChemB;
        public int savedChemC;
        public int savedChemD;

        public int savedBuffA;
        public int savedBuffB;
        public int savedBuffC;

        public int[] savedRecipeBuffA;
        public int[] savedRecipeBuffB;
        public int[] savedRecipeBuffC;

    }
}
