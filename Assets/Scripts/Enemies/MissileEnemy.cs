using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy
{
    [SerializeField] private float attackTime;

    [SerializeField] protected float bulletSpeed = 10;

    [SerializeField] protected Bullet bulletPreFab;

    [SerializeField] protected float bulletDamage;

    protected override void Start()
    {
        base.Start();
        health = new Health(200, 0, 200);
        weapon = new Weapon("Missile Enemy Weapon", bulletDamage, bulletSpeed);
    }

    protected void Awake()
    {
        StartCoroutine(ShootMissile(attackTime));
    }
    protected override void Update()
    {
        base.Update();
        //move while not in the player perimeter
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.right = direction;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    public void SetMissileEnemy(float _attackTime)
    {
        attackTime = _attackTime;
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }
    public override void Attack(float interval)
    {
        ShootMissile(interval);
    }

    private IEnumerator ShootMissile(float _interval)
    {
        while (gameObject != null)
        {
            yield return new WaitForSecondsRealtime(_interval);
            weapon.ShootMissile(bulletPreFab, "Player", GetComponent<MissileEnemy>());
        }
    }

    public override void Die()
    {
        base.Die();
        ScoreManager.score += 50;
    }
}
