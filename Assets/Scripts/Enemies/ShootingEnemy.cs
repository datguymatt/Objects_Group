using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] private float attackTime;

    [SerializeField] protected float bulletSpeed = 10;

    [SerializeField] protected Bullet bulletPreFab;

    [SerializeField] protected float bulletDamage;

    protected override void Start()
    {
        
        base.Start();
        health = new Health(200, 0, 200);
        weapon = new Weapon("Shooting Enemy Weapon", bulletDamage, bulletSpeed);
    }

    protected void Awake()
    {
        StartCoroutine(ShootBullet(attackTime));
    }
    protected override void Update()
    {
        base.Update();
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.right = direction;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    public void SetShootingEnemy(float _attackTime)
    {
        attackTime = _attackTime;
    }
    public override void Move(Vector2 direction, Vector2 target)
    {
        //move the enemy
        Debug.Log("");
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public override void Attack(float interval)
    {
        //start shooting
        ShootBullet(interval);
        
    }

    private IEnumerator ShootBullet(float _interval)
    {
        while(gameObject != null)
        {
            yield return new WaitForSecondsRealtime(_interval);
            weapon.ShootPlayer(bulletPreFab, "Player", GetComponent<ShootingEnemy>());
        }
    }

    public override void Die()
    {
        base.Die();
        ScoreManager.score += 50;
    }


}
