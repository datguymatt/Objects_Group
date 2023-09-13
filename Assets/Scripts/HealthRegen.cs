using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private float regenAmount = 50;
    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.health.AddHealth(regenAmount);
            audioManager.PlaySFXAudio("player_health_regen");
            Destroy(gameObject);
        }
    }
}
