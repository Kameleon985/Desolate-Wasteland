using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICamp : MonoBehaviour
{
    public GameObject campView;
    public GameObject buildingView;
    public GameObject ErrorView;
    public GameObject marketView;
    public GameObject labView;
    public GameObject craftingResultView;

    public static UICamp Instance;

    public RoundTracker roundTracker;

    bool LabBuild = false;
    bool MarketBuild = false;
    bool BarracksBuild = false;
    bool ShootingRangeBuild = false;
    bool ArmoryBuild = false;
    bool HydroponicsBuild = false;

    public Button buildLaboButton;
    public Button buildMarketButton;
    public Button buildBarracksButton;
    public Button buildShootingRangeButton;
    public Button buildArmoryButton;
    public Button buildHydroponicsButton;

    public Button goLaboButton;
    public Button goMarketButton;

    public ResourcesHandler resourcesHandler;
    public ArmyHandler armyHandler;

    public GameObject ArmyTransferView;
    public Slider armyTransferSlider;

    public Text campMeleeAmount;
    public Text MeleeAmount;
    public Text campRangeAmount;
    public Text RangeAmount;
    public Text campEliteAmount;
    public Text EliteAmount;

    public Text ChemAAmount;
    public Text ChemBAmount;
    public Text ChemCAmount;
    public Text ChemDAmount;
    public Text BuffAAmount;
    public Text BuffBAmount;
    public Text BuffCAmount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        UpdateUIValues();
    }

    public void BuildingScreenOpen()
    {
        buildingView.gameObject.SetActive(true);
    }
    public void BuildingScreenClose()
    {
        buildingView.gameObject.SetActive(false);
    }

    public void ArmyTransferScreenClose()
    {
        ArmyTransferView.gameObject.SetActive(false);
        armyTransferSlider.value = 0;
    }
    public void ArmyTransferScreenOpen()
    {
        ArmyTransferView.gameObject.SetActive(true);
    }

    public void MarketScreenOpen()
    {
        marketView.SetActive(true);
    }

    public void MarketScreenClose()
    {
        marketView.SetActive(false);
    }

    public void LaboScreenOpen()
    {
        labView.SetActive(true);
    }

    public void LaboScreenClose()
    {
        labView.SetActive(false);
    }

    public void CraftingResultScreenOpen()
    {
        craftingResultView.SetActive(true);
    }

    public void CraftingResultScreenClose()
    {
        craftingResultView.SetActive(false);
    }

    public void ErrorScreenClose()
    {
        ErrorView.gameObject.SetActive(false);
    }

    public void buildLab()
    {
        bool sufficientScrap = false;
        bool sufficientPlastic = false;
        bool sufficientElectronics = false;
        if (SaveSerial.Scrap >= 5)
        {
            sufficientScrap = true;
        }
        if (SaveSerial.Plastic >= 5)
        {
            sufficientPlastic = true;
        }
        if (SaveSerial.Electronics >= 3)
        {
            sufficientElectronics = true;
        }


        if (!LabBuild)
        {
            //Cost
            if (sufficientScrap && sufficientPlastic && sufficientElectronics)
            {

                resourcesHandler.RemoveResources(5, 5, 3);
                LabBuild = true;
                SaveSerial.LabBuild = LabBuild;
                Debug.Log("Labo Built");
                buildLaboButton.interactable = false;
                goLaboButton.interactable = true;

            }
            else if (!sufficientScrap || !sufficientPlastic || !sufficientElectronics)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error LaboBuild something weird happened to resources");
            }
        }
        else
        {
            buildLaboButton.interactable = false;
            goLaboButton.interactable = true;
        }

        

    }

    public void buildMarket()
    {

        bool sufficientScrap = false;
        bool sufficientPlastic = false;
        if (SaveSerial.Scrap >= 5)
        {
            sufficientScrap = true;
        }
        if (SaveSerial.Plastic >= 3)
        {
            sufficientPlastic = true;
        }

        if (!MarketBuild)
        {
            //Cost
            if (sufficientScrap && sufficientPlastic)
            {
                //Sets
                resourcesHandler.RemoveResources(5, 3, 0);
                MarketBuild = true;
                SaveSerial.MarketBuild = MarketBuild;
                buildMarketButton.interactable = false;
                goMarketButton.interactable = true;

            }
            else if (!sufficientScrap || !sufficientPlastic)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error MarketBuild something weird happened to resources");
            }

        }
        else
        {
            buildMarketButton.interactable = false;
            goMarketButton.interactable = true;
        }

        

    }
    public void buildBarracks()
    {

        bool sufficientScrap = false;
        bool sufficientPlastic = false;
        if (SaveSerial.Scrap >= 5)
        {
            sufficientScrap = true;
        }
        if (SaveSerial.Plastic >= 3)
        {
            sufficientPlastic = true;
        }

        if (!BarracksBuild)
        {
            //Cost
            //IF
            if (sufficientScrap && sufficientPlastic)
            {
                resourcesHandler.RemoveResources(5, 3, 0);
                BarracksBuild = true;
                SaveSerial.BarracksBuild = BarracksBuild;
                buildBarracksButton.interactable = false;
                armyHandler.BarrackBuiltIncrease();


            }
            else if (!sufficientScrap || !sufficientPlastic)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error BarracksBuild something weird happened to resources");
            }

        }
        else
        {
            buildBarracksButton.interactable = false;
            
        }

    }
    public void buildShootingRange()
    {
        bool sufficientScrap = false;
        bool sufficientPlastic = false;
        bool sufficientElectronics = false;
        if(SaveSerial.Scrap >= 5)
        {
            sufficientScrap = true;
        }
        if (SaveSerial.Plastic >= 5)
        {
            sufficientPlastic = true;
        }
        if (SaveSerial.Electronics >= 3)
        {
            sufficientElectronics = true;
        }


        if (!ShootingRangeBuild)
        {
            //Cost
            if (sufficientScrap && sufficientPlastic && sufficientElectronics)
            {
                resourcesHandler.RemoveResources(5, 5, 3);
                ShootingRangeBuild = true;
                SaveSerial.ShootingRangeBuild = ShootingRangeBuild;
                buildShootingRangeButton.interactable = false;
                armyHandler.ShootingRangeBuiltIncrease();
            }
            else if(!sufficientScrap || !sufficientPlastic || !sufficientElectronics)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error ShootingRangeBuild something weird happened to resources");
            }
        }
        else
        {
            buildShootingRangeButton.interactable = false;
            
        }

    }
    public void buildArmory()
    {
        bool sufficientScrap = false;
        bool sufficientPlastic = false;
        bool sufficientElectronics = false;
        if (SaveSerial.Scrap >= 10)
        {
            sufficientScrap = true;
        }
        if (SaveSerial.Plastic >= 5)
        {
            sufficientPlastic = true;
        }
        if (SaveSerial.Electronics >= 7)
        {
            sufficientElectronics = true;
        }

        if (!ArmoryBuild)
        {
            //Cost
            if (sufficientScrap && sufficientPlastic && sufficientElectronics)
            {
                resourcesHandler.RemoveResources(10, 5, 7);
                ArmoryBuild = true;
                SaveSerial.ArmoryBuild = ArmoryBuild;
                buildArmoryButton.interactable = false;
                armyHandler.ArmoryBuiltIncrease();
            }
            else if (!sufficientScrap || !sufficientPlastic || !sufficientElectronics)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error ArmoryBuild something weird happened to resources");
            }
        }
        else
        {
            buildArmoryButton.interactable = false;
            
        }

    }
    public void buildHydroponics()
    {
        if (!HydroponicsBuild)
        {
            roundTracker.isHydroponicsBuilt = false;

            bool sufficientScrap = false;
            bool sufficientPlastic = false;
            bool sufficientElectronics = false;
            if (SaveSerial.Scrap >= 10)
            {
                sufficientScrap = true;
            }
            if (SaveSerial.Plastic >= 15)
            {
                sufficientPlastic = true;
            }
            if (SaveSerial.Electronics >= 3)
            {
                sufficientElectronics = true;
            }

            //Cost
            if (sufficientScrap && sufficientPlastic && sufficientElectronics)
            {
                resourcesHandler.RemoveResources(10, 15, 3);
                HydroponicsBuild = true;
                SaveSerial.HydroponicsBuild = HydroponicsBuild;
                buildHydroponicsButton.interactable = false;
                roundTracker.isHydroponicsBuilt = true;
            }
            else if (!sufficientScrap || !sufficientPlastic || !sufficientElectronics)
            {
                ErrorView.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Error HydroponicsBuild something weird happened to resources");
            }
        }
        else
        {
            buildHydroponicsButton.interactable = false;
            roundTracker.isHydroponicsBuilt = true;            
        }

    }

    public void updateFromSave(bool LoadLabBuild, bool LoadMarketBuild, bool LoadBarracksBuild, bool LoadShootingRange, bool LoadArmoryBuild, bool LoadHydroponicsBuild)
    {

        LabBuild = LoadLabBuild;
        MarketBuild = LoadMarketBuild;
        BarracksBuild = LoadBarracksBuild;
        ShootingRangeBuild = LoadShootingRange;
        ArmoryBuild = LoadArmoryBuild;
        HydroponicsBuild = LoadHydroponicsBuild;

        UpdateUIValues();
    }

    void updateLabBuild()
    {        
            buildLaboButton.interactable = !(SaveSerial.LabBuild);
            goLaboButton.interactable = SaveSerial.LabBuild;
    }
    void updateMarketBuild()
    {
            buildMarketButton.interactable = !(SaveSerial.MarketBuild);
            goMarketButton.interactable = SaveSerial.MarketBuild;
    }
    void updateBarracksBuild()
    {
        buildBarracksButton.interactable = !(SaveSerial.BarracksBuild);
    }
    void updateShootingRangeBuild()
    {
        buildShootingRangeButton.interactable = !(SaveSerial.ShootingRangeBuild);
    }
    void updateArmoryBuild()
    {
        buildArmoryButton.interactable = !(SaveSerial.ArmoryBuild);
    }
    void updateHydroponicsBuild()
    {
        buildHydroponicsButton.interactable = !(SaveSerial.HydroponicsBuild);
    }

    void updateCampArmy()
    {
        SetCampMelee(SaveSerial.CampMeleeUnit);
        SetCampRange(SaveSerial.CampRangeUnit);
        SetCampElite(SaveSerial.CampEliteUnit);
    }

    void updatePlayerArmy()
    {
        MeleeAmount.text = SaveSerial.MeleeUnit+ "";
        RangeAmount.text = SaveSerial.RangeUnit+ "";
        EliteAmount.text = SaveSerial.EliteUnit+ "";
    }

    void updateLabo()
    {
        BuffAAmount.text = "BuffA: " + SaveSerial.BuffA;
        BuffBAmount.text = "BuffB: " + SaveSerial.BuffB;
        BuffCAmount.text = "BuffC: " + SaveSerial.BuffC;

        ChemAAmount.text = "ChemA: " + SaveSerial.ChemA;
        ChemBAmount.text = "ChemB: " + SaveSerial.ChemB;
        ChemCAmount.text = "ChemC: " + SaveSerial.ChemC;
        ChemDAmount.text = "ChemD: " + SaveSerial.ChemD;
    }

    public void SetCampMelee(int number)
    {
        campMeleeAmount.text = number + "";
        SaveSerial.CampMeleeUnit = number;
    }
    public void SetCampRange(int number)
    {
        campRangeAmount.text = number + "";
        SaveSerial.CampRangeUnit = number;
    }
    public void SetCampElite(int number)
    {
        campEliteAmount.text = number + "";
        SaveSerial.CampEliteUnit = number;
    }

    public void UpdateUIValues()
    {
        updateLabBuild();
        updateMarketBuild();
        updateBarracksBuild();
        updateShootingRangeBuild();
        updateArmoryBuild();
        updateHydroponicsBuild();

        updateCampArmy();
        updatePlayerArmy();

        updateLabo();
    }


    public void ReturnToMap()
    {
        SceneManager.LoadScene("Map&UI");
    }
}
