using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] GameObject healthVFX = default;
    [SerializeField] float durationOfVFX = 1f;
    [SerializeField] AudioClip healthSFX = default;
    [Range(0f,1f)] [SerializeField] float healthSFXVolume = 0.5f;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(player.GetHealth() < 300)
        {
            player.GainLife();
            GameObject healthSplash = Instantiate
                (healthVFX,
                 transform.position,
                 Quaternion.identity) as GameObject;
            Destroy(healthSplash, durationOfVFX);
            AudioSource.PlayClipAtPoint(healthSFX, Camera.main.transform.position, healthSFXVolume);
        }
        Destroy(gameObject);
    }
}
