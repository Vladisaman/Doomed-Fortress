using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverObj;
    public bool isPaused = false;
    public bool isClicked = false;
    private int score;

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        //RestartButton.gameObject.SetActive(true);
        //PauseButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (isClicked == false && isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0f;
            isClicked = true;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            isClicked = false;
        }
    }
}
