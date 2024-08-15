using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    //Get Input from Player and launch a projectile
    public float firerate;
    public PlayerController playerController;
    public Transform bulletPosition;
    public GameObject bullet;

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, firerate);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerController.missileCount > 0)
        {
            Missile();
        }
    }

    void Missile()
    {
        GameObject obj = MissilePooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = bulletPosition.position;
        obj.SetActive(true);
        playerController.missileCount--;
    }

    void Shoot()
    {
            GameObject obj = BulletPooler.current.GetPooledObject();
            if (obj == null) return;
            obj.transform.position = bulletPosition.position;
            obj.SetActive(true);        
    }

    public void StopShooting()
    {
        CancelInvoke("Shoot");
    }
}
