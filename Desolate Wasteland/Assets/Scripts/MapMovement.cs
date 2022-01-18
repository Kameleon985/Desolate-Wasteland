using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public Sprite Stationary;
    public Sprite Moving;
    public float speed;
    private Vector2 destPosition;
    private bool moving;
    private SpriteRenderer sprite;

    private void Start()
    {
        moving = false;
        destPosition = (Vector2)transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            destPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            destPosition = (Vector2)transform.position;
            moving = false;
        }
        if (moving && (Vector2)transform.position != destPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, destPosition, Time.deltaTime * speed);
            sprite.sprite = Moving;
        }
        else
        {
            moving = false;
            sprite.sprite = Stationary;
        }
    }

    private void OnMouseUp()
    {
        if (moving == false)
        {
            Debug.Log("Enter location");
        }
    }

}
