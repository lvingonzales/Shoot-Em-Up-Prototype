using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    public List<GameObject> upgradePanels;
    public GameObject upgradeHandler;

    Button panelButton;
    GameObject upgradeIconObject;
    Image upgradeIconImage;
    TextMeshProUGUI upgradeInfo;
    UpgradeHandler upgradeHandlerScript;
    Sprite currentSpriteSelected;

    private void Awake()
    {
        upgradeHandlerScript = upgradeHandler.GetComponent<UpgradeHandler>();
    }

    private void OnEnable()
    {
        for  (int i = 0; i < upgradePanels.Count; i++)
        {
            upgradePanels[i].GetComponent<Button>().enabled = true;
        }
    }

    public void LevelUpDisplayChange ()
    {
        
        upgradeHandlerScript.ChooseThreeUpgrades();
        for (int i = 0; i < upgradePanels.Count; i++)
        {
            currentSpriteSelected = upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].upgradeIcon;
            
            upgradeIconObject = upgradePanels[i].transform.GetChild(0).gameObject;
            
            upgradeIconImage = upgradeIconObject.GetComponent<Image>();
            
            upgradeInfo = upgradePanels[i].GetComponentInChildren<TextMeshProUGUI>();

            upgradeIconImage.sprite = currentSpriteSelected;
            
            if (upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].level == 3)
            {
                upgradeInfo.text =
                upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].upgradeName + "\n" +
                upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].description + "\n" +
                 "MAX LEVEL REACHED";
                upgradePanels[i].GetComponent<Button>().enabled = false;
            }
            else
            {
                upgradeInfo.text =
                upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].upgradeName + "\n" +
                upgradeHandlerScript.upgrades[upgradeHandlerScript.randomChosenUpgrades[i]].description;
            }
        }
    }

    public void HandleButtonClick (GameObject buttonClicked)
    {
        
        for (int i = 0; i < upgradePanels.Count;i++)
        {
            Debug.Log("ButtonCLicked: " + buttonClicked + " CurrentPanel: " + upgradePanels[i].gameObject);
            if (buttonClicked == upgradePanels[i].gameObject) 
            {
                //Debug.Log("FOUND CORRECT UPGRADE");\
                Debug.Log(i);
                upgradeHandlerScript.GetSelectedUpgrade(i);
            }
        }
    }
}
