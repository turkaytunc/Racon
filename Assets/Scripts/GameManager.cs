using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float playerHealth;
    private float score = 0;

    [SerializeField] private bool isGameOver;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }



    public void GameOver()
    {
        StartCoroutine(LoadGameOverScene());
    }

    public void SetPlayerHealth(float playerHealth)
    {
            this.playerHealth = playerHealth;
    }

    public float GetPlayerHealth()
    {
        return this.playerHealth;
    }


    public void SetScore(float score)
    {
        this.score += score;
    }

    public float GetScore()
    {
        return this.score;
    }



    private IEnumerator LoadGameOverScene()
    {
        
        yield return new WaitForSeconds(2f);
        isGameOver = true;
        SceneManager.LoadScene("GameOver");

    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void LoadGameLevel()
    {
        isGameOver = false;
        SetPlayerHealth(500);
        score = 0;
        SceneManager.LoadScene("Scene001");
    }

    public void LoadMainMenu()
    {
        isGameOver = false;
        SetPlayerHealth(500);
        score = 0;
        SceneManager.LoadScene("MainMenu");
    }


}
