using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : MonoBehaviour
{
    public int enemyhp = 3;
    public GameEvent enemyDeath;
    public GameEvent uiUpdate;
    public int enemyAmmoCount;
    RangedPathfinding rangedPathfinding;

    public Transform enemyBulletPosition;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private PlayerHealthManager healthmanager;

    private void OnEnable()
    {
        //Invoke("Disable", 4f);
        enemyhp = 3;
        enemyAmmoCount = 10;
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
        if (collision.CompareTag("Player")) healthmanager.ApplyDamage(2, collision.gameObject);
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
        else
        {
            killEnemy();
        }
    }

    void killEnemy()
    {
        enemyDeath.TriggerEvent();
        uiUpdate.TriggerEvent();
        Disable();
        rangedPathfinding = this.GetComponent<RangedPathfinding>();
        rangedPathfinding.ResetPath();
    }

    public void Shoot()
    {
        if (enemyAmmoCount > 0)
        {
            GameObject obj = Instantiate(enemyProjectile);
            if (obj == null) return;
            obj.transform.position = enemyBulletPosition.position;
            obj.SetActive(true);
            enemyAmmoCount--;
        }
    }
}
