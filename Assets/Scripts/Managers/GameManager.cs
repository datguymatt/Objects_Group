using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    //[SerializeField] private Transform[] spawnPositions;
    //[SerializeField] private Transform[] powerupSpawnPoints;

    // JT SCRIPTS
    [SerializeField] private GameObject parentGameObject;
        [SerializeField] private List<GameObject> childObjects = new List<GameObject>(); // List to store child objects
        [SerializeField] private GameObject jtenemyPlantSpawner;

        //and then one more JT script in Start and a void called GetChild
        //

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

    //instance
    private static GameManager instance;

    // UI 
    public GameObject Player;
    public static bool gameIsFinished = false;
    public static bool gameIsPaused = false;

        public static GameManager GetInstance()
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

        private void Update()
        {
            GameFlow();
        }

        void Start()
        {
           // Find the player
           Player = GameObject.FindWithTag("Player");

          Invoke("GetChildren" , 1f);
          Invoke("SpawnInvoke", 2f);
          isEnemySpawning = true;

            //start the main theme music
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlayMusicAudio("music_main");
        }
        public void SpawnInvoke()
        {
            StartCoroutine(SpawnEnemy());
            StartCoroutine(SpawnPowerup());
            StartCoroutine(SpawnBomb());
            StartCoroutine(SpawnHealthRegen());
        }
        void CreateEnemy()
        {
                int randomIndex = Random.Range(0, childObjects.Count);
                tempEnemy = Instantiate(meleeEnemyPreFab);
                tempEnemy.transform.position = childObjects[randomIndex].transform.position;
                tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
        }

        void CreateShootingEnemy()
        {
                //spawn shooting enemy
            int randomIndex = Random.Range(0, childObjects.Count);
            tempEnemy = Instantiate(shootingEnemyPreFab);
            tempEnemy.transform.position = childObjects[randomIndex].transform.position;
            tempEnemy.GetComponent<Enemy>().weapon = shootingWeapon;
            tempEnemy.GetComponent<ShootingEnemy>().SetShootingEnemy(5f);
        }

        void CreateMissileEnemy()
        {
            int randomIndex = Random.Range(0, childObjects.Count);
                //spawn missile enemy
            tempEnemy = Instantiate(missileEnemyPreFab);
            tempEnemy.transform.position = childObjects[randomIndex].transform.position;
            tempEnemy.GetComponent<Enemy>().weapon = missileWeapon;
            tempEnemy.GetComponent<MissileEnemy>().SetMissileEnemy(2f);
        }

        IEnumerator SpawnEnemy()
        {
            while (isEnemySpawning)
            {
                CreateEnemy();
                CreateShootingEnemy();
                CreateMissileEnemy();
                JTSpawnPlant();
                    yield return new WaitForSeconds(enemySpawnRate);
            }
        }
        void JTSpawnPlant()
        {
                int randomIndex = Random.Range(0, childObjects.Count);
                //spawn missile enemy
                tempEnemy = Instantiate(jtenemyPlantSpawner);
                tempEnemy.transform.position = childObjects[randomIndex].transform.position;
        }

        IEnumerator SpawnPowerup()
        {
        while (isEnemySpawning)
            {
                int randomIndex = Random.Range(0, childObjects.Count);

                tempPowerup = Instantiate(shootingPowerupPreFab);
                tempPowerup.transform.position = childObjects[randomIndex].transform.position;
                yield return new WaitForSeconds(powerupSpawnRate);
            }
        }

    IEnumerator SpawnBomb()
    {
        while (isEnemySpawning)
        {
                int randomIndex = Random.Range(0, childObjects.Count);

            tempPowerup = Instantiate( bombPreFab);
            tempPowerup.transform.position = childObjects[randomIndex].transform.position;
            yield return new WaitForSeconds(bombSpawnRate);
        }
    }
    IEnumerator SpawnHealthRegen()
    {
        while (isEnemySpawning)
        {
            int randomIndex = Random.Range(0, childObjects.Count);

            tempPowerup = Instantiate(healthRegenPreFab);
            tempPowerup.transform.position = childObjects[randomIndex].transform.position;
            yield return new WaitForSeconds(healthRegenSpawnRate);
        }
    }
    //JT
    void GetChildren()
    {
        if (parentGameObject != null)
        {
            Transform parentTransform = parentGameObject.transform;
            foreach (Transform childTransform in parentTransform)
            {
                childObjects.Add(childTransform.gameObject);
            }
        }
    }

    // Ilya Script to operate Gameflow (game finished, paused)
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
