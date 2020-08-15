using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour
{
    public bool GameStarted;
    public bool modezombie;

    private void Start()
    {
        Invoke("StartGame", 2f);
    }

    private void StartGame()
    {
        GameStarted = true;
    }

    public void RestartGame()
    {
        Invoke("Restart", 1f);
    }

    private void Restart()
    {
        if(modezombie)
        SceneManager.LoadScene(2);
        else
        SceneManager.LoadScene(1);

    }
}