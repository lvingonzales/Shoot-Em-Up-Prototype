using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    //Get Input from Player and launch a projectile
    public float firerate;
    public Transform bulletPosition;
    public GameObject bullet;

    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("Shoot", 0.0f, firerate);
    }
    void Shoot()
    {
        GameObject obj = ObjectPooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = bulletPosition.position;
        obj.SetActive(true);
    }
}
