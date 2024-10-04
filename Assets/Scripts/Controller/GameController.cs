using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player1;
    public GameObject upgradeScreen;
    public PlayerStats playerStats;
    public GameEvent pause;
    public static bool isGamePaused;

    [SerializeField] private PlayerHealthManager healthManager;
    [SerializeField] private UpgradeHandler upgradeHandler;
    private PlayerController playerController;

    private void Awake()
    {
        playerStats.ResetStats();
    }
    private void Start()
    {
        PlayerCreation();
        ResumeGame();
    }

    private void PlayerCreation()
    {
        GameObject player = Instantiate(player1);
        healthManager.player = player;
        playerController = player.GetComponent<PlayerController>();
        playerController.upgradeHandler = upgradeHandler;
        player.SetActive(true);
        player.transform.position = this.transform.position;
    }

    private void Update()
    {
        PauseControl();
    }

    public void PauseTrigger()
    {
        pause.TriggerEvent();
    }

    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            if (upgradeScreen.activeInHierarchy)
            {
                isGamePaused = true;
            }
            if (isGamePaused)
            {
                PauseTrigger();
                PauseGame();
            }
            else
            {
                PauseTrigger();
                ResumeGame();
            }
            
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if(!upgradeScreen.activeInHierarchy)
        {
            isGamePaused = false;
            Time.timeScale = 1;
        } 
    }

    public void ExitToMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
