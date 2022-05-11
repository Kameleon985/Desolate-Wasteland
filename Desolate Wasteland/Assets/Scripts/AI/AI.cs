using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    private Transform bestCoverSpot;
    private Transform closestHero;
    public abstract void ConstructBehaviourTree();
    public abstract void TakeAction();
    public abstract void TakeDamage(int damage);
    public abstract void SetBestCoverSpot(Transform cover);
    public abstract Transform GetBestCoverSpot();
    public abstract void SetClosestHero(Transform hero);
    public abstract Transform GetClosestHero();
}
