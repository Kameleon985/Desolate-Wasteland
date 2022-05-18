using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public string name;

    void Start()
    {
        name = this.gameObject.name;
    }


}
