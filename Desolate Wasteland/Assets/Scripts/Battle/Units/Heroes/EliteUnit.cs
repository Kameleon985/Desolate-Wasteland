﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteUnit : MonoBehaviour
{
    static readonly int maxHealth = 40;

    static int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    static int currentHealth = 40;
    int attackDamage = 10;
    int movementSpeed;
    int initiative;


    //int range; //TO DISCUSS
    //int rangeDamage;
    //int ammo;  //TO DISCUSS

    static int quantity = SaveSerial.EliteUnit; //To read from SaveSerial PlayerArmy

    public static bool buffAGiven = false; //Health buff ? // This is the cheaper buff
    public static bool buffBGiven = false; //Attack buff ?
    public static bool buffCGiven = false; //Initiative buff ?

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

}