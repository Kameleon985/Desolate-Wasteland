using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayerMovement : MonoBehaviour
{

    public float playerSpeed = 2.5f;
    public Transform shotSpawn;
    public Camera cam;
    
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePos;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);       

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
/*
        Vector2 lookDirection = mousePos - rb.position;
        float angleZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angleZ;*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Kolizja");
        if (collision.gameObject.CompareTag("Surowiec"))
        {
            //Debug.Log("Kolizja z wrogiem");
            Destroy(gameObject);
            //Death();
        }
    }

    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
}

    

