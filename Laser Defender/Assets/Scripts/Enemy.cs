using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter = default;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] int scoreValue = 25;
    [SerializeField] bool isBoss = false;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab = default;
    [SerializeField] float projectileSpeed = 10f;

    [Header("FX")]
    [SerializeField] GameObject deathVFX = default;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX = default;
    [Range(0f,1f)] [SerializeField] float deathSFXVolume = 1f;
    [SerializeField] AudioClip[] shootingSFX = default;

    AudioSource myAudioSource;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate
            (laserPrefab,
             transform.position,
             Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioClip clip = shootingSFX[UnityEngine.Random.Range(0,shootingSFX.Length)];
        myAudioSource.PlayOneShot(clip);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate
            (deathVFX,
             transform.position,
             Quaternion.identity) as GameObject;
        Destroy(explosion,durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        if (isBoss)
        {
            FindObjectOfType<BossDeath>().BossDies();
        }
    }
}
