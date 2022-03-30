using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsAssignement : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    private void Start()
    {
        line.Draw(points);
    }
}
