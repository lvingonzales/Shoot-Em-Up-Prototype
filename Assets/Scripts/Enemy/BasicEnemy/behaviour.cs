using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private float moveDirection = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 3.0f);
    }
    void Start()
    {
        // InvokeRepeating("changeDirection", 2.0f, 4.0f);
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move ()
    {
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Disable();
    }
    /*void changeDirection ()
    {
        Debug.Log("direction changed");
        if (moveDirection < 0)
        {
            Debug.Log("Less Than");
            moveDirection = moveDirection * -1;
            Debug.Log(moveDirection);
        }
        else
        {
            moveDirection = moveDirection * -1;
        }
    } 
    */
}
