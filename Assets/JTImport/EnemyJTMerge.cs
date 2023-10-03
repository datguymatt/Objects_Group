using UnityEngine;
using System.Collections;

public class EnemyJTMerge : PlayableObject
{
    private EnemyType enemyType;

    protected Transform target; 

    [SerializeField] protected float speed;
    [SerializeField] protected float weaponDamage;

    protected AudioManager audioManager;

    /// Jt Script---
    public string childObjectName = "Sprite"; // Name of the child object with SpriteRenderer
    public string childObjectAuraName = "EnemyAura"; // Name of the child object with SpriteRenderer

    public float damageAnimDuration = 0.25f; // Time in seconds for the color transition

    private Color startColor = Color.red;
    private Color endColor = Color.white;
    private Color auraColor = new Color(0.9150943f, 0.6100943f, 0f, 0.1294118f);
    private Color hurtAuraColor = new Color(0.9150943f, 0.6100943f, 0f, 0f);

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRendererAura; // Reference to the SpriteRenderer component

    /// JTEnd----------
    protected virtual void Start()
    {
        //find the player in the menu - this is the eternal target for the enemies
        //assign that position to the Transform object variable
        target = GameObject.FindWithTag("Player").transform;
        
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        /// JT Script
        Transform childObject = transform.Find(childObjectName);
        Transform childObjectAura = transform.Find(childObjectAuraName);
        spriteRenderer = childObject.GetComponent<SpriteRenderer>();
        spriteRendererAura = childObjectAura.GetComponent<SpriteRenderer>();

    }

    protected virtual void Update()
    {
        ////rotate towards player
        target = GameObject.FindWithTag("Player").transform;
    }
    public override void Move(Vector2 direction, Vector2 target)
    {}

    public override void Move(Vector2 direction)
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }
    public override void Move(float speed)
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.right = direction;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public override void Shoot()
    {}

    public override void ShootPlayer()
    {}

    public override void Attack(float interval)
    {}

    public override void Attack(Transform target)
    {}

    public void SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
    }

    public override void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        audioManager.PlaySFXAudio("enemy_hit_bullet");
        Debug.Log(health.GetHealth() + "is this enemies health");
        //JT Script
        StartCoroutine(LerpColor());
        //end

        if (health.GetHealth() <= 0)
        {
            Die();
        }
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

