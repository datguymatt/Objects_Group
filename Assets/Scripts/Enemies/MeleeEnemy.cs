using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField] private float meleeDamage = 25;
    // Finding ScoreManager
    ScoreManager scoreManager;

    protected override void Start()
    {
        // Finding ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();

        //dif
        meleeDamage = meleeDamage + difficultyIncrease;

        // Enemy Start
        base.Start();
        health = new Health(50, 0, 50);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            Debug.Log("ENEMY collided with player");
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetDamage(meleeDamage);
            Destroy(gameObject);
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    protected override void Update()
    {
        if(target != null)
        {
            Attack(target); 
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.right = direction;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public override void Attack(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public override void Die()
    {
        scoreManager.score += 10;
        base.Die();
    }
}
