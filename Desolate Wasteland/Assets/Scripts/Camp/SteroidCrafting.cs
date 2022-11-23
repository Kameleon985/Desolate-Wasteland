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

    public Text suggestChemA;
    public Text suggestChemB;
    public Text suggestChemC;
    public Text result;


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

            sliderChemA.interactable = true;
            sliderChemB.interactable = true;
            sliderChemC.interactable = true;
        }
        else
        {
            commitCrafting.interactable = false;

            sliderChemA.interactable = false;
            sliderChemB.interactable = false;
            sliderChemC.interactable = false;
        }


        if (craftBuffA.isOn) {
            if (SaveSerial.ChemA > RecipeBuffA[4])
            {
                sliderChemA.maxValue = RecipeBuffA[4];
            }
            else
            {
                sliderChemA.maxValue = SaveSerial.ChemA;
            }
            if (SaveSerial.ChemB > RecipeBuffA[4])
            {
                sliderChemB.maxValue = RecipeBuffA[4];
            }
            else
            {
                sliderChemB.maxValue = SaveSerial.ChemB;
            }
            if (SaveSerial.ChemC > RecipeBuffA[4])
            {
                sliderChemC.maxValue = RecipeBuffA[4];
            }
            else
            {
                sliderChemC.maxValue = SaveSerial.ChemC;
            }
        }

        if (craftBuffB.isOn)
        {
            if (SaveSerial.ChemA > RecipeBuffB[4])
            {
                sliderChemA.maxValue = RecipeBuffB[4];
            }
            else
            {
                sliderChemA.maxValue = SaveSerial.ChemA;
            }
            if (SaveSerial.ChemB > RecipeBuffB[4])
            {
                sliderChemB.maxValue = RecipeBuffB[4];
            }
            else
            {
                sliderChemB.maxValue = SaveSerial.ChemB;
            }
            if (SaveSerial.ChemC > RecipeBuffB[4])
            {
                sliderChemC.maxValue = RecipeBuffB[4];
            }
            else
            {
                sliderChemC.maxValue = SaveSerial.ChemC;
            }
        }

        if (craftBuffC.isOn)
        {
            if (SaveSerial.ChemA > RecipeBuffC[4])
            {
                sliderChemA.maxValue = RecipeBuffC[4];
            }
            else
            {
                sliderChemA.maxValue = SaveSerial.ChemA;
            }
            if (SaveSerial.ChemB > RecipeBuffC[4])
            {
                sliderChemB.maxValue = RecipeBuffC[4];
            }
            else
            {
                sliderChemB.maxValue = SaveSerial.ChemB;
            }
            if (SaveSerial.ChemC > RecipeBuffC[4])
            {
                sliderChemC.maxValue = RecipeBuffC[4];
            }
            else
            {
                sliderChemC.maxValue = SaveSerial.ChemC;
            }
        }


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
                //Debug.Log("Success in creation BuffA");
                result.text = "Udało się wyprodukować BuffA";
                SaveSerial.BuffA += 1;
            }
            else
            {
                //Debug.Log("Bad proportions - Resources got wasted");
                result.text = "Złe proporcje - Zmarnowano składniki";
            }

            if(userCostA > RecipeBuffA[0])
            {
                //Debug.Log("Too much ChemA, try a little less next time");
                suggestChemA.text = "Za dużo ChemA, spróbuj trochę mniej.";
            }
            else if(userCostA < RecipeBuffA[0])
            {
                //Debug.Log("Not enough ChemA, try a little more next time");
                suggestChemA.text = "Za mało ChemA, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemA");
                suggestChemA.text = "Idealna ilość ChemA!";
            }



            if (userCostB > RecipeBuffA[1])
            {
                //Debug.Log("Too much ChemB, try a little less next time");
                suggestChemB.text = "Za dużo ChemB, spróbuj trochę mniej.";
            }
            else if (userCostB < RecipeBuffA[1])
            {
                //Debug.Log("Not enough ChemB, try a little more next time");
                suggestChemB.text = "Za mało ChemB, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemB");
                suggestChemB.text = "Idealna ilość ChemB!";
            }


            if (userCostC > RecipeBuffA[2])
            {
                //Debug.Log("Too much ChemC, try a little less next time");
                suggestChemC.text = "Za dużo ChemC, spróbuj trochę mniej.";
            }
            else if (userCostC < RecipeBuffA[2])
            {
                //Debug.Log("Not enough ChemC, try a little more next time");
                suggestChemC.text = "Za mało ChemC, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemC");
                suggestChemC.text = "Idealna ilość ChemC!";
            }
        }

        if (craftBuffB.isOn)
        {
            if (userCostA == RecipeBuffB[0] && userCostB == RecipeBuffB[1] && userCostC == RecipeBuffB[2])
            {
                //Debug.Log("Success in creation BuffB");
                result.text = "Udało się wyprodukować BuffB";
                SaveSerial.BuffB += 1;
            }
            else
            {
                //Debug.Log("Bad proportions - Resources got wasted");
                result.text = "Złe proporcje - Zmarnowano składniki";
            }

            if (userCostA > RecipeBuffB[0])
            {
                //Debug.Log("Too much ChemA, try a little less next time");
                suggestChemA.text = "Za dużo ChemA, spróbuj trochę mniej.";
            }
            else if (userCostA < RecipeBuffB[0])
            {
                //Debug.Log("Not enough ChemA, try a little more next time");
                suggestChemA.text = "Za mało ChemA, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemA");
                suggestChemA.text = "Idealna ilość ChemA!";
            }



            if (userCostB > RecipeBuffB[1])
            {
                //Debug.Log("Too much ChemB, try a little less next time");
                suggestChemB.text = "Za dużo ChemB, spróbuj trochę mniej.";
            }
            else if (userCostB < RecipeBuffB[1])
            {
                //Debug.Log("Not enough ChemB, try a little more next time");
                suggestChemB.text = "Za mało ChemB, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemB");
                suggestChemB.text = "Idealna ilość ChemB!";
            }


            if (userCostC > RecipeBuffB[2])
            {
                //Debug.Log("Too much ChemC, try a little less next time");
                suggestChemC.text = "Za dużo ChemC, spróbuj trochę mniej.";
            }
            else if (userCostC < RecipeBuffB[2])
            {
                //Debug.Log("Not enough ChemC, try a little more next time");
                suggestChemC.text = "Za mało ChemC, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemC");
                suggestChemC.text = "Idealna ilość ChemC!";
            }
        }

        if (craftBuffC.isOn)
        {
            if (userCostA == RecipeBuffC[0] && userCostB == RecipeBuffC[1] && userCostC == RecipeBuffC[2])
            {
                //Debug.Log("Success in creation BuffC");
                SaveSerial.BuffC += 1;
                result.text = "Udało się wyprodukować BuffC";
            }
            else
            {
                //Debug.Log("Bad proportions - Resources got wasted");
                result.text = "Złe proporcje - Zmarnowano składniki";
            }


            if (userCostA > RecipeBuffC[0])
            {
                //Debug.Log("Too much ChemA, try a little less next time");
                suggestChemA.text = "Za dużo ChemA, spróbuj trochę mniej.";
            }
            else if (userCostA < RecipeBuffC[0])
            {
                //Debug.Log("Not enough ChemA, try a little more next time");
                suggestChemA.text = "Za mało ChemA, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemA");
                suggestChemA.text = "Idealna ilość ChemA!";
            }



            if (userCostB > RecipeBuffC[1])
            {
                //Debug.Log("Too much ChemB, try a little less next time");
                suggestChemB.text = "Za dużo ChemB, spróbuj trochę mniej.";
            }
            else if (userCostB < RecipeBuffC[1])
            {
                //Debug.Log("Not enough ChemB, try a little more next time");
                suggestChemB.text = "Za mało ChemB, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemB");
                suggestChemB.text = "Idealna ilość ChemB!";
            }


            if (userCostC > RecipeBuffC[2])
            {
                //Debug.Log("Too much ChemC, try a little less next time");
                suggestChemC.text = "Za dużo ChemC, spróbuj trochę mniej.";
            }
            else if (userCostC < RecipeBuffC[2])
            {
                //Debug.Log("Not enough ChemC, try a little more next time");
                suggestChemC.text = "Za mało ChemC, spróbuj trochę więcej";
            }
            else
            {
                //Debug.Log("Perfect amount of ChemC");
                suggestChemC.text = "Idealna ilość ChemC!";
            }
        }

        SaveSerial.ChemA -= userCostA;
        SaveSerial.ChemB -= userCostB;
        SaveSerial.ChemC -= userCostC;
        SaveSerial.ChemD -= costChemD;

        UICamp.Instance.CraftingResultScreenOpen();
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
            RecipeBuffA = new int[] { costChemA, costChemB, costChemC, costChemD, controlSum};
        }
        //RecipeBuffA = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
        RecipeBuffA = new int[] { costChemA, costChemB, costChemC, costChemD, controlSum};

        Debug.Log("RecipeA: A" + RecipeBuffA[0] + ", B" + RecipeBuffA[1] + ", C" + RecipeBuffA[2] + ", D" + RecipeBuffA[3]+", SUM"+RecipeBuffA[4]);
        return RecipeBuffA;
    }

    public int[] GenerateRecipeBuffB()
    {
        int controlSum = 8;
        
        int costChemB = Random.Range(3, 7);
        int costChemA = Random.Range(0, controlSum-costChemB+1);

        int costChemC = 0;
        if (costChemA + costChemB < controlSum)
        {
            costChemC = controlSum - costChemA - costChemB;
        }
        RecipeBuffB = new int[] { costChemA, costChemB, costChemC, costChemD, controlSum};

        
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
            RecipeBuffC = new int[] { costChemA, costChemB, costChemC, costChemD, controlSum};
        }
        //RecipeBuffC = "ChemA:" + costChemA + ";" + "ChemB:" + costChemB + ";" + "ChemC:" + costChemC + ";ChemD:" + costChemD;
        RecipeBuffC = new int[] { costChemA, costChemB, costChemC, costChemD, controlSum};

        Debug.Log("RecipeC: A" + RecipeBuffC[0] + ", B" + RecipeBuffC[1] + ", C" + RecipeBuffC[2] + ", D" + RecipeBuffC[3] + ", SUM" + RecipeBuffC[4]);
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
