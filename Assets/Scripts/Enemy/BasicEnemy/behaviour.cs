using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    public int enemyhp = 3;
    public GameEvent enemyDeath;
    public GameEvent uiUpdate;

    RangedPathfinding rangedPathfinding;

    private void OnEnable()
    {
        //Invoke("Disable", 4f);

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
        rangedPathfinding = this.GetComponent<RangedPathfinding>();
        rangedPathfinding.ResetPath();
        Disable();
    }
}
