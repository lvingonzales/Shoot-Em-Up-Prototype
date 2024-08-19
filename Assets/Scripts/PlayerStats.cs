using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float hpValue;
    public float expValue;
    public float expMaxValue;
    public float level;
    public float missileCount;

    public void ResetHealth()
    {
        hpValue = 6;
    }

    public void ResetStats()
    {
        hpValue = 6;
        expValue = 0;
        expMaxValue = 10;
        level = 1;
        missileCount = 20;
    }

    public void MissileFired()
    {
        missileCount--;
    }

    public void LevelUp ()
    {
        expValue = 0;
        level++;
    }    
}
