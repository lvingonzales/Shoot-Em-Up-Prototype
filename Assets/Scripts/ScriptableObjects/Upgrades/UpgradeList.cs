using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "UpgradeInfo")]
public class UpgradeList : ScriptableObject
{
    public int id;
    public Sprite upgradeIcon;
    public string upgradeName;
    public string description;
    public int level = 0;
    public bool isApplied = false;
    public float chanceToAppear = 16.7f;

    public void ResetUpgrades()
    {
        level = 0;
        isApplied = false;
        chanceToAppear = 16.7f;
    }
}

    
