using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    //public values that are gloablly accessed and updated in Update()
    public static int score;
    public static int highschore;
    public static float health;
    public static string quickFireTimer;
    public static int bombsInventory;

    //UI overlay text
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highschoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI quickFireTimerText;

    //bomb image object variables
    [SerializeField] private GameObject bomb1;
    [SerializeField] private GameObject bomb2;
    [SerializeField] private GameObject bomb3;
    [SerializeField] private GameObject bomb4;

    private void CheckForBombs()
    {
        bomb1.SetActive(false);
        bomb2.SetActive(false);
        bomb3.SetActive(false);
        bomb4.SetActive(false);
        switch (bombsInventory)
        {
            case 0:
                break;
            case 1:
                bomb1.SetActive(true); 
                break;
            case 2:
                bomb1.SetActive(true);
                bomb2.SetActive(true);
                break;
            case 3:
                bomb1.SetActive(true);
                bomb2.SetActive(true);
                bomb3.SetActive(true);
                break;
            case 4:
                bomb1.SetActive(true);
                bomb2.SetActive(true);
                bomb3.SetActive(true);
                bomb4.SetActive(true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //update the UI with current globally accessible values
        scoreText.text = "Score " + score.ToString();
        healthText.text = "Health: " + Mathf.RoundToInt(health).ToString();
        //highscore feature needed

        //quickfire timer
        quickFireTimerText.text =  quickFireTimer;
        //bombs in Inventory can be checked for, and an image will be displayed for every one (max 4)
        CheckForBombs();
    }
}
