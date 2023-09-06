using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : PlayableObject
{
    private EnemyType enemyType;

    protected Transform target; 

    [SerializeField] protected float speed;
    [SerializeField] protected float weaponDamage;

    protected AudioManager audioManager;

    protected virtual void Start()
    {
        //find the player in the menu - this is the eternal target for the enemies
        //assign that position to the Transform object variable
        target = GameObject.FindWithTag("Player").transform;
        
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    protected virtual void Update()
    {
        
        ////rotate towards player
        target = GameObject.FindWithTag("Player").transform;
        transform.LookAt(target);
        
        //old rotate
        //float angle = Mathf.Atan2(target.transform.position.x, target.transform.position.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Player player = GetComponent<Player>();
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        player.GetDamage(25);
    //        Destroy(gameObject);
    //    }
    //}

    //public void Update()
    //{
    //    //unsure why this is here?????
    //    switch (enemyType)
    //    {
    //        case EnemyType.Melee:
    //            Move(target.position);
    //            Attack(1f);
    //            break;
    //        case EnemyType.Exploder:
    //            Move(target.position);
    //            Shoot();
    //            break;
    //        case EnemyType.Shooter:
    //            Move(5.0f);
    //            Attack(1f);
    //            Shoot();
    //            break;
    //        case EnemyType.MachineGun:
    //            Move(target.position);
    //            Attack(0.5f);
    //            Shoot();
    //            break;
    //    }

    //}


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
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Shoot()
    {}

    public override void ShootPlayer()
    {
  
    }

    public override void Attack(float interval)
    {}

    public override void Attack(Transform target)
    {

    }

    public void SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
    }

    public override void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        audioManager.PlaySFXAudio("enemy_hit_bullet");
        Debug.Log(health.GetHealth() + "is this enemies health");
        if (health.GetHealth() <= 0)
        {
            Die();
        }

    }
}
