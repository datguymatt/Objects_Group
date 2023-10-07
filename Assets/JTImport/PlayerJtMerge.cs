using System;
using System.Collections;
using UnityEngine;

public class PlayerJTMerge : PlayableObject
{
    //variables
    [SerializeField] private Camera cam;
    [SerializeField] protected float speed;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletPowerupSpeed = 0.5f;
    [SerializeField] private Bullet bulletPreFab;
    [SerializeField] private Bomb bombPreFab;
    [SerializeField] float maxHealthSet;
    [SerializeField] float currentHealthSet;

    //firepoint
    [SerializeField] private Transform firePoint;
        // JT Scripts, update the weapon shoot()function so that instead of passing a playableobject and then pulling its transform,
        // just directly pull a transform in the weapon method, and then make that point the shooting spawn point
        // in this script update Shoot() and ShootHold() to pass shootPoint instead of "this"
    [SerializeField] private Transform shootPoint;

    [SerializeField]
    private AudioManager audioManager;

    public Action<float> OnHealthUpdate;

    private bool isPowerupActive;
    private bool isShooting = false;

    /// Jt Script---
    public string childObjectName = "PlayerShip"; // Name of the child object with SpriteRenderer
    public string childObjectAuraName = "PlayerAura"; // Name of the child object with SpriteRenderer
    public float damageAnimDuration = 0.25f; // Time in seconds for the color transition

    private Color startColor = Color.red;
    private Color endColor = Color.white;
    private Color auraColor = new Color(0.9137255f, 0.0f, 0.8353961f, 0.282353f);
    private Color hurtAuraColor = new Color(0.9150943f, 0.6100943f, 0f, 0f);

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRendererAura; // Reference to the SpriteRenderer component

    /// JTEnd----------

    // bomb stuff
    public bool _hasBomb = false;
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
        health = new Health(maxHealthSet, 0.5f, currentHealthSet);
        //health.RegenHealth();

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
    //     //where is this supposed to be called from?
    //     weapon.Shoot(bulletPreFab, shootPoint, "Enemy");
    //     audioManager.PlaySFXAudio("player_laser_shoot");
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
            // weapon.Shoot(bulletPreFab, shootPoint, "Enemy");
            // audioManager.PlaySFXAudio("player_laser_shoot");
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
                Destroy(g);
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
