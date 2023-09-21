using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
   [SerializeField] GameObject missleEnemy;
    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject shooter;
    //public GameManager gameManager;
    //public ScoreManager scoreManager;
    public int difficultyInc;
    bool isPlaying;
   
    void Start()
    {
        isPlaying = true;
        //gameManager = GetComponent<GameManager>();
        //scoreManager = GetComponent<ScoreManager>();
        StartCoroutine(DifficultyCurve());
    }

    
   
    void DifficultyIncrease()
    {
        difficultyInc++;
    }

    IEnumerator DifficultyCurve()
    {
        while (isPlaying)
        {
            DifficultyIncrease();
            yield return new WaitForSeconds(7);
        }
        
    }
   
}
