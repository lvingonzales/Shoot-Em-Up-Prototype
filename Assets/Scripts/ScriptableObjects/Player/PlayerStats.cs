using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public int MAXLEVEL = 10;
    public float hpValue;
    public float expValue;
    public float expMaxValue;
    public float level;
    public float missileCount;
    public bool isInvulnerable;
    public List<UpgradeList> appliedUpgrades;

    public void ResetHealth()
    {
        hpValue = 6;
    }

    public void ResetStats()
    {
        hpValue = 6;
        expValue = 0;
        level = 1;
        missileCount = 20;
        isInvulnerable = false;
        appliedUpgrades.Clear();
    }

    public void MissileFired()
    {
        missileCount--;
    }

    public void LevelUp ()
    {
        expValue = 0;
        level++;
        expMaxValue = expMaxValue++;
    }    
}
