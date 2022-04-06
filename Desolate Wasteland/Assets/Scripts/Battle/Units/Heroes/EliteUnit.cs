using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteUnit : MonoBehaviour
{
    readonly int maxHealth = 40;

    int feedness = 3; // 1 for each day of not being fed, after depleeted start decreasing health (-5 for each day?)

    int currentHealth = 40;
    int attackDamage = 10;
    int movementSpeed;
    int initiative;


    //int range; //TO DISCUSS
    //int rangeDamage;
    //int ammo;  //TO DISCUSS

    int quantity; //To read from SaveSerial PlayerArmy

    bool buffAGiven = false; //Health buff ? // This is the cheaper buff
    bool buffBGiven = false; //Attack buff ?
    bool buffCGiven = false; //Initiative buff ?

    void dealDamage(int dmg)
    {
        if(quantity > 0)
        {
            currentHealth -= dmg;
            if(currentHealth <= 0) //If unit health in stack <= 0
            {
                quantity--; //One unit in stack died                
                if(quantity <= 0)
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

}
