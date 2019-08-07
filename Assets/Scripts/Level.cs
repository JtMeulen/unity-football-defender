using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float WaitForLoad = 1f;

    public void LoadMainMenu()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();

        if(gameSession)
        {
            gameSession.ResetGame();
        }

        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverCoroutine());
    }

    IEnumerator LoadGameOverCoroutine()
    {
        yield return new WaitForSeconds(WaitForLoad);
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
