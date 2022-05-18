﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteEnemy : BaseEnemy
{
    public SpriteRenderer sr;

    static readonly int maxHealth = 25;
    static int currentHealth = 25;
    public static int initiative = 5;
    static int quantity = 2;
    public GameObject unitCounter;

    public void Start()
    {
        setUnitCount();
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

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        BaseUnit otherUnit = obj as BaseUnit;

        if (otherUnit != null)
        {
            return this.getInitiative().CompareTo(otherUnit.getInitiative());
        }
        else
            throw new ArgumentException("Object is not a BaseUnit");
    }
}