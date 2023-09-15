using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon
{
    private string name;
    private float damage;
    private float bulletSpeed;

    

    public Weapon(string _name, float _damage, float _bulletSpeed)
    {
        name = _name;
        damage = _damage;
        bulletSpeed = _bulletSpeed;
    }

    public Weapon(){}

    public void ShootPlayer(Bullet _bullet, string _target, ShootingEnemy _enemy, float _timeToDie=5)
    {
        Debug.Log("shooting enemy is doing the thing");
        Player _player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        _bullet.SetBullet(damage, _target, bulletSpeed, true);
        Bullet tempBullet = GameObject.Instantiate(_bullet, new Vector3 (_enemy.transform.position.x, _enemy.transform.position.y, -0.11f), _enemy.transform.rotation);
        //Bullet tempBullet = GameObject.Instantiate(_bullet, new Vector3(_player.transform.position.x, _player.transform.position.y, -0.11f), _player.transform.rotation);
        GameObject.Destroy(tempBullet.gameObject, _timeToDie);
    }

    public void ShootMissile(Bullet _bullet, string _target, MissileEnemy _enemy, float _timeToDie = 5)
    {
        Debug.Log("shooting enemy is doing the thing");
        Player _player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        _bullet.SetBullet(damage, _target, bulletSpeed, true);
        Bullet tempBullet = GameObject.Instantiate(_bullet, new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, -0.11f), _enemy.transform.rotation);
        //Bullet tempBullet = GameObject.Instantiate(_bullet, new Vector3(_player.transform.position.x, _player.transform.position.y, -0.11f), _player.transform.rotation);
        GameObject.Destroy(tempBullet.gameObject, _timeToDie);
    }

    public void Shoot(Bullet _bullet, PlayableObject _player, string _target, float _timeToDie=5)
    {
        _bullet.SetBullet(damage, _target, bulletSpeed, false);
        Bullet tempBullet = GameObject.Instantiate(_bullet, new Vector3 (_player.transform.position.x, _player.transform.position.y, -0.11f), _player.transform.rotation);
        //play sound
        
        //Debug.Log("Weapon is shooting");
        GameObject.Destroy(tempBullet.gameObject, _timeToDie);
    }

    public void ShootHold()
    {

    }

    public float GetDamage()
    {
        return damage;
    }
}
