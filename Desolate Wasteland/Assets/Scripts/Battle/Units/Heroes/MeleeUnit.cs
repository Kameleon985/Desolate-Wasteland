using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeleeUnit : BaseHero
{
    public SpriteRenderer sr;

    public static readonly int maxHealth = 25;

    static int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    public static int currentHealth = 25;  

    int movementSpeed;

    public static int initiative = 10;

    public static int attackDamage = 10;

    public static int quantity = SaveSerial.MeleeUnit; //To read from SaveSerial PlayerArmy

    public static bool buffAGiven = false; //Health buff ? // This is the cheaper buff
    public static bool buffBGiven = false; //Attack buff ?
    public static bool buffCGiven = false; //Initiative buff ?

    public GameObject unitCounter;

    public void setAttackDamage(int increase)
    {
        attackDamage = attackDamage + increase;
    }

    public int getAttackDamage()
    {
        Debug.Log("Melee dmg: " + Convert.ToInt32(attackDamage + attackDamage * ((double)quantity / 10)) + " q:" + quantity + " baseDmg:" + attackDamage);
        return Convert.ToInt32(attackDamage + attackDamage * ((double)quantity / 10));
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return sr;
    }

    public override int getInitiative()
    {
        return initiative;
    }

    public override void setInitiative(int init)
    {
        initiative = init;
    }

    public void Start()
    {
        setUnitCount();
    }

    internal static void dealDamage(int dmg)
    {
        if (quantity > 0)
        {
            currentHealth -= dmg;
            Debug.Log("Unit is taking " + dmg + " damage, currentHP: " + currentHealth);
            if (currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died
                //setUnitCount();

                SaveSerial.MeleeUnit = quantity;
                Debug.Log("Unit died, only " + quantity + " units left");


                //UICamp.Instance.updatePlayerArmy();


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
        if(quantity > 0)
        {
            if (feedness > 0)
            {
                feedness -= 1;
                Debug.Log("MU feedness: " + feedness);
            }
            else if (feedness <= 0)
            {
                dealDamage(5);
                Debug.Log("MU health decreased due to hunger, current: " + currentHealth);
            }
        }
    }

    internal static void Feed()
    {
        if (feedness < 3)
        {
            feedness += 1;
            Debug.Log("MeleeUnits are feed from hunger currently: " + feedness);
            if (currentHealth < maxHealth)
            {
                if(maxHealth - currentHealth < 5)
                {
                    currentHealth = maxHealth;
                }
                Debug.Log("MU Healed from: " + currentHealth);
                currentHealth += 5;
                Debug.Log("MU Healed to: " + currentHealth);
            }

        }
        else if (feedness == 3)
        {
            currentHealth = maxHealth;
            Debug.Log("Feed to max hp ~MeleeUnit");
        }
    }

    public void setUnitCount()
    {
        unitCounter.GetComponentInChildren<Text>().text = quantity.ToString();
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

                SaveSerial.MeleeUnit = quantity;
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
}
