using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletController bulletController;
    public GameEvent onPlayerDamage;
    public GameEvent onPlayerDeath;
    public int exp, expmax, level;
    public int health = 6;

    private void Awake()
    {
        exp = 0;
        expmax = 10;
        level = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (health <= 0)
            {
                Debug.Log("IM DEAD");
                onPlayerDeath.TriggerEvent();
                gameObject.SetActive(false);
            }
            else
            {
                health = health - 2;
            }

        }
    }

    public void Experience ()
    {
        if (exp >= expmax)
        {
            exp = 0;
            level++;
        }
        else
        {
            exp++;
        }

    }
}
