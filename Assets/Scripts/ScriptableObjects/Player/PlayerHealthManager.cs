using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealthManager")]
public class PlayerHealthManager : ScriptableObject
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<GameEvent> gameEvents;

    public GameObject player;
    
    /*
        0. OnPlayerDamage
        1. OnPlayerDeath
        2. OnPlayerLevelUp
        3. UIUpdate
     */

    
    public void ApplyDamage(int damage, GameObject target)
    {
        if (target == null) return;

        if (target == player)
        {
            if (playerStats.isInvulnerable) return;
            playerStats.hpValue -= damage;
            gameEvents[0].TriggerEvent();               // OnPlayerDamage
            gameEvents[3].TriggerEvent();               // UIUpdate

            if (playerStats.hpValue <= 0)
            {
                playerStats.hpValue = 0;
                gameEvents[1].TriggerEvent();           // OnPlayerDeath
            }
        }
    }
}
