using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedUnit : BaseHero
{
    public SpriteRenderer sr;

    public static readonly int maxHealth = 20;

    static int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    public static int attackDamage = 8;
    public static int currentHealth = 20;
    public int attackRange = 5;
    int movementSpeed;
    public static int initiative = 5;

    static int quantity = SaveSerial.RangeUnit; //To read from SaveSerial PlayerArmy

    public static bool buffAGiven = false; //Health buff ? // This is the cheaper buff
    public static bool buffBGiven = false; //Attack buff ?
    public static bool buffCGiven = false; //Initiative buff ?
	
    public GameObject unitCounter;

    public void Start()
    {
        setUnitCount();
    }
	
	public int getAttackDamage()
    {
        return attackDamage;
	}

    static void dealDamage(int dmg)
    {
        if (quantity > 0)
        {
            currentHealth -= dmg;
            Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died

                SaveSerial.RangeUnit = quantity;
                Debug.Log("Unit died, only " + quantity + " units left");

                UICamp.Instance.updatePlayerArmy();

                if (quantity <= 0)
                {
                    //Kill unit on Map
                }
                else
                {
                    currentHealth = maxHealth;
                }
            }
        }

    }

    internal static void Hungry()
    {
        if (feedness > 0)
        {
            feedness -= 1;
            Debug.Log("RU feedness: " + feedness);
        }
        else if (feedness <= 0)
        {
            dealDamage(5);
            Debug.Log("RU health decreased due to hunger, current: " + currentHealth);
        }

    }

    internal static void Feed()
    {
        if (feedness < 3)
        {
            feedness += 1;
            Debug.Log("RU are feed from hunger currently: " + feedness);
            if (currentHealth < maxHealth)
            {
                Debug.Log("RU Healed from: " + currentHealth);
                currentHealth += 5;
                Debug.Log("RU Healed to: " + currentHealth);
            }

        }
        else if (feedness == 3)
        {
            currentHealth = maxHealth;
            Debug.Log("Feed to max hp ~RU");
        }
    }

    public void setUnitCount()
    {
        unitCounter.GetComponentInChildren<Text>().text = quantity.ToString();
    }
}
