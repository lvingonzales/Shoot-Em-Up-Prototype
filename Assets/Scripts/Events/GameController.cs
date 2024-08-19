using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player1;
    public PlayerStats playerStats;
    public GameEvent pause;
    public static bool isGamePaused;

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
        player.SetActive(true);
        player.transform.position = this.transform.position;
    }

    private void Update()
    {
        PauseControl();
    }

    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    void PauseGame()
    {
        pause.TriggerEvent();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pause.TriggerEvent();
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void ExitToMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
