using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeEnemy : BaseEnemy
{
    public SpriteRenderer sr;

    static readonly int maxHealth = 20;
    static int currentHealth = 20;
    public static int initiative = 5;
    public GameObject unitCounter;
    public GameObject healthCounter;
    public int attackRange = 50;
    static int damage = 7;

    public void Start()
    {
        setUnitUIData();
        //takeDamage(0);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public static int GetDamage()
    {
        return damage;
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
                setUnitUIData();

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
                    setUnitUIData();
                }
            }
            setUnitUIData();
        }

    }

    public void setUnitUIData()
    {
        unitCounter.GetComponentInChildren<Text>().text = quantity.ToString();
        healthCounter.GetComponentInChildren<Text>().text = currentHealth + "/" + maxHealth;
    }
}
