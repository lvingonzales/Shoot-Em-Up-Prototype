using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirectionX;
    private float moveDirectionY;

    //awake is called after all objects are intialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player Inputs
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        //Move
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirectionX * moveSpeed, moveDirectionY * moveSpeed);
    }

    private void ProcessInputs()
    {
        moveDirectionX = Input.GetAxis("Horizontal");
        moveDirectionY = Input.GetAxis("Vertical");
    }
}
