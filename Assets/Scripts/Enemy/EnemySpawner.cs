using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int hordeSize;
    public static EnemySpawner current;

    private List<GameObject> enemies;

    private void Awake()
    {
       current = this;
    }
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        enemies = new List<GameObject>();
        for (int i = 0; i < hordeSize; i++)
        {
            GameObject obj = Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }
        InvokeRepeating("Spawn", 0.0f, 0.10f);
        //InvokeRepeating("AddHorde", 0.0f, 5.0f);
    }

    public GameObject GetCurrentEnemy ()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }

        return null;
    }

    void Spawn ()
    {
        GameObject obj = current.GetCurrentEnemy();
        if (obj == null) return;
        obj.transform.position = this.transform.position;
        obj.SetActive(true);
        
    }
    
    void AddHorde()
    {
        hordeSize += 3;
        for (int i = enemies.Count-1;  i < hordeSize; i++)
        {
            GameObject obj = Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }
    }
}
