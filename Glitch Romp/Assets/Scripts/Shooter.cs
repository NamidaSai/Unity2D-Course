using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab = default;
    [SerializeField] GameObject hand = default;
    [SerializeField] AudioClip[] shootSFX = default;

    AttackerSpawner myLaneSpawner;
    Animator animator;
    GameObject projectileParent;
    AudioSource myAudioSource;

    const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach(AttackerSpawner spawner in spawners)
        {
            bool IsCloseEnough =
                (Mathf.Abs(spawner.transform.position.y - transform.position.y)
                <= Mathf.Epsilon);
            if (IsCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        return myLaneSpawner.transform.childCount > 0;
    }


    public void Fire()
    {
        Projectile newProjectile = Instantiate
            (projectilePrefab,
             hand.transform.position,
             Quaternion.identity) as Projectile;
        newProjectile.transform.parent = projectileParent.transform;

        AudioClip clip = shootSFX[UnityEngine.Random.Range(0,shootSFX.Length)];
        myAudioSource.PlayOneShot(clip);
    }
}
