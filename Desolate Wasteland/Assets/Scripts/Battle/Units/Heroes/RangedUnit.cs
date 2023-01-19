using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedUnit : BaseHero
{
    public SpriteRenderer sr;

    public static int maxHealth = 20;
    public static readonly int maxDefaultHealth = 20;

    static int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    public static int attackDamage = 8;
    public static readonly int defaultAttackDamage = 8;
    public static int currentHealth = 20;
    public int attackRange = 5;
    int movementSpeed;
    public static int initiative = 5;

    static int quantity = SaveSerial.RangeUnit; //To read from SaveSerial PlayerArmy

    public static bool buffAGiven = false; //Health buff ? // This is the cheaper buff
    public static bool buffBGiven = false; //Attack buff ?
    public static bool buffCGiven = false; //Initiative buff ?

    public GameObject unitCounter;
    public GameObject healthCounter;

    public void Start()
    {
        maxHealth = maxDefaultHealth;
        attackDamage = defaultAttackDamage;
        if (currentHealth > maxDefaultHealth)
        {
            maxHealth = maxDefaultHealth;
            currentHealth = maxDefaultHealth;
        }
        quantity = SaveSerial.RangeUnit;
        setUnitUIData();

    }

    public void Update()
    {
        setUnitUIData();
    }

    public int getAttackDamage()
    {
        //Debug.Log("Ranged dmg: " + Convert.ToInt32(attackDamage + attackDamage * ((double)quantity / 10)) + " q:" + quantity + " baseDmg:" + attackDamage);
        return Convert.ToInt32(attackDamage + attackDamage * ((double)quantity / 10));
    }

    public override int getInitiative()
    {
        return initiative;
    }

    public override void setInitiative(int init)
    {
        initiative = init;
    }

    static void dealDamage(int dmg)
    {
        if (quantity > 0)
        {
            currentHealth -= dmg;
            //Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died

                SaveSerial.RangeUnit = quantity;
                //Debug.Log("Unit died, only " + quantity + " units left");
                if (UICamp.Instance != null)
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
        if (quantity > 0)
        {
            if (feedness > 0)
            {
                feedness -= 1;
                //Debug.Log("RU feedness: " + feedness);
            }
            else if (feedness <= 0)
            {
                dealDamage(5);
                //Debug.Log("RU health decreased due to hunger, current: " + currentHealth);
            }
        }

    }

    internal static void Feed()
    {
        if (feedness < 3)
        {
            feedness += 1;
            //Debug.Log("RU are feed from hunger currently: " + feedness);
            if (currentHealth < maxHealth)
            {
                //Debug.Log("RU Healed from: " + currentHealth);
                currentHealth += 5;
                //Debug.Log("RU Healed to: " + currentHealth);
            }

        }
        else if (feedness == 3)
        {
            currentHealth = maxHealth;
            //Debug.Log("Feed to max hp ~RU");
        }
    }

    public void setUnitUIData()
    {
        unitCounter.GetComponentInChildren<Text>().text = quantity.ToString();
        healthCounter.GetComponentInChildren<Text>().text = currentHealth + "/" + maxHealth;
    }

    public override void takeDamage(int dmg)
    {
        if (quantity > 0)
        {
            currentHealth -= dmg;
            //Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died
                setUnitUIData();

                SaveSerial.RangeUnit = quantity;
                //Debug.Log("Unit died, only " + quantity + " units left");

                if (quantity <= 0)
                {
                    UnitManager.Instance.heroList.Remove(this);
                    BattleMenuMenager.instance.UnitKilled(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    currentHealth = maxHealth;
                    setUnitUIData();
                }
            }
            setUnitUIData();
        }
    }

    public static void giveBuffA()
    {
        maxHealth += 10;
        currentHealth = maxHealth;
    }

    public static void giveBuffB()
    {
        attackDamage += 6;
    }

    public static void giveBuffC()
    {
        maxHealth += 5;
        currentHealth = maxHealth;
        attackDamage += 3;
    }
}
