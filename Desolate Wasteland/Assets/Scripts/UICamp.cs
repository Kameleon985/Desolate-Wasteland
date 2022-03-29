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
        if (!LabBuild)
        {
            //Cost
            if (SaveSerial.Scrap < 5)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 5) 
                {
                    //Debug.Log("Not enough Materials");
                    ErrorView.gameObject.SetActive(true);
                    if (SaveSerial.Electronics < 3)
                    {
                        //Debug.Log("Not enough Materials");
                        ErrorView.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                //Sets
                resourcesHandler.RemoveResources(5, 5, 3);
                LabBuild = true;
                SaveSerial.LabBuild = LabBuild;
                Debug.Log("Labo Built");
                buildLaboButton.interactable = false;
                goLaboButton.interactable = true;
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
        if (!MarketBuild)
        {
            //Cost
            if (SaveSerial.Scrap < 5)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 3) 
                {
                    ErrorView.gameObject.SetActive(true);
                }
            }
            else
            {
                //Sets
                resourcesHandler.RemoveResources(5, 3, 0);
                MarketBuild = true;
                SaveSerial.MarketBuild = MarketBuild;
                buildMarketButton.interactable = false;
                goMarketButton.interactable = true;
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
        if (!BarracksBuild)
        {
            //Cost
            //IF
            if (SaveSerial.Scrap < 5)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 3)
                {
                    ErrorView.gameObject.SetActive(true);
                }
            }
            else
            {
                resourcesHandler.RemoveResources(5, 3, 0);
                BarracksBuild = true;
                SaveSerial.BarracksBuild = BarracksBuild;
                buildBarracksButton.interactable = false;
                armyHandler.BarrackBuiltIncrease();
            }
        }
        else
        {
            buildBarracksButton.interactable = false;
            //SpawnUNITS - TO-DO
        }

    }
    public void buildShootingRange()
    {
        if (!ShootingRangeBuild)
        {
            //Cost
            if (SaveSerial.Scrap < 5)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 5)
                {
                    ErrorView.gameObject.SetActive(true);
                    if (SaveSerial.Electronics < 3)
                    {
                        ErrorView.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                resourcesHandler.RemoveResources(5, 5, 3);
                ShootingRangeBuild = true;
                SaveSerial.ShootingRangeBuild = ShootingRangeBuild;
                buildShootingRangeButton.interactable = false;
                armyHandler.ShootingRangeBuiltIncrease();
            }
        }
        else
        {
            buildShootingRangeButton.interactable = false;
            //SpawnUnits - To-Do
        }

    }
    public void buildArmory()
    {
        if (!ArmoryBuild)
        {
            //Cost
            if (SaveSerial.Scrap < 10)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 5)
                {
                    ErrorView.gameObject.SetActive(true);
                    if (SaveSerial.Electronics < 7)
                    {
                        ErrorView.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                resourcesHandler.RemoveResources(10, 5, 7);
                ArmoryBuild = true;
                SaveSerial.ArmoryBuild = ArmoryBuild;
                buildArmoryButton.interactable = false;
                armyHandler.ArmoryBuiltIncrease();
            }
        }
        else
        {
            buildArmoryButton.interactable = false;
            //To-Do Spawn Units
        }

    }
    public void buildHydroponics()
    {
        if (!HydroponicsBuild)
        {
            //Cost
            if (SaveSerial.Scrap < 10)
            {
                //Debug.Log("Not enough Materials");
                ErrorView.gameObject.SetActive(true);
                if (SaveSerial.Plastic < 15)
                {
                    ErrorView.gameObject.SetActive(true);
                    if (SaveSerial.Electronics < 3)
                    {
                        ErrorView.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                resourcesHandler.RemoveResources(10, 15, 3);
                HydroponicsBuild = true;
                SaveSerial.HydroponicsBuild = HydroponicsBuild;
                buildHydroponicsButton.interactable = false;
            }
        }
        else
        {
            buildHydroponicsButton.interactable = false;
            //To-Do Steady growth of Vitals
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
