using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    public int enemyhp = 3;
    public GameEvent enemyDeath;
    public GameEvent uiUpdate;

    private string struckBy;
    private float moveDirection = 1;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 10.0f);

        if (enemyhp < 3)
        {
            enemyhp = 3;
        }

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
            if (enemyhp > 0)
            {
                enemyhp--;
            }
            else
            {
                killEnemy();
            }
        }
        if (collision.CompareTag("Missile"))
        {
            if (enemyhp > 0)
            {
                enemyhp= enemyhp - 3;
            }
            else
            {
                killEnemy();
            }
        }       
    }

    void killEnemy()
    {
        enemyDeath.TriggerEvent();
        uiUpdate.TriggerEvent();
        Disable();
    }
}
