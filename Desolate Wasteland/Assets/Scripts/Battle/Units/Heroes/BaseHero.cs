using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHero : BaseUnit
{
    public int attackDamage = 10;

    public abstract void takeDamage(int dmg);
}
