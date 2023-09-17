using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JTEnemyPlantSpawner : Enemy
{

    [SerializeField] private float spawnTime;

    private Animator animationController;
    [SerializeField] private GameObject bearPrefab;
    private Spawner spawner;

    // Finding ScoreManager
    ScoreManager scoreManager;

    protected override void Start()
    {
        // Finding ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();

        // Enemy Start
        animationController = GetComponent<Animator>();
        base.Start();
        health = new Health(100, 0, 100);
        StartCoroutine(SpawnBear(spawnTime));
    }

    protected void Awake()
    {
    }
    protected override void Update()
    {
        base.Update();
    }



    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        animationController.Play("PlantDamaged");
        ///ClearDamageAnim();

    }

    public void ClearDamageAnim()
    {

    }
    private IEnumerator SpawnBear(float _interval)
    {
        while(gameObject != null)
        {
            yield return new WaitForSecondsRealtime(_interval);
            Instantiate(bearPrefab, transform.position, Quaternion.identity);
        }
    }

    public override void Die()
    {
        base.Die();
        scoreManager.score += 50;
    }


}
