using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    //Get Input from Player and launch a projectile

    public Transform bulletPosition;
    public GameObject bullet;

    private void Start()
    {
        InvokeRepeating("Shoot", 1.0f, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Shoot()
    {
        GameObject obj = ObjectPooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = bulletPosition.position;
        obj.SetActive(true);
    }
}
