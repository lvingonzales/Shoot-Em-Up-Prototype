using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI healthMeter;
    public TextMeshProUGUI missileCount;
    public TextMeshProUGUI experienceAmount;
    public TextMeshProUGUI Level;
    public PlayerStats stats;
    public GameObject pausePanel, upgradeScreen;

    private void Start()
    {
        UIUpdate();
        pausePanel.SetActive(false);
        upgradeScreen.SetActive(false);
    }
    public void UIUpdate()
    {
        healthMeter.text = "HP: " + stats.hpValue;
        experienceAmount.text = "EXP: " + stats.expValue + "/" + stats.expMaxValue;
        Level.text = "Level: " + stats.level;
        missileCount.text = "Missiles: " + stats.missileCount;
    }

    public void Pause()
    {
        if(pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
        }
    }

    public void Upgrades()
    {
        if(upgradeScreen.activeInHierarchy)
        {
            upgradeScreen.SetActive(false);
        }
        else
        {
            upgradeScreen.SetActive(true);
        }
    }
}
