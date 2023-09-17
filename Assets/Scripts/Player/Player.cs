using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : PlayableObject
{
    //variables
    [SerializeField] private Camera cam;
    [SerializeField] protected float speed;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletPowerupSpeed = 0.5f;
    [SerializeField] private Bullet bulletPreFab;
    [SerializeField] private Bomb bombPreFab;

    //firepoint
    [SerializeField] private Transform firePoint;

    [SerializeField]
    private AudioManager audioManager;

    public Action<float> OnHealthUpdate;

    private bool isPowerupActive;
    private bool isShooting = false;


    // bomb check
    public static bool _hasBomb = false;

    // bomb stuff
    public bool isActiveInventory;
    [SerializeField] private float bombFuseTime = 2;
    [SerializeField] private float bombShootSpeed;
    private ParticleSystem explosion;

    public void SetPowerupFeatures()
    {
        isPowerupActive = true;
    }

    public void ResetPowerupsToNull()
    {
        isPowerupActive = false;
    }

    private void Start()
    {
        //explicity set health vars, just in case inspector is defaulting to 0
        maxHealth = 200;
        currentHealth = 200;
        //set player health
        health = new Health(maxHealth,10, currentHealth);

        //Set The Player Weapon
        Debug.Log(weaponDamage + "is your wepaon damage");
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);
        OnHealthUpdate?.Invoke(health.GetHealth());
        Debug.Log("players health is "+health.GetHealth());
        

        //set the score, health, highscore
        //health
        ScoreManager.health = health.GetHealth();
    }

    private void Update()
    {
        //shoot on mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        //shoot at fast rate
        //only can be held while powerup is active
        if (Input.GetKey(KeyCode.Mouse0) && isPowerupActive && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootHold());
        }
        //shoo
        if (ScoreManager.bombsInventory > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("bomb is being shot");
                Shoot(bombPreFab, firePoint);
                _hasBomb = false;
                ScoreManager.bombsInventory--;
            }
        }
    }

    public override void Shoot()
    {
        //where is this supposed to be called from?
        weapon.Shoot(bulletPreFab, this, "Enemy");
        audioManager.PlaySFXAudio("player_laser_shoot");
    }

    public override void Attack(Transform target)
    {
        //not used, but needs to be here for interface...
    }

    public override void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        audioManager.PlaySFXAudio("player_hit_bullet");
        Debug.Log(health.GetHealth() + "is the players health");

        //update scoreboard
        ScoreManager.health = health.GetHealth();

        if (Mathf.RoundToInt(health.GetHealth()) <= 0)
        {
            Die();
            if (Mathf.RoundToInt(health.GetHealth()) < 0)
            {
                ScoreManager.health = 0;
            }
        } 
    }

    public IEnumerator ShootHold()
    {
        while (isPowerupActive && Input.GetKey(KeyCode.Mouse0)) 
        {
            yield return new WaitForSeconds(bulletPowerupSpeed);
            weapon.Shoot(bulletPreFab, this, "Enemy");
            audioManager.PlaySFXAudio("player_laser_shoot");
        }
        isShooting = false;
        
    }

    public void Shoot(Bomb _bomb, Transform _firePoint)
    {
        //instantiate,move
        Bomb tempBomb = Instantiate(_bomb, new Vector3(_firePoint.transform.position.x, _firePoint.transform.position.y, -0.11f), _firePoint.transform.rotation);
        tempBomb.isActiveInventory = false;
        tempBomb.thrown = true;
        //play sound
        //audioManager.PlaySFXAudio("bomb_shoot");
        //Debug.Log("Weapon is shooting");
        StartCoroutine(BlowUp(tempBomb));
    }

    public IEnumerator BlowUp(Bomb tempBomb)
    {
            explosion = tempBomb.GetComponentInChildren<ParticleSystem>();
            audioManager.PlaySFXAudio("bomb_fuse");
            yield return new WaitForSeconds(bombFuseTime);
            //blowup!
            audioManager.PlaySFXAudio("bomb_explode");
            explosion.Play();
            //kill all enemies
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                g.GetComponent<Enemy>().Die();
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(tempBomb.gameObject);
    }
}
