using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private float regenAmount;
    [SerializeField] private AudioManager audioManager;

    DifficultyManager dM;
    GameObject difficultyManager;
   
    // Start is called before the first frame update
    void Awake()
    {
        difficultyManager = GameObject.Find("DifficultyManager");
        dM = difficultyManager.GetComponent<DifficultyManager>();

        audioManager = FindAnyObjectByType<AudioManager>();
        regenAmount = 50 + (dM.difficultyInc * 2);
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
