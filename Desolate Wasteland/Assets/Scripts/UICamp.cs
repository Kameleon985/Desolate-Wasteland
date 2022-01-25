using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICamp : MonoBehaviour
{
    public GameObject campView;
    public GameObject buildingView;
    public GameObject marketView;
    public GameObject labView;
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

    public void buildLab()
    {
        if (!LabBuild)
        {
            //Cost
            //IF
            LabBuild = true;
            SaveSerial.LabBuild = LabBuild;
            Debug.Log("Labo Built");
            buildLaboButton.interactable = false;
        }
        else
        {
            buildLaboButton.interactable = false;
        }
        
    }

    public void buildMarket()
    {
        if (!MarketBuild)
        {
            //Cost
            //IF
            MarketBuild = true;
            SaveSerial.MarketBuild = MarketBuild;
            buildMarketButton.interactable = false;
        }
        else
        {
            buildMarketButton.interactable = false;
        }

    }
    public void buildBarracks()
    {
        if (!BarracksBuild)
        {
            //Cost
            //IF
            BarracksBuild = true;
            SaveSerial.BarracksBuild = BarracksBuild;
            buildBarracksButton.interactable = false;
        }
        else
        {
            buildBarracksButton.interactable = false;
        }

    }
    public void buildShootingRange()
    {
        if (!ShootingRangeBuild)
        {
            //Cost
            //IF
            ShootingRangeBuild = true;
            SaveSerial.ShootingRangeBuild = ShootingRangeBuild;
            buildShootingRangeButton.interactable = false;
        }
        else
        {
            buildShootingRangeButton.interactable = false;
        }

    }
    public void buildArmory()
    {
        if (!ArmoryBuild)
        {
            //Cost
            //IF
            ArmoryBuild = true;
            SaveSerial.ArmoryBuild = ArmoryBuild;
            buildArmoryButton.interactable = false;
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
            //Cost
            //IF
            HydroponicsBuild = true;
            SaveSerial.HydroponicsBuild = HydroponicsBuild;
            buildHydroponicsButton.interactable = false;
        }
        else
        {
            buildHydroponicsButton.interactable = false;
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
    }
    void updateMarketBuild()
    {
            buildMarketButton.interactable = !(SaveSerial.MarketBuild);
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

    public void UpdateUIValues()
    {
        updateLabBuild();
        updateMarketBuild();
        updateBarracksBuild();
        updateShootingRangeBuild();
        updateArmoryBuild();
        updateHydroponicsBuild();
    }


    public void ReturnToMap()
    {
        SceneManager.LoadScene("Map&UI");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
