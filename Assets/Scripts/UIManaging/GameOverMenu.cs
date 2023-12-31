using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameOverMenu : MonoBehaviour
{

    // This is were you put MainMenu scene name 
    [SerializeField] public string MenuName;

    // This varibles need to be assigned in the inspector
    public TMP_Text HighScoreValue;
    public TMP_Text HighScoreText;
    public GameObject PauseMenuUI;
    public GameObject FinishMenuUI;
    public GameObject OverlayUI;

    // This varibles assigned at the start of the game
    private GameManager gameManager;
    public ScoreManager scoreManager;

    private void Awake()
    {
        // Finds ScoreManager and GameManager 
        gameManager = FindObjectOfType<GameManager>(); 
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        // This function happens when the game is finished
        GameFinishFunction();
    }

    // Disable OverlayUI and pass value of highscore to txtUI. 
    // Check if the current Score is higher than previous one.
    private void GameFinishFunction()
    {
        if (GameManager.gameIsFinished == true)
        {
            Time.timeScale = 0f;
            // Making sure PauseMenu is disabled
            PauseMenuUI.SetActive(false);
            OverlayUI.SetActive(false);
            FinishMenuUI.SetActive(true);
            HighScoreValue.text = ScoreManager.highScore.ToString();

            if(ScoreManager.newHigh == ScoreManager.highScore)
            {
                HighScoreText.text = "New High SCORE";
            }
            else
            {
                HighScoreText.text = "CURRENT HIGH SCORE";  
            }
        }
    }

    // Restart Button Function 
    public void Restart()
    {
        Destroy(gameManager);
        Time.timeScale = 1f;
        GameManager.gameIsFinished = false;
        FinishMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit Game Function
    public void ExitGame()
    {
        Application.Quit();
    }

    // MainMenu Button Function
    public void GoToMenu()
    {
        Destroy(gameManager);
        GameManager.gameIsFinished = false;
        Time.timeScale = 1f;
        FinishMenuUI.SetActive(false);
        SceneManager.LoadScene(MenuName);
    }
}
