using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField] private float meleeDamage = 25;
    private ScoreManager scoreManager;
    protected override void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        //base is just the virtual class that it is inheriting from????
        base.Start();
        health = new Health(maxHealth, currentHealth);
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
        if(GameManager2.gameIsFinished == false)
        {
            Attack(target);
        }
    }

    public override void Attack(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public override void Die()
    {
        base.Die();
        scoreManager.score += 10;
    }
}
