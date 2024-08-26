using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject rEnemy, mEnemy;
    public int hordeSize;
    public static EnemySpawner current;
    public Transform rSpawner1, rSpawner2, mSpawner1, mSpawner2;
    public Transform[] rangedRoutes;

    RangedPathfinding rangedpathfinding;
    bool spawnSide;
    private List<GameObject> rEnemies;
    private List<GameObject> mEnemies;

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
        rEnemies = new List<GameObject>();
        for (int i = 0; i < (hordeSize*.75); i++)
        {
            GameObject obj = Instantiate(rEnemy);
            rangedpathfinding = obj.GetComponent<RangedPathfinding>();
            rangedpathfinding.routes[0] = rangedRoutes[0];
            obj.SetActive(false);
            rEnemies.Add(obj);
        }
        mEnemies = new List<GameObject>();
        for (int i = 0; i < (hordeSize * 0); i++)
        {
            GameObject obj = Instantiate(mEnemy);
            obj.SetActive(false);
            mEnemies.Add(obj);
        }
        InvokeRepeating("Spawn", 1f, .5f);
    }

    public GameObject GetCurrentRangedEnemy ()
    {
        for (int i = 0; i < rEnemies.Count; i++)
        {
            if (!rEnemies[i].activeInHierarchy)
            {
                return rEnemies[i];
            }
        }

        return null;
    }

    public GameObject GetCurrentMeleeEnemy()
    {
        for (int i = 0; i < mEnemies.Count; i++)
        {
            if (!mEnemies[i].activeInHierarchy)
            {
                return mEnemies[i];
            }
        }

        return null;
    }

    void Spawn ()
    {
        GameObject robj = current.GetCurrentRangedEnemy();
        if (robj == null) return;
        robj.transform.position = rSpawner1.transform.position;
        robj.SetActive(true);

        GameObject mobj = current.GetCurrentMeleeEnemy();
        if (mobj == null) return;
        if (spawnSide)
        {
            mobj.transform.position = mSpawner1.transform.position;
            spawnSide = !spawnSide;
        }
        else
        {
            mobj.transform.position = mSpawner2.transform.position;
            spawnSide = !spawnSide;
        }
        mobj.SetActive(true);
    }
}
