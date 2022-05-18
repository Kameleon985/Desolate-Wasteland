﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMovement : MonoBehaviour
{
    public Sprite Stationary;
    public Sprite Moving;
    public float speed;
    private Vector2 destPosition;
    private bool moving;
    private SpriteRenderer sprite;
    private string atLocation;
    private bool mouseOver;
    public float movePoints;

    public GameObject notClickableThrough;


    private void Start()
    {
        notClickableThrough.SetActive(false);
        mouseOver = false;
        moving = false;
        destPosition = (Vector2)transform.position;
        sprite = GetComponent<SpriteRenderer>();
        atLocation = null;
        GameEventSystem.Instance.OnEnterLocation += SavePosition;
        GameEventSystem.Instance.OnEnterMap += LoadData;
        GameEventSystem.Instance.OnSaveButton += SavePosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            notClickableThrough.SetActive(!notClickableThrough.activeInHierarchy);
        }

        if (notClickableThrough.activeSelf)
        {
            //Do nothing
        }
        else
        {

            if (moving)
            {
                movePoints -= Time.deltaTime;
                GameEventSystem.Instance.PlayerMovement(movePoints);
            }
            if (Input.GetMouseButtonDown(0) && !mouseOver)
            {
                destPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float distance = Vector3.Distance(transform.position, (Vector3)destPosition);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, ((Vector3)destPosition - transform.position), distance, LayerMask.GetMask("Terrain"));

                Debug.DrawRay(transform.position, ((Vector3)destPosition - transform.position), Color.red);


                if (hit)
                {
                    Ray2D ray = new Ray2D(transform.position, (Vector3)hit.point - transform.position);
                    distance = Vector3.Distance(transform.position, hit.point);
                    destPosition = ray.GetPoint(distance - 0.2f);
                }
                GameEventSystem.Instance.PlayerClick(distance / speed);
                moving = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                destPosition = (Vector2)transform.position;
                moving = false;
            }
            if (movePoints > 0 && moving && (Vector2)transform.position != destPosition)
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

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Location"))
        {
            atLocation = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Location"))
        {
            atLocation = collision.name;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Location"))
        {
            atLocation = collision.name;
        }

    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    private void OnMouseUp()
    {
        if (moving == false && atLocation != null)
        {
            movePoints -= 1;
            GameEventSystem.Instance.PlayerMovement(movePoints);
            GameEventSystem.Instance.EnterLocation(atLocation);
        }
    }

    public void SavePosition()
    {
        float[] mapPositionArr = { transform.position.x, transform.position.y };
        SaveSerial.onMapPosition = mapPositionArr;
        SaveSerial.onMapMovementPoints = movePoints;
    }

    public void LoadData()
    {
        float[] mapPositionArr = SaveSerial.onMapPosition;
        movePoints = SaveSerial.onMapMovementPoints;
        transform.position = new Vector2(mapPositionArr[0], mapPositionArr[1]);
    }

    public void OnDestroy()
    {
        GameEventSystem.Instance.OnEnterLocation -= SavePosition;
        GameEventSystem.Instance.OnEnterMap -= LoadData;
    }

}