using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private SpriteRenderer renderer;

    private void Awake()
    {
        renderer.color = baseColor;
    }



}
