using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    [SerializeField] public string Menu; 

    private void Start()
    {
        Resume();
    }
    private void Update()
    {
        Pause();
    }
    // Restart Button Function 
    public void Resume()
    {   
        GameManager2.gameIsPaused = false;
    }
    // Pause Function
    private void Pause()
    {
        if(GameManager2.gameIsPaused == true)
        {
            Time.timeScale = 0f;
            PauseMenuUI.SetActive(true);
        }
        if(GameManager2.gameIsPaused == false)
        {
            Time.timeScale = 1f;
            PauseMenuUI.SetActive(false);
        }
    }
    // Quit Game Function
    public void ExitGame()
    {
        Application.Quit();
    }
    // MainMenu Button Function
    public void GoToMenu()
    {
        SceneManager.LoadScene(Menu);

    }
}
