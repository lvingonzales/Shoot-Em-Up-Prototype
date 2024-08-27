using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject rEnemy, mEnemy;
    public int rangedHordeSize;
    public int meleeHordeSize;
    public static EnemySpawner current;
    public Transform[] spawnLocations;
    public Transform[] rangedRoutes;

    [SerializeField] private bool[] isXPathReserved;
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
        for (int i = 0; i < rangedHordeSize; i++)
        {
            GameObject obj = Instantiate(rEnemy);
            rangedpathfinding = obj.GetComponent<RangedPathfinding>();
            for (int j = 0; j < rangedRoutes.Length; j++)
            {
                if (!isXPathReserved[j])
                {
                    rangedpathfinding.route = rangedRoutes[j];
                    isXPathReserved[j] = true;
                    break;
                }
            }
            
            obj.SetActive(false);
            rEnemies.Add(obj);
        }
        mEnemies = new List<GameObject>();
        for (int i = 0; i < meleeHordeSize; i++)
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

    void Spawn()
    {
        int selectedLocation = Random.Range(0, 4);

        //Debug.Log("Selected Location: " + selectedLocation);
        GameObject robj = current.GetCurrentRangedEnemy();
        if (robj == null) return;
        robj.transform.position = spawnLocations[selectedLocation].position;
        robj.SetActive(true);


        //Debug.Log("Selected Location: " + selectedLocation);
        GameObject mobj = current.GetCurrentMeleeEnemy();
        if (mobj == null) return;
        mobj.transform.position = spawnLocations[selectedLocation].position;
        mobj.SetActive(true);
    }
}
