using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    public GameEvent enemyDeath;
    public float moveSpeed;
    public PlayerController playerController;
    
    
    private float moveDirection = 1;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 3.0f);

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
        if (collision.CompareTag("Bullet"))
        {
            enemyDeath.TriggerEvent();
            Disable();
        }

        if (collision.CompareTag("Player")) Debug.Log("CONTACT");
        
    }
}
