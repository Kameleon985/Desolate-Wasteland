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
    static int quantity = 1;
    public GameObject unitCounter;
    public int attackRange = 10;
    public int ammo = 2;

    public void Start()
    {
        setUnitCount();
    }

    public override int getInitiative()
    {
        return initiative;
    }

    public override void setInitiative(int init)
    {
        initiative = init;
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
