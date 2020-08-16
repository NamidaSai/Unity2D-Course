using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathVFX = default;
    [SerializeField] AudioClip deathSFX = default;
    [Range(0f, 1f)] [SerializeField] float deathSFXVolume = 0.5f;

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TriggerDeathVFX();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if(!deathVFX) { return; }
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(deathVFXObject, 1f);
    }
}
