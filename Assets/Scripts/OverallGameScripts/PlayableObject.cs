using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableObject : MonoBehaviour, IDamageable
{
    public Health health = new Health();
    //serialized fields for all inheritting members
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;

    public Weapon weapon;

    public virtual void Move(Vector2 direction, Vector2 target)
    {}

    public virtual void Move(Vector2 direction)
    {}

    public virtual void Move(float speed)
    {}

    public virtual void Shoot()
    {}

    public virtual void ShootPlayer()
    {}

    public virtual void Attack(float interval)
    {}

    public abstract void Attack(Transform target);

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        if (health.GetHealth() <= 0)
        {
            Die();
        }
        
    }
    
}
