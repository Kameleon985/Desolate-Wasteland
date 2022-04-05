using UnityEngine;
using UnityEngine.UI;

public class Marketplace : MonoBehaviour
{

    public Slider slider;
    public Toggle fromVitals;
    public Toggle fromScrap;
    public Toggle fromPlastic;
    public Toggle fromElectronics;
    public Toggle toVitals;
    public Toggle toScrap;
    public Toggle toPlastic;
    public Toggle toElectronics;
    private int fromResource; //0 - Vitals, 1 - Scrap, 2 - Plastic, 3 - Electronics, -1 - Empty
    private int toResource; // Same as above
    private int fromAmount;
    private int toAmount;
    public Text maxSliderValueText;
    private int maxSliderValue;
    public Text textSliderCurrentExchange;
    public Button acceptTrade;

    //public ToggleGroup fromGroup;
    //public ToggleGroup toGroup;

    private void Start()
    {
        ShowSliderValue();
        maxSliderValueText.text = maxSliderValue + "";
    }

    private void Update()
    {
        if ((toVitals || toScrap || toPlastic || toElectronics) && (fromVitals || fromScrap || fromPlastic || fromElectronics))
        {
            
            if (fromResource == toResource || fromResource == -1 || toResource == -1)
            {
                slider.interactable = false;
                acceptTrade.interactable = false;
                slider.maxValue = 0;
            }
            else
            {
                slider.maxValue = maxSliderValue / 3;
                acceptTrade.interactable = true;
                slider.interactable = true;
            }
        }
        else
        {
            slider.interactable = false;
            acceptTrade.interactable = false;
        }
    }

    public void ShowSliderValue()
    {
        string sliderText = "Koszt wymiany: " + slider.value*3 + " jednostek";
        textSliderCurrentExchange.text = sliderText;
    }

    public void commitTrade()
    {
        if(fromResource == 0)
        {
            SaveSerial.Vitals -= (int)slider.value * 3;
        }
        else if (fromResource == 1)
        {
            SaveSerial.Scrap -= (int)slider.value * 3;
        }
        else if (fromResource == 2)
        {
            SaveSerial.Plastic -= (int)slider.value * 3;
        }
        else if (fromResource == 3)
        {
            SaveSerial.Electronics -= (int)slider.value * 3;
        }

        if(toResource == 0)
        {
            SaveSerial.Vitals += (int)slider.value;
        }
        else if (toResource == 1)
        {
            SaveSerial.Scrap += (int)slider.value;
        }
        if (toResource == 2)
        {
            SaveSerial.Plastic += (int)slider.value;
        }
        if (toResource == 3)
        {
            SaveSerial.Electronics += (int)slider.value;
        }


        UIUpdate.Instance.UpdateUIValues();
        UICamp.Instance.MarketScreenClose();
    }

    public void ToggledFromVitals()
    {
        if (fromVitals.isOn)
        {
            fromResource = 0;
            maxSliderValue = SaveSerial.Vitals;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = maxSliderValue / 3;
        }
        else
        {
            fromResource = -1;
            maxSliderValue = 0;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = 0;
        }
    }

    public void ToggledFromScrap()
    {
        if (fromScrap.isOn)
        {
            fromResource = 1;
            maxSliderValue = SaveSerial.Scrap;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = maxSliderValue / 3;
        }
        else
        {
            fromResource = -1;
            maxSliderValue = 0;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = 0;
        }
    }

    public void ToggledFromPlastic()
    {
        if (fromPlastic.isOn)
        {
            fromResource = 2;
            maxSliderValue = SaveSerial.Plastic;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = maxSliderValue / 3;
        }
        else
        {
            fromResource = -1;
            maxSliderValue = 0;
            maxSliderValueText.text = maxSliderValue + "";
            slider.maxValue = 0;
        }
    }
    public void ToggledFromElectronics()
    {
        if (fromElectronics.isOn)
        {
            fromResource = 3;
            maxSliderValue = SaveSerial.Electronics;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = maxSliderValue / 3;
        }
        else
        {
            fromResource = -1;
            maxSliderValue = 0;
            maxSliderValueText.text = maxSliderValue/3 + "";
            slider.maxValue = 0;
        }
    }


    public void ToggledToVitals()
    {
        if (toVitals.isOn)
        {
            toResource = 0;
        }
        else
        {
            toResource = -1;
        }
    }
    public void ToggledToScrap()
    {
        if (toScrap.isOn)
        {
            toResource = 1;
        }
        else
        {
            toResource = -1;
        }
    }

    public void ToggledToPlastic()
    {
        if (toPlastic.isOn)
        {
            toResource = 2;
        }
        else
        {
            toResource = -1;
        }
    }
    public void ToggledToElectronics()
    {
        if (toElectronics.isOn)
        {
            toResource = 3;
        }
        else
        {
            toResource = -1;
        }
    }
}
