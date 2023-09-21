using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
   [SerializeField] GameObject missleEnemy;
    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject shooter;
    public GameManager gameManager;
    public ScoreManager scoreManager;
    public int score;
   
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        scoreManager = GetComponent<ScoreManager>();
        StartCoroutine("DifficultyCurve");
    }

    
   
    void DifficultyIncrease()
    {
        missleEnemy.GetComponent<MissileEnemy>().difficultyIncrease++;
        meleeEnemy.GetComponent<MeleeEnemy>().difficultyIncrease++;
        shooter.GetComponent<ShootingEnemy>().difficultyIncrease++;
    }

    IEnumerator DifficultyCurve()
    {
        DifficultyIncrease();
        yield return new WaitForSeconds(10);
    }
   
}
