using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void MainScene()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void PlayerStaus(int lifeStatus)
    {
        if (lifeStatus <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void PlayAgain()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1.0f;
        PlayerHealth.instance.RestoreLife();

    }
}
