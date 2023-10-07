using System;
using System.Collections;
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

    /// Jt Script---
    public string childObjectName = "PlayerShip"; // Name of the child object with SpriteRenderer
    public string childObjectAuraName = "PlayerAura"; // Name of the child object with SpriteRenderer
    public float damageAnimDuration = 0.25f; // Time in seconds for the color transition

    private Color startColor = Color.red;
    private Color endColor = Color.white;
    private Color auraColor = new Color(0.9137255f, 0.0f, 0.8353961f, 0.1568628f);
    private Color hurtAuraColor = new Color(0.9150943f, 0.6100943f, 0f, 0f);

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRendererAura; // Reference to the SpriteRenderer component

    /// JTEnd----------

    public void SetPowerupFeatures()
    {
        isPowerupActive = true;
    }

    public void ResetPowerupsToNull()
    {
        isPowerupActive = false;
    }

    private void Awake()
    {
                /// JT Script
        Transform childObject = transform.Find(childObjectName);
        Transform childObjectAura = transform.Find(childObjectAuraName);

        spriteRenderer = childObject.GetComponent<SpriteRenderer>();

        spriteRendererAura = childObjectAura.GetComponent<SpriteRenderer>();
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

        StartCoroutine(LerpColor());


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
                if(g.gameObject.name == "Bullet(Clone)")
                {
                    //do nothing
                    Debug.Log("this is not an enemy");
                }
                else
                {
                    Debug.Log("I'm about to destroy" + g.gameObject.name);
                    if (g.gameObject.name != "Sprite")
                    {
                        g.GetComponent<Enemy>().Die();
                    }
                    Debug.Log("successfully destroyed" + g.gameObject.name);
                }
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(tempBomb.gameObject);
    }

        //JTSCript
        private IEnumerator LerpColor()
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < damageAnimDuration)
        {
            // Calculate the current progress of the lerp
            float t = elapsedTime / damageAnimDuration;

            // Interpolate the color from red to white
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            Color lerpedAura = Color.Lerp(hurtAuraColor, auraColor, t);
            // Assign the lerped color to the SpriteRenderer
            if (spriteRenderer != null)
            {
                spriteRenderer.color = lerpedColor;
                spriteRendererAura.color = lerpedAura;
            }

            // Update the elapsed time
            elapsedTime = Time.time - startTime;

            yield return null;
        }
    }
    //JTEnd
    
}
