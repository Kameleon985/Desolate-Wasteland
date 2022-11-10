using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteEnemy : BaseEnemy
{
    public SpriteRenderer sr;

    static readonly int maxHealth = 25;
    static int currentHealth = 25;
    public static int initiative = 8;
    public GameObject unitCounter;
    public int attackRange = 50;
    public int ammo = 2;
    static int damage = 10;

    public void Start()
    {
        setUnitCount();
        //takeDamage(0);
    }

    public override int getInitiative()
    {
        return initiative;
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public override void setInitiative(int init)
    {
        initiative = init;
    }

    public static int GetDamage()
    {
        return damage;
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

                Debug.Log("Unit died, only " + quantity + " units left");

                if (quantity <= 0)
                {
                    UnitManager.Instance.enemyList.Remove(this);
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
