using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public List<UpgradeList> upgrades;
    public int[] randomChosenUpgrades = new int[3];

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<Image> upgradeSlots;
    Image slotSprite;

    float randomValue;
    float total;
    bool numberChanged;
    float[] prob = new float[6];
    bool checkedForDuplicates = false;
    bool[] isSlotOccupied = new bool [3];
    UpgradeList selectedUpgrade;
    
    private void Awake()
    {
        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            isSlotOccupied[i] = false;
        }
    }

    private void Start()
    {
        foreach (UpgradeList upgrades in upgrades)
        {
            upgrades.ResetUpgrades();
        } 
    }

    public void ChooseThreeUpgrades()
    {   
        prob[0] = upgrades[0].chanceToAppear;
        for (int i = 1; i < upgrades.Count; i++)
        {
                prob[i] = prob[i-1] + upgrades[i].chanceToAppear;
        }

        for (int i = 0; i < 3; i++)
        {
            randomValue = Random.Range(0, 100);
            for (int j = 0; j < 6; j++)
            {
                if (randomValue < prob[j])
                {
                    randomChosenUpgrades[i] = j;
                    break;
                }
            }
        }
        checkedForDuplicates = false;
        while (!checkedForDuplicates)
        {
            numberChanged = false;
            for (int i = 0; i<3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (j == i) continue;
                    if (randomChosenUpgrades[i] == randomChosenUpgrades[j])
                    {
                        randomValue = Random.Range(0, 100);
                        numberChanged = true;
                        for (int k = 0; k < 6; k++)
                        {
                            if (randomValue < prob[k])
                            {
                                randomChosenUpgrades[j] = k;
                                break;
                            }
                        }
                    }
                }
            }
            if (!numberChanged) checkedForDuplicates = true;
        }
        Debug.Log(randomChosenUpgrades[0] + "\n" +
            randomChosenUpgrades[1] + "\n" +
            randomChosenUpgrades[2] + "\n");
    }

    public void GetSelectedUpgrade (int chosenUpgradeIndex)
    {
        //Debug.Log("Number Chosen" +  chosenUpgradeIndex);
        //Debug.Log(randomChosenUpgrades[chosenUpgradeIndex]);
        selectedUpgrade = upgrades[randomChosenUpgrades[chosenUpgradeIndex]];
        selectedUpgrade.level++;
        selectedUpgrade.isApplied = true;
        playerStats.appliedUpgrades.Add(selectedUpgrade);
        selectedUpgrade.chanceToAppear = 33.4f;

        float x, z = 0;
        int y = 0;

        for (int i = 0; i < 6; i++)
        {
            if (upgrades[i].chanceToAppear == 33.4f)
            {
                z = upgrades[i].chanceToAppear + z;
            }else
            {
                y++;
            }
        }
        x = 100.2f - z;
        for (int i = 0; i < 6; i++)
        {
            if (upgrades[i].chanceToAppear < 33.4f)
            {
                upgrades[i].chanceToAppear = x / y;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (!isSlotOccupied[i])
            {
                slotSprite = upgradeSlots[i].GetComponent<Image>();
                slotSprite.sprite = selectedUpgrade.upgradeIcon;
                isSlotOccupied[i] = true;
                break;
            }
        }
    }
}
