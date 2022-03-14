using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmySlot : MonoBehaviour, IDropHandler
{

    public GameObject armyTransferView;
    public Text sliderMaxValueText;
    public Slider slider;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped on item slot - ArmySlot");
        if(eventData.pointerDrag != null && eventData.pointerDrag.gameObject.tag == this.gameObject.tag)
        {
            //Open Transfer
            armyTransferView.gameObject.SetActive(true);
            string stringMaxValue = eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            int intMaxValue = int.Parse(stringMaxValue);
            sliderMaxValueText.text = stringMaxValue;
            slider.maxValue = intMaxValue;
            Debug.Log("Dropped on slot - Same Tag");

        } else if (eventData.pointerDrag != null) //To delete Debug only
        {
            Debug.Log("Dropped on slot - Different Tag");
        }
        
    }
}
