using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteroidCrafting : MonoBehaviour
{
    private int[] RecipeBuffA;
    private int[] RecipeBuffB;
    private int[] RecipeBuffC;

    public int costChemD = 2;

    public Toggle craftBuffA;
    public Toggle craftBuffB;
    public Toggle craftBuffC;

    public Slider sliderChemA;
    public Slider sliderChemB;
    public Slider sliderChemC;

    public Button commitCrafting;

    private Text textSliderValueA;
    private Text textSliderValueB;
    private Text textSliderValueC;

    private void Awake()
    {
        Debug.Log("Awake Called");
        if (SaveSerial.RecipeBuffA[0] == -1)
        {
            SaveSerial.RecipeBuffA = GenerateRecipeBuffA();
        }
        else
        {
            RecipeBuffA = SaveSerial.RecipeBuffA;
            Debug.Log("RecipeA: A" + RecipeBuffA[0] + ", B" + RecipeBuffA[1] + ", C" + RecipeBuffA[2] + ", D" + RecipeBuffA[3]);
        }
        if (SaveSerial.RecipeBuffB[0] == -1)
        {
            SaveSerial.RecipeBuffB = GenerateRecipeBuffB();
        }
        else
        {
            RecipeBuffB = SaveSerial.RecipeBuffB;
            Debug.Log("RecipeB: A" + RecipeBuffB[0] + ", B" + RecipeBuffB[1] + ", C" + RecipeBuffB[2] + ", D" + RecipeBuffB[3]);
        }
        if (SaveSerial.RecipeBuffC[0] == -1)
        {
            SaveSerial.RecipeBuffC = GenerateRecipeBuffC();
        }
        else
        {
            RecipeBuffC = SaveSerial.RecipeBuffC;
            Debug.Log("RecipeC: A" + RecipeBuffC[0] + ", B" + RecipeBuffC[1] + ", C" + RecipeBuffC[2] + ", D" + RecipeBuffC[3]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textSliderValueA = sliderChemA.transform.Find("CurrentValue").GetComponent<Text>();
        textSliderValueB = sliderChemB.transform.Find("CurrentValue").GetComponent<Text>();
        textSliderValueC = sliderChemC.transform.Find("CurrentValue").GetComponent<Text>();
        ShowSliderValueA();
        ShowSliderValueB();
        ShowSliderValueC();

        
    }
    public void ShowSliderValueA()
    {
        string sliderText = sliderChemA.value+"";
        textSliderValueA.text = sliderText;
    }
    public void ShowSliderValueB()
    {
        string sliderText = sliderChemB.value + "";
        textSliderValueB.text = sliderText;
    }
    public void ShowSliderValueC()
    {
        string sliderText = sliderChemC.value + "";
        textSliderValueC.text = sliderText;
    }

    // Update is called once per frame
    void Update()
    {
        if ((craftBuffA.isOn || craftBuffB.isOn || craftBuffC.isOn) && SaveSerial.ChemD >= costChemD)
        {
            commitCrafting.interactable = true;
        }
        else
        {
            commitCrafting.interactable = false;
        }

        sliderChemA.maxValue = SaveSerial.ChemA;
        sliderChemB.maxValue = SaveSerial.ChemB;
        sliderChemC.maxValue = SaveSerial.ChemC;

        sliderChemA.transform.Find("Max").GetComponent<Text>().text = sliderChemA.maxValue + "";
        sliderChemB.transform.Find("Max").GetComponent<Text>().text = sliderChemB.maxValue + "";
        sliderChemC.transform.Find("Max").GetComponent<Text>().text = sliderChemC.maxValue + "";
    }

    public void Craft()
    {
        int userCostA = (int)sliderChemA.value;
        int userCostB = (int)sliderChemB.value;
        int userCostC = (int)sliderChemC.value;

        if (craftBuffA.isOn)
        {
            if (userCostA == RecipeBuffA[0] && userCostB == RecipeBuffA[1] && userCostC == RecipeBuffA[2]) {
                Debug.Log("Success in creation BuffA");
                SaveSerial.BuffA += 1;
            }
            else
            {
                Debug.Log("Bad proportions - Resources got wasted");
            }
        }

        if (craftBuffB.isOn)
        {
            if (userCostA == RecipeBuffB[0] && userCostB == RecipeBuffB[1] && userCostC == RecipeBuffB[2])
            {
                Debug.Log("Success in creation BuffB");
                SaveSerial.BuffB += 1;
            }
            else
            {
                Debug.Log("Bad proportions - Resources got wasted");
            }
        }

        if (craftBuffC.isOn)
        {
            if (userCostA == RecipeBuffC[0] && userCostB == RecipeBuffC[1] && userCostC == RecipeBuffC[2])
            {
                Debug.Log("Success in creation BuffC");
                SaveSerial.BuffC += 1;
            }
            else
            {
                Debug.Log("Bad proportions - Resources got wasted");
            }
        }

        SaveSerial.ChemA -= userCostA;
        SaveSerial.ChemB -= userCostB;
        SaveSerial.ChemC -= userCostC;
        SaveSerial.ChemD -= costChemD;

        UICamp.Instance.UpdateUIValues();
    }

    public int[] GenerateRecipeBuffA()
    {
        int controlSum = 6; //To Determine
        int costChemA = Random.Range(3, 5); //
        int costChemB;
        if(costChemA == 3)
        {
            costChemB = Random.Range(0, 4); //
        }
        else
        {
            costChemB = Random.Range(0, 3); //
        }
        
        int costChemC = 0;
        if(costChemA+costChemB<controlSum)
        {
            costChemC = controlSum - costChemA - costChemB;
            //RecipeBuffA = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC+";ChemD:" + costChemD;
            RecipeBuffA = new int[] { costChemA, costChemB, costChemC, costChemD};
        }
        //RecipeBuffA = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
        RecipeBuffA = new int[] { costChemA, costChemB, costChemC, costChemD };

        Debug.Log("RecipeA: A" + RecipeBuffA[0] + ", B" + RecipeBuffA[1] + ", C" + RecipeBuffA[2] + ", D" + RecipeBuffA[3]);
        return RecipeBuffA;
    }

    public int[] GenerateRecipeBuffB()
    {
        int controlSum = 8; //To Determine
        
        int costChemB = Random.Range(3, 7); //
        int costChemA = Random.Range(0, controlSum-costChemB+1); //

        int costChemC = 0;
        if (costChemA + costChemB < controlSum)
        {
            costChemC = controlSum - costChemA - costChemB;
            //RecipeBuffB = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
            RecipeBuffB = new int[] { costChemA, costChemB, costChemC, costChemD };
        }
        //RecipeBuffB = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
        RecipeBuffB = new int[] { costChemA, costChemB, costChemC, costChemD };

        Debug.Log("RecipeB: A"+RecipeBuffB[0]+", B"+RecipeBuffB[1]+", C"+RecipeBuffB[2]+", D"+RecipeBuffB[3]);
        return RecipeBuffB;
    }

    public int[] GenerateRecipeBuffC()
    {
        int controlSum = 8; //To Determine

        int costChemA = Random.Range(1, 5); //
        int costChemB = Random.Range(1, controlSum - costChemA - 3); //

        int costChemC = 0;
        if (costChemA + costChemB < controlSum)
        {
            costChemC = controlSum - costChemA - costChemB;
            //RecipeBuffC = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
            RecipeBuffC = new int[] { costChemA, costChemB, costChemC, costChemD };
        }
        //RecipeBuffC = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
        RecipeBuffC = new int[] { costChemA, costChemB, costChemC, costChemD };

        Debug.Log("RecipeC: A" + RecipeBuffC[0] + ", B" + RecipeBuffC[1] + ", C" + RecipeBuffC[2] + ", D" + RecipeBuffC[3]);
        return RecipeBuffC;
    }


    public void DebugAddChems() // DEBUG TO REMOVE
    {
        SaveSerial.ChemA = 99;
        SaveSerial.ChemB = 99;
        SaveSerial.ChemC = 99;
        SaveSerial.ChemD = 99;
        UICamp.Instance.UpdateUIValues();
    }


}
