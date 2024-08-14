using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            if (stats.health > 0)
            {
                stats.health = stats.health - 2;
            } else
            {
                stats.health = 0;

                Debug.Log("DEAD!");
            }
        }
    }


}
