using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 300;

    [Header("FX")]
    [SerializeField] AudioClip deathSFX = default;
    [Range(0f,1f)] [SerializeField] float deathSFXVolume = 1f;
    [SerializeField] AudioClip shootingSFX = default;
    [SerializeField] AudioClip hitSFX = default;
    [Range(0f,1f)] [SerializeField] float hitSFXVolume = 1f;
    [SerializeField] GameObject hitVFX = default;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab = default;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;
    AudioSource myAudioSource;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called upon initialisation.
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos,newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectileSpeed);
            myAudioSource.PlayOneShot(shootingSFX);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
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
        FindObjectOfType<HealthDisplay>().RemoveLife();
        HitFX();
        if(health <= 0)
        {
            Die();
        }
    }

    private void HitFX()
    {
        GameObject explosion = Instantiate
            (hitVFX,
             transform.position,
             Quaternion.identity) as GameObject;
        Destroy(explosion, durationOfExplosion);
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        }
    }

    private void Die()
    {
        FindObjectOfType<SceneLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public void GainLife()
    {
        health += 100;
        FindObjectOfType<HealthDisplay>().GainLife();
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
