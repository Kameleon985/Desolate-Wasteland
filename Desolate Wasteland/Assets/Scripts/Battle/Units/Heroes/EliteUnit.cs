using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteUnit : BaseHero
{
    public SpriteRenderer sr;

    static readonly int maxHealth = 40;

    static int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    static int currentHealth = 40;
    public static int meleeDamage = 14;
    int movementSpeed;
    public static int initiative = 8;


    public int attackRange = 4; //TO DISCUSS
    public int rangeDamage = 15;
    public int ammo = 3;  //TO DISCUSS

    static int quantity = SaveSerial.EliteUnit; //To read from SaveSerial PlayerArmy

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
        Debug.Log("Elite dmg melee: " + Convert.ToInt32(meleeDamage + meleeDamage * ((double)quantity / 10)) + " q:"+quantity+" baseDmg:"+meleeDamage);
        return Convert.ToInt32(meleeDamage+meleeDamage*((double)quantity /10));
    }

    public int getRangeDamage()
    {
        Debug.Log("Elite dmg ranged: " + Convert.ToInt32(rangeDamage + rangeDamage * ((double)quantity / 10)) + " q:" + quantity + " baseDmg:" + rangeDamage);
        return Convert.ToInt32(rangeDamage + rangeDamage * ((double)quantity / 10));
    }

    public int getAmmo()
    {
        return ammo;
    }

    public override int getInitiative()
    {
        return initiative;
    }

    public void setAmmo(int ammo)
    {
        this.ammo = ammo;
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
            Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died

                SaveSerial.EliteUnit = quantity;
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
        if (quantity > 0)
        {
            if (feedness > 0)
            {
                feedness -= 1;
                Debug.Log("EU feedness: " + feedness);
            }
            else if (feedness <= 0)
            {
                dealDamage(5);
                Debug.Log("EU health decreased due to hunger, current: " + currentHealth);
            }
        }
    }

    internal static void Feed()
    {
        if (feedness < 3)
        {
            feedness += 1;
            Debug.Log("EU are feed from hunger currently: " + feedness);
            if (currentHealth < maxHealth)
            {
                Debug.Log("EU Healed from: " + currentHealth);
                currentHealth += 5;
                Debug.Log("EU Healed to: " + currentHealth);
            }

        }
        else if (feedness == 3)
        {
            currentHealth = maxHealth;
            Debug.Log("Feed to max hp ~EU");
        }
    }

    public override void takeDamage(int dmg)
    {
        if (quantity > 0)
        {
            currentHealth -= dmg;
            Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died
                setUnitCount();

                SaveSerial.RangeUnit = quantity;
                Debug.Log("Unit died, only " + quantity + " units left");

                if (quantity <= 0)
                {
                    UnitManager.Instance.heroList.Remove(this);
                    BattleMenuMenager.instance.UnitKilled(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    currentHealth = maxHealth;
                }
            }

        }
    }

    public void setUnitCount()
    {
        unitCounter.GetComponentInChildren<Text>().text = quantity.ToString();
    }
}
