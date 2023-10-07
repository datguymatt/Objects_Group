using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float damage;

    [SerializeField] private bool isEnemyBullet;

    public void SetBullet(float _damage, string _targetTag, float speed, bool _isEnemyBullet)
    {
        this.damage = _damage;
        this.speed = speed;
        this.tag = _targetTag;
        isEnemyBullet = _isEnemyBullet;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && isEnemyBullet == false)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.GetDamage(damage);
            Destroy(gameObject);
        } else if (collision.CompareTag("Player") && isEnemyBullet == true)
        {
            Player player = collision.GetComponent<Player>();
            player.GetDamage(damage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
