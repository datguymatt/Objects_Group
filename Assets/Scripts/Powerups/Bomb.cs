using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //variables
    public bool isActiveInventory = true;
    private bool isFirstTime;
    public ParticleSystem explosion;
    public bool thrown;
    //private float bombSpeed = 0.1f;

    public Player player;

    public void Awake()
    {
        player = FindFirstObjectByType<Player>();    
        explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Stop();
    }

    private void Update()
    {
    }
    public Bomb SetBomb(bool _isActiveInventory)
    {
        isActiveInventory = _isActiveInventory;
        
        return this;
    }

    /// Refacroting in progress
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActiveInventory)
        {
            player = collision.GetComponent<Player>();
            Player._hasBomb = true;
            ScoreManager.bombsInventory++;
            Destroy(gameObject);

        }

    }



}
