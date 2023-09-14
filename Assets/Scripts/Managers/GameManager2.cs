
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class GameManager2 : MonoBehaviour
{
    
    [Header("Game Enemies")]
    [SerializeField] private GameObject meleeEnemyPreFab;
    [SerializeField] private GameObject shootingEnemyPreFab;
    [SerializeField] private GameObject missileEnemyPreFab;
    //powerup prefabs
    [SerializeField] private GameObject shootingPowerupPreFab;
    [SerializeField] private GameObject bombPreFab;
    [SerializeField] private GameObject healthRegenPreFab;
    //spawns
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform[] powerupSpawnPoints;
    //bool for spawn
    private bool isEnemySpawning;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private float powerupSpawnRate;
    [SerializeField] private float bombSpawnRate;
    [SerializeField] private float healthRegenSpawnRate;

    //temp stuff
    private GameObject tempEnemy;
    private GameObject tempPowerup;

    //speed vars
    static float shootingEnemyBulletSpeed = 3.03f;
    static float missileEnemyBulletSpeed = 7.14f;
    static float shootingEnemyDamage = 3.03f;
    static float missileEnemyDamage = 7.14f;

    //weapons
    private Weapon meleeWeapon = new Weapon("Melee", 10, 0);
    private Weapon shootingWeapon = new Weapon("Shooting Enemy Weapon", shootingEnemyDamage, shootingEnemyBulletSpeed);
    private Weapon missileWeapon = new Weapon("Missile Enemy Weapon", missileEnemyDamage, missileEnemyBulletSpeed);

    // UI 
    public GameObject Player;
    public static bool gameIsFinished = false;
    public static bool gameIsPaused = false;

    //instance
    private static GameManager2 instance;

    public static GameManager2 GetInstance()
    {
        return instance;
    }
    void SetSingleton()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {
        // Find the player
        Player = GameObject.FindWithTag("Player");
        isEnemySpawning = true;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnBomb());
        StartCoroutine(SpawnHealthRegen());
        //start the main theme music
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusicAudio("music_main");
    }

    private void Update()
    {
        GameFlow();
    }
    void CreateEnemy()
    {
        tempEnemy = Instantiate(meleeEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
    }

    void CreateShootingEnemy()
    {
        //spawn shooting enemy
        tempEnemy = Instantiate(shootingEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = shootingWeapon;
        tempEnemy.GetComponent<ShootingEnemy>().SetShootingEnemy(5f);
    }

    void CreateMissileEnemy()
    {
        //spawn missile enemy
        tempEnemy = Instantiate(missileEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon =  missileWeapon;
        tempEnemy.GetComponent<MissileEnemy>().SetMissileEnemy(2f);
    }

    IEnumerator SpawnEnemy()
    {
        while (isEnemySpawning)
        {
            CreateEnemy();
            CreateShootingEnemy();
            CreateMissileEnemy();
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (isEnemySpawning)
        {
            tempPowerup = Instantiate(shootingPowerupPreFab);
            tempPowerup.transform.position = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Length)].position;
            yield return new WaitForSeconds(powerupSpawnRate);
        }
    }

    IEnumerator SpawnBomb()
    {
        while (isEnemySpawning)
        {
            tempPowerup = Instantiate(bombPreFab);
            tempPowerup.transform.position = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Length)].position;
            yield return new WaitForSeconds(bombSpawnRate);
        }
    }

    IEnumerator SpawnHealthRegen()
    {
        while (isEnemySpawning)
        {
            tempPowerup = Instantiate(healthRegenPreFab);
            tempPowerup.transform.position = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Length)].position;
            yield return new WaitForSeconds(healthRegenSpawnRate);
        }
    }
    private void GameFlow()
    {
        if (Player == null)
        {
            gameIsFinished = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = true;
        }
    }



}
