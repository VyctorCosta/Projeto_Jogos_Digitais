using TMPro;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // public int totalScore;
    public TextMeshProUGUI scoreText;

    public GameObject gameOver;

    public GameObject pauseScreen;

    public bool isPaused = false;

    public static GameController instance;

    private float punctuationGettedInScene = -1f;
    private float bananaGettedInScene = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        Time.timeScale = 1f;

        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = Punctuation.instance.totalScore < 10 ? $"0{Punctuation.instance.totalScore}" : Punctuation.instance.totalScore.ToString();
        punctuationGettedInScene++;
    }

    public void increaseBanana()
    {
        bananaGettedInScene++;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        Punctuation.instance.totalScore -= punctuationGettedInScene;
        Punctuation.instance.specialBanana -= bananaGettedInScene;
        SceneManager.LoadScene(lvlName);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
    }
}
